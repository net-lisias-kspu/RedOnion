using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using RedOnion.KSP.Completion;
using RedOnion.ROS;

namespace RedOnion.KSP.CommonAPIHelpers
{
	/// <summary>
	/// The purpose of this is to provide functionality like
	/// dictionary.keyname in-game, for example, for a mapping of planets accessed
	/// by planetname that is not to be modified.
	/// 
	/// Implementing this for ROS and Lua will allow us to add dictionaries like the
	/// global "bodies" in the future without having to create a custom per-engine
	/// definition per dictionary-like-thing.
	/// </summary>
	public class ScriptStringKeyedConstDictionary<T> : ICompletable, IUserDataType, ISelfDescribing
	{
		public ScriptStringKeyedConstDictionary()
		{
		}

		public Dictionary<string, T> baseDict = new Dictionary<string, T>();

		public IList<string> PossibleCompletions =>baseDict.Keys.ToList();

		public bool TryGetCompletion(string completionName, out object completion)
		{
			if(baseDict.TryGetValue(completionName, out T completionT))
			{
				completion = completionT;
				return true;
			}
			completion = null;
			return false;
		}

		public Descriptor Descriptor => throw new NotImplementedException("RedOnion.KSP.CommonAPIHelpers.StringKeyedConstDictionary<T> needs an implementation of ISelfDescribing");

		public DynValue Index(Script script, DynValue index, bool isDirectIndexing)
		{
			if (index.Type != DataType.String)
			{
				throw new Exception("Type of index must be string.");
			}

			if (baseDict.TryGetValue(index.String,out T value))
			{
				return DynValue.FromObject(script,value);
			}
			else
			{
				return DynValue.Nil;
			}
		}

		public DynValue MetaIndex(Script script, string metaname)
		{
			return null;
		}

		public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			throw new NotSupportedException("Cannot modify fields of this const dictionary.");
		}
	}
}
