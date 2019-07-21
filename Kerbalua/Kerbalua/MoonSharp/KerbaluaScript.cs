using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using RedOnion.KSP.Autopilot;
using UnityEngine;
using RedOnion.KSP.MathUtil;
using System;
using KSP.UI.Screens;
using RedOnion.KSP.Lua.Proxies;
using Kerbalua.Parsing;
using RedOnion.UI;
using System.Reflection;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using API = RedOnion.KSP.API;
using RedOnion.KSP.ReflectionUtil;
using static RedOnion.KSP.API.Reflect;
using RedOnion.OS.Repl;
using Kerbalua.Completion;
using MoonSharp.Interpreter.Loaders;
using RedOnion.Utility.Settings;

namespace Kerbalua.MoonSharp
{
	public class KerbaluaScript : Script, IReplEngine
	{
		static KerbaluaScript()
		{
			UserData.RegistrationPolicy = InteropRegistrationPolicy.Automatic;
			GlobalOptions.CustomConverters
				.SetClrToScriptCustomConversion(
					(Script script, ModuleControlSurface m)
						=> DynValue.FromObject(script, new LuaProxy(m)) //DynValue.NewTable(new ModuleControlSurfaceProxyTable(this, m))
					);
			// DON'T ATTEMPT TO IMPLEMENT ACTION's OR FUNC's to be passed into
			// csharp objects for things like "foreach". The entire execution
			// of the foreach will be uninterruptible.

			GlobalOptions.CustomConverters
				.SetScriptToClrCustomConversion(DataType.Function
					, typeof(UnityAction), (f) => new UnityAction(() =>
					{
						var script = (KerbaluaScript)f.Function.OwnerScript;
						try
						{
							var co = script.CreateCoroutine(f);
							co.Coroutine.AutoYieldCounter = 1000;
							script.coroutineQueue.Enqueue(co);
						}
						catch(Exception ex)
						{
							if (ex is InterpreterException interExcept)
							{
								script.replProcess.PrintError(interExcept.DecoratedMessage);
							}
							else
							{
								script.replProcess.PrintError(ex.Message);
							}
							Debug.Log(ex.ToString());
						}
					}));
		}

		ReplProcess replProcess=null;
		public KerbaluaScript() : base(CoreModules.Preset_Complete)
		{
			Options.DebugPrint = (string str) => {
				replProcess?.PrintOutput(str);
			};

			Options.ScriptLoader = new FileSystemScriptLoader();
			((ScriptLoaderBase)Options.ScriptLoader).IgnoreLuaPathGlobal = true;
			((ScriptLoaderBase)Options.ScriptLoader).ModulePaths = new string[] { GlobalSettings.BaseScriptsPath + "/?.lua" };

			Initialize();
		}


		Queue<DynValue> coroutineQueue = new Queue<DynValue>();

		DynValue CreateCoroutine(string source)
		{
			DynValue co = null;

				if (IncompleteLuaParsing.IsImplicitReturn(source))
				{
					source = "return " + source;
				}

				DynValue mainFunction = base.DoString("return function () " + source + " end");
				co=CreateCoroutine(mainFunction);
				co.Coroutine.AutoYieldCounter = 1000;
			

			return co;
		}

		public void SetReplProcess(ReplProcess replProcess)
		{
			this.replProcess = replProcess;
		}

		bool isWaitingForInput = false;
		public bool IsWaitingForInput()
		{
			return isWaitingForInput;
		}

		public bool IsIdle()
		{
			return coroutineQueue.Count == 0;
		}

		public void Interrupt()
		{
			coroutineQueue.Clear();
			isWaitingForInput = false;
			inputStr = "";
		}

		string inputStr = "";
		public void ReceiveInput(string str)
		{
			inputStr = str;
		}

		public void EvaluateSource(string source)
		{
			try
			{
				coroutineQueue.Enqueue(CreateCoroutine(source));
			}
			catch (Exception ex)
			{
				if (ex is InterpreterException interExcept)
				{
					replProcess.PrintError(interExcept.DecoratedMessage);
				}
				else
				{
					replProcess.PrintError(ex.Message);
				}
				Debug.Log(ex.ToString());
			}
		}

