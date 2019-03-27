using System;
using System.Collections.Generic;
using RedOnion.Script.Parsing;

namespace RedOnion.Script
{
	/// <summary>
	/// Runtime engine
	/// </summary>
	public class Engine : Engine<Parser>, IEngine
	{
		public Engine()
			: this(engine => new BasicObjects.BasicRoot(engine)) { }
		public Engine(Func<IEngine, IEngineRoot> createRoot)
			: base(createRoot, new Parser()) { }
		public Engine(Parser.Option parserOptions)
			: base(engine => new BasicObjects.BasicRoot(engine), new Parser(parserOptions)) { }
		public Engine(Func<IEngine, IEngineRoot> createRoot, Parser.Option parserOptions)
			: base(createRoot, new Parser(parserOptions)) { }
	}
	/// <summary>
	/// Runtime engine
	/// </summary>
	public partial class Engine<P> : AbstractEngine, IEngine where P : Parser
	{
		/// <summary>
		/// Root object (global namespace)
		/// </summary>
		public IEngineRoot Root { get; set; }

		/// <summary>
		/// Every statement or function call will decrease this (if positive)
		/// and will throw exception if it reaches zero
		/// </summary>
		/// <remarks>Indexing and creation of objects counted as well</remarks>
		public int ExecutionCountdown
		{
			get => executionCountdown;
			set => executionCountdown = countdownFrom = value;
		}
		protected int executionCountdown, countdownFrom;
		public int CountdownPercent => countdownFrom == 0 ? 100 : executionCountdown*100/countdownFrom;

		public class TookTooLong : Exception
		{
			public TookTooLong() : base("Took too long") { }
		}
		protected void CountStatement()
		{
			if (ExecutionCountdown > 0 && --ExecutionCountdown == 0)
				throw new TookTooLong();
		}

		protected Engine(Func<IEngine, IEngineRoot> createRoot, P parser)
		{
			Parser = parser;
			Root = createRoot(this);
			Context = new EngineContext(this);
		}

		/// <summary>
		/// Run script in a string
		/// </summary>
		public void Execute(string source)
		{
			var compiled = Compile(source);
			Execute(compiled);
		}

		/// <summary>
		/// Reset engine
		/// </summary>
		public virtual void Reset()
		{
			var reset = Resetting?.GetInvocationList();
			if (reset != null)
			{
				foreach (var action in reset)
				{
					try
					{
						action.DynamicInvoke(this);
					}
					catch (Exception ex)
					{
						DebugLog("Exception in Engine.Reset: " + ex.Message);
						Resetting -= (Action<IEngine>)action;
					}
				}
			}
			Exit = 0;
			Root.Reset();
			Parser.Reset();
			Arguments.Clear();
			Context = new EngineContext(this);
			ContextStack.Clear();
		}
		/// <summary>
		/// Event executed on reset (before the root is cleared)
		/// </summary>
		public event Action<IEngine> Resetting;

		~Engine() => Dispose(false);
		public void Dispose() => Dispose(true);
		protected virtual void Dispose(bool disposing)
		{
			if (Parser == null)
				return;
			try
			{

				var reset = Resetting?.GetInvocationList();
				if (reset != null)
				{
					foreach (var action in reset)
					{
						try
						{
							action.DynamicInvoke(this);
						}
						catch { }
					}
				}
				Root.Dispose();
				Root = null;
			}
			catch { }
			Parser = null;
		}


		/// <summary>
		/// Compile source to code
		/// </summary>
		public CompiledCode Compile(string source)
		{
			string[] strings;
			byte[] code;
			int[] lineMap;
			CompiledCode.SourceLine[] lines;
			try
			{
				Parser.Unit(source);
				code = Parser.CodeToArray();
				strings = Parser.StringsToArray();
				lineMap = Parser.LineMapToArray();
				lines = Parser.LinesToArray();
			}
			finally
			{
				Parser.Reset();
			}
			return new CompiledCode(strings, code, lineMap)
			{
				Source = source,
				Lines = lines
			};
		}

		/// <summary>
		/// Box value (StringObj, NumberObj, ...)
		/// </summary>
		public virtual IObject Box(Value value)
		{
			if (value.IsReference)
				value = value.RValue;
			if (value.Kind == ValueKind.Object)
				return (IObject)value.ptr;
			return Root.Box(value);
		}

		/// <summary>
		/// Parser
		/// </summary>
		protected P Parser;
		/// <summary>
		/// Argument list for function calls
		/// </summary>
		public ArgumentList Arguments { get; } = new ArgumentList();

		/// <summary>
		/// Current context (method)
		/// </summary>
		protected EngineContext Context;
		/// <summary>
		/// Stack of contexts (methods)
		/// </summary>
		protected Stack<EngineContext> ContextStack = new Stack<EngineContext>();

		/// <summary>
		/// Create new execution/activation context (for function call)
		/// </summary>
		public IObject CreateContext(IObject self, IScope scope, int argc)
		{
			ContextStack.Push(Context);
			Context = new EngineContext(this, self, scope, argc);
			return Context.Vars.BaseClass;
		}

		/// <summary>
		/// Create new variables holder object
		/// </summary>
		public virtual IScope CreateVars(IObject baseClass)
			=> new BasicObjects.BasicObject(this, baseClass);

		/// <summary>
		/// Destroy last execution/activation context
		/// </summary>
		public Value DestroyContext()
		{
			Context = ContextStack.Pop();
			var value = Exit == OpCode.Return ? Result : new Value();
			if (Exit != OpCode.Raise)
				Exit = OpCode.Undefined;
			return value;
		}
	}
}
