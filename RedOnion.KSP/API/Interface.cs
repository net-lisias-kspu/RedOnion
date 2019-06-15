using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using RedOnion.ROS;
using RedOnion.KSP.Completion;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace RedOnion.KSP.API
{
	/// <summary>
	/// Link from instance to creator (ROS object to function).
	/// </summary>
	public class CreatorAttribute : Attribute
	{
		public Type Creator { get; }
		public CreatorAttribute(Type creator)
			=> Creator = creator;
	}
	/// <summary>
	/// Used for <c>Globals</c> and classes derived directly from <c>Table</c>
	/// to avoid disturbing MoonSharp in RedOnion.Builder. See <c>GlobalMembers</c> for example.
	/// </summary>
	public class ProxyDocsAttribute : Attribute
	{
		public Type ForType { get; }
		public ProxyDocsAttribute(Type type)
			=> ForType = type;
	}
	/// <summary>
	/// Ignore this class in RedOnion.Builder (e.g. RuntimeRoot).
	/// </summary>
	public class IgnoreForDocsAttribute : Attribute
	{
	}
	/// <summary>
	/// Sorted list/dictionary of object members with all the documentation.
	/// </summary>
	public class MemberList : IEnumerable<IMember>, ICompletable
	{
		readonly Dictionary<string, KeyValuePair<int, IMember>> dict;
		readonly IMember[] list;
		public string Help { get; }
		public MemberList(string help, IMember[] members)
		{
			Help = help;
			list = members;
			dict = new Dictionary<string, KeyValuePair<int, IMember>>
				(list.Length, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < members.Length; i++)
			{
				var member = members[i];
				dict.Add(member.Name, new KeyValuePair<int, IMember>(i, member));
			}
		}
		public IEnumerator<IMember> GetEnumerator()
		{
			for (int i = 0; i < list.Length; i++)
				yield return list[i];
		}
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
		public int Count => list.Length;
		public int Length => list.Length;

		public IList<string> PossibleCompletions => dict.Keys.ToList();

		public IMember this[int i]
			=> list[i];
		public IMember this[string name]
			=> dict.TryGetValue(name, out var it) ? it.Value : null;
		public int Find(string name)
			=> dict.TryGetValue(name, out var it) ? it.Key : -1;
		public bool Contains(string name)
			=> dict.ContainsKey(name);
		public bool TryGetValue(string name, out IMember member)
		{
			if (dict.TryGetValue(name, out var pair))
			{
				member = pair.Value;
				return true;
			}
			member = null;
			return false;
		}
		public bool TryGetCompletion(string completionName, out object completion)
		{
			if (TryGetValue(completionName, out IMember member))
			{
				completion = member;
				return true;
			}
			completion = null;
			return false;
		}
	}
	/// <summary>
	/// Documentation and list of members for the type.
	/// </summary>
	public interface IType
	{
		MemberList Members { get; }
	}
	/// <summary>
	/// Class member (property, methods are read-only properties returning callable object).
	/// </summary>
	public interface IMember
	{
		string Name { get; }
		string Type { get; }
		string Help { get; }
		bool CanRead { get; }
		bool CanWrite { get; }

		Value RosGet(object self);
		DynValue LuaGet(object self);
		void RosSet(object self, Value value);
		void LuaSet(object self, DynValue value);
	}
	/// <summary>
	/// Static boolean property.
	/// </summary>
	public class Bool : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<bool> Get { get; }
		public Action<bool> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.NewBoolean(Get());
		public void RosSet(object self, Value value)
			=> Set(value.ToBool());
		public void LuaSet(object self, DynValue value)
			=> Set(value.Boolean);

		public Bool(string name, string help,
			Func<bool> read, Action<bool> write = null)
		{
			Name = name;
			Type = "bool";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Boolean property.
	/// </summary>
	public class Bool<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, bool> Get { get; }
		public Action<Self, bool> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.NewBoolean(Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, value.ToBool());
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, value.Boolean);

		public Bool(string name, string help,
			Func<Self, bool> read, Action<Self, bool> write = null)
		{
			Name = name;
			Type = "bool";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Static integer property.
	/// </summary>
	public class Int : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<int> Get { get; }
		public Action<int> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get());
		public void RosSet(object self, Value value)
			=> Set(value.ToInt());
		public void LuaSet(object self, DynValue value)
			=> Set((int)value.Number);

		public Int(string name, string help,
			Func<int> read, Action<int> write = null)
		{
			Name = name;
			Type = "int";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Integer property.
	/// </summary>
	public class Int<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, int> Get { get; }
		public Action<Self, int> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, value.ToInt());
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, (int)value.Number);

		public Int(string name, string help,
			Func<Self, int> read, Action<Self, int> write = null)
		{
			Name = name;
			Type = "int";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Static unsigned integer property.
	/// </summary>
	public class UInt : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<uint> Get { get; }
		public Action<uint> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get());
		public void RosSet(object self, Value value)
			=> Set(value.ToUInt());
		public void LuaSet(object self, DynValue value)
			=> Set((uint)value.Number);

		public UInt(string name, string help,
			Func<uint> read, Action<uint> write = null)
		{
			Name = name;
			Type = "uint";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Unsigned integer property.
	/// </summary>
	public class UInt<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, uint> Get { get; }
		public Action<Self, uint> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, value.ToUInt());
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, (uint)value.Number);

		public UInt(string name, string help,
			Func<Self, uint> read, Action<Self, uint> write = null)
		{
			Name = name;
			Type = "uint";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Static single-precision floating-point property.
	/// </summary>
	public class Float : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<float> Get { get; }
		public Action<float> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get());
		public void RosSet(object self, Value value)
			=> Set((float)value.ToDouble());
		public void LuaSet(object self, DynValue value)
			=> Set((float)value.Number);

		public Float(string name, string help,
			Func<float> read, Action<float> write = null)
		{
			Name = name;
			Type = "float";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Single-precision floating-point property.
	/// </summary>
	public class Float<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, float> Get { get; }
		public Action<Self, float> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, (float)value.ToDouble());
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, (float)value.Number);

		public Float(string name, string help,
			Func<Self, float> read, Action<Self, float> write = null)
		{
			Name = name;
			Type = "float";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Static double-precision floating-point property.
	/// </summary>
	public class Double : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<double> Get { get; }
		public Action<double> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get());
		public void RosSet(object self, Value value)
			=> Set(value.ToDouble());
		public void LuaSet(object self, DynValue value)
			=> Set(value.Number);

		public Double(string name, string help,
			Func<double> read, Action<double> write = null)
		{
			Name = name;
			Type = "double";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Double-precision floating-point property.
	/// </summary>
	public class Double<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, double> Get { get; }
		public Action<Self, double> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.NewNumber(Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, value.ToDouble());
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, value.Number);

		public Double(string name, string help,
			Func<Self, double> read, Action<Self, double> write = null)
		{
			Name = name;
			Type = "double";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Static text property.
	/// </summary>
	public class String : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<string> Get { get; }
		public Action<string> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.NewString(Get());
		public void RosSet(object self, Value value)
			=> Set(value.ToStr());
		public void LuaSet(object self, DynValue value)
			=> Set(value.String);

		public String(string name, string help,
			Func<string> read, Action<string> write = null)
		{
			Name = name;
			Type = "string";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Text property.
	/// </summary>
	public class String<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, string> Get { get; }
		public Action<Self, string> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.NewString(Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, value.ToStr());
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, value.String);

		public String(string name, string help,
			Func<Self, string> read, Action<Self, string> write = null)
		{
			Name = name;
			Type = "string";
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Static property getting/setting C#-native object.
	/// </summary>
	public class Native : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<object> Get { get; }
		public Action<object> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.FromObject(null, Get());
		public void RosSet(object self, Value value)
			=> Set(value.obj);
		public void LuaSet(object self, DynValue value)
			=> Set(value.ToObject());

		public Native(string name, string type, string help,
			Func<object> read, Action<object> write = null)
		{
			Name = name;
			Type = type;
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Property getting/setting C#-native object.
	/// </summary>
	public class Native<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, object> Get { get; }
		public Action<Self, object> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.FromObject(null, Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, value.obj);
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, value.ToObject());

		public Native(string name, string type, string help,
			Func<Self, object> read, Action<Self, object> write = null)
		{
			Name = name;
			Type = type;
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Property getting/setting C#-native object.
	/// </summary>
	public class Native<Self, T> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, T> Get { get; }
		public Action<Self, T> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.FromObject(null, Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, (T)value.obj);
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, (T)value.ToObject());

		public Native(string name, string help,
			Func<Self, T> read, Action<Self, T> write = null)
		{
			Name = name;
			Type = typeof(T).Name;
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Static property getting/setting interop object (usable by both ROS and LUA).
	/// </summary>
	public class Interop : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<object> Get { get; }
		public Action<object> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get());
		public DynValue LuaGet(object self)
			=> DynValue.FromObject(null, Get());
		public void RosSet(object self, Value value)
			=> Set(value.obj);
		public void LuaSet(object self, DynValue value)
			=> Set(value.ToObject());

		public Interop(string name, string type, string help,
			Func<object> read, Action<object> write = null)
		{
			Name = name;
			Type = type;
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Property getting/setting interop object (usable by both ROS and LUA).
	/// </summary>
	public class Interop<Self> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, object> Get { get; }
		public Action<Self, object> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.FromObject(null, Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, (InteropObject)value.obj);
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, (InteropObject)value.ToObject());

		public Interop(string name, string type, string help,
			Func<Self, object> read, Action<Self, object> write = null)
		{
			Name = name;
			Type = type;
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Property getting/setting interop object (usable by both ROS and LUA).
	/// </summary>
	public class Interop<Self, T> : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		public bool CanRead { get; }
		public bool CanWrite { get; }

		public Func<Self, T> Get { get; }
		public Action<Self, T> Set { get; }

		public Value RosGet(object self)
			=> new Value(Get((Self)self));
		public DynValue LuaGet(object self)
			=> DynValue.FromObject(null, Get((Self)self));
		public void RosSet(object self, Value value)
			=> Set((Self)self, (T)value.obj);
		public void LuaSet(object self, DynValue value)
			=> Set((Self)self, (T)value.ToObject());

		public Interop(string name, string type, string help,
			Func<Self, T> read, Action<Self, T> write = null)
		{
			Name = name;
			Type = type;
			Help = help;
			CanRead = read != null;
			CanWrite = write != null;
			Get = read;
			Set = write;
		}
	}
	/// <summary>
	/// Abstract base class for functions and methods.
	/// </summary>
	public abstract class Method : IMember
	{
		public string Name { get; }
		public string Type { get; }
		public string Help { get; }
		bool IMember.CanRead => true;
		bool IMember.CanWrite => false;
		public abstract Value RosGet(object self);
		public abstract DynValue LuaGet(object self);
		void IMember.RosSet(object self, Value value) { }
		void IMember.LuaSet(object self, DynValue value) { }
		protected Method(string name, string type, string help)
		{
			Name = name;
			Type = type;
			Help = help;
		}
	}
	/// <summary>
	/// Static property returning a function.
	/// </summary>
	public class Function : Method, IMember
	{
		public Func<FunctionBase> Get { get; }

		public override Value RosGet(object self)
			=> new Value(Get());
		public override DynValue LuaGet(object self)
			=> DynValue.FromObject(null, Get());

		public Function(string name, string type, string help,
			Func<FunctionBase> read) : base(name, type, help)
			=> Get = read;
	}
}