		string DynValueToString(DynValue dynValue)
		{
			string result = "";

			if (dynValue.Type == DataType.String)
			{
				result = "\"" + dynValue.ToObject() + "\"";
			}
			else if (dynValue.Type == DataType.Nil || dynValue.Type == DataType.Void)
			{
				result = dynValue.ToString();
			}
			else
			{
				result += dynValue.ToObject().ToString();
			}

			return result;
		}

		public void FixedUpdate(float timeAllotted)
		{
			if (coroutineQueue.Count > 0)
			{
				var coroutine = coroutineQueue.Peek();
				DynValue result=null;

				try
				{
					if (isWaitingForInput)
					{
						isWaitingForInput = false;
						var tempstr = inputStr;
						inputStr = "";
						result=coroutine.Coroutine.Resume(tempstr);
					}
					else
					{
						result=coroutine.Coroutine.Resume();
					}
				}
				catch(Exception ex)
				{
					if (ex is InterpreterException interExcept)
					{
						replProcess.PrintError(interExcept.DecoratedMessage);
					}
					else
					{
						replProcess.PrintError(ex.Message);
					}
					Debug.Log(ex.ToString());
				}

				if (coroutine.Coroutine.State == CoroutineState.Dead)
				{
					coroutineQueue.Dequeue();
					replProcess?.PrintReturnValue(DynValueToString(result));
				}
				else if(coroutine.Coroutine.State == CoroutineState.Suspended)
				{
					coroutineQueue.Enqueue(coroutineQueue.Dequeue());
				}
			}
		}

		public string EngineName()
		{
			return "Kerbalua";
		}

		public void Reset()
		{
			Interrupt();
			Globals.Clear();
			Globals.MetaTable = null;
			Globals.RegisterCoreModules(CoreModules.Preset_Complete);

			Initialize();
		}

		void Initialize()
		{
			Globals.MetaTable = API.LuaGlobals.Instance;

			var defaultMappings = NamespaceMappings.DefaultAssemblies;

			Globals["new"] = Constructor.Instance;
			Globals["static"] = new Func<object, DynValue>((o) =>
			{
				if (o is Type t)
				{
					return UserData.CreateStatic(t);
				}
				return UserData.CreateStatic(o.GetType());
			});
			Globals["gettype"] = new Func<object, DynValue>((o) =>
			{
				if (o is DynValue d && d.Type == DataType.UserData)
				{
					return DynValue.FromObject(this, d.UserData.Descriptor.Type);
				}
				if (o is Type t)
				{
					return DynValue.FromObject(this, t);
				}
				return DynValue.FromObject(this, o.GetType());
			});
			Globals["assemblies"] = new Func<List<Assembly>>(() =>
			{
				return AppDomain.CurrentDomain.GetAssemblies().ToList();
			});

			Globals["getstring"] = new Action(() =>
			{
				isWaitingForInput = true;

			});

			Globals["assembly"] = new GetMappings();

			Globals["printall"] = DoString(
			@"
				return function(lst) for i=0,lst.Count-1 do print(i..' '..lst[i].ToString()) end end
				");

			Globals["import"] = defaultMappings.GetNamespace("");

			Globals["reldir"] = new Func<double, double, RelativeDirection>((heading, pitch) => new RelativeDirection(heading, pitch));
		}

		public IList<string> GetCompletions(string source, int cursorPos, out int replaceStart, out int replaceEnd)
		{
			try
			{
				return LuaIntellisense.GetCompletions(Globals, source, cursorPos, out replaceStart, out replaceEnd);
			}
			catch (Exception e)
			{
				Debug.Log(e);
				replaceStart = replaceEnd = cursorPos;
				return new List<string>();
			}
		}

		public IList<string> GetDisplayableCompletions(string source, int cursorPos, out int replaceStart, out int replaceEnd)
		{
			return GetCompletions(source, cursorPos, out replaceStart, out replaceEnd);
		}
	}
}
