using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using UnityEngine;

namespace RedOnion.KSP.ReflectionUtil
{
	public partial class NamespaceInstance : IUserDataType
	{
		public DynValue Index(MoonSharp.Interpreter.Script script, DynValue index, bool isDirectIndexing)
		{
			//Debug.Log(index);

			if (index.Type != DataType.String)
			{
				// Exceptions thrown from in here will get intercepted by interpreter and not shown
				throw new Exception("Type of index must be string for NamespaceInstance");
			}

			if (TryGetSubNamespace(index.String, out NamespaceInstance namespaceInstance))
			{
				return DynValue.FromObject(script,namespaceInstance);
			}

			if (TryGetType(index.String, out Type type))
			{
				return UserData.CreateStatic(type);
			}

			// Exceptions thrown from in here will get intercepted by interpreter and not shown
			throw new Exception("No type or subnamespace named " + index + " found in namespace " + ToString());
		}

		DynValue IUserDataType.MetaIndex(MoonSharp.Interpreter.Script script, string metaname)
		{
			//if (metaname == "__tostring")
			//{
			//	return DynValue.FromObject(script, new CallbackFunction(ToString));
			//}
			//Debug.Log(metaname);
			return null;
		}

		//DynValue ToString(ScriptExecutionContext arg1, CallbackArguments args)
		//{
		//	return DynValue.FromObject(arg1.OwnerScript,ToString());
		//}


		bool IUserDataType.SetIndex(MoonSharp.Interpreter.Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			throw new NotSupportedException("Cannot modify fields of "+nameof(NamespaceInstance));
		}
	}
}
