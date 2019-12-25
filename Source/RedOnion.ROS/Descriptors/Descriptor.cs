using System;
using System.Collections.Generic;
using System.Threading;

namespace RedOnion.ROS
{
	public abstract partial class Descriptor
	{
		public string Name { get; }
		public Type Type { get; }
		public ExCode Primitive { get; }
		public TypeCode TypeCode { get; }
		public bool IsNumber => Primitive.Kind() == OpKind.Number;
		public bool IsNumberOrChar { get; }
		public bool IsStringOrChar { get; }
		public bool IsFpNumber { get; }
		public bool IsIntegral { get; }

		public virtual object Box(ref Value self)
			=> self.obj;
		public virtual bool IsInstanceOf(ref Value it)
			=> Type.IsInstanceOfType(it.Box());
		public virtual bool Equals(ref Value self, object obj)
			=> obj is Value val ? val.desc == this && self.obj.Equals(val.obj) : self.obj.Equals(obj);
		public virtual int GetHashCode(ref Value self)
			=> self.obj.GetHashCode();

		protected Descriptor(Type type)
			: this(type.Name, type) { }
		protected Descriptor(string name, Type type)
		{
			Name = name ?? type.Name;
			Type = type;
			Primitive = type.IsEnum ? ExCode.Enum : type.IsValueType ? ExCode.Struct : ExCode.Class;
			TypeCode = TypeCode.Object;
		}
		protected Descriptor(string name)
		{
			var type = GetType();
			Name = name ?? type.Name;
			Type = type;
			Primitive = ExCode.Class;
			TypeCode = TypeCode.Object;
		}
		protected Descriptor()
		{
			var type = GetType();
			Name = type.Name;
			Type = type;
			Primitive = ExCode.Class;
			TypeCode = TypeCode.Object;
		}
		internal Descriptor(string name, Type type, ExCode primitive, TypeCode typeCode)
		{
			Name = name;
			Type = type;
			Primitive = primitive;
			TypeCode = typeCode;
			var code = (OpCode)primitive;
			IsNumberOrChar = code >= OpCode.Char && code < OpCode.Create;
			IsStringOrChar = code >= OpCode.String && code <= OpCode.LongChar;
			IsFpNumber = code >= OpCode.Float && code <= OpCode.LongDouble;
			IsIntegral = code.Kind() == OpKind.Number && !IsFpNumber;
		}

		public override string ToString() => Name;
		public virtual string ToString(ref Value self, string format, IFormatProvider provider, bool debug)
			=> format != null && self.obj is IFormattable fmt
			? fmt.ToString(format, provider) : self.obj?.ToString() ?? Name;

		public virtual bool Convert(ref Value self, Descriptor to)
		{
			if (to.Primitive == ExCode.String)
			{
				self = ToString(ref self, null, Value.Culture, false);
				return true;
			}
			return false;
		}

		public virtual int Find(object self, string name, bool add = false) => -1;
		public virtual string NameOf(object self, int at) => null;
		public virtual bool Get(ref Value self, int at) => false;
		public virtual bool Set(ref Value self, int at, OpCode op, ref Value value) => false;

		public virtual bool Unary(ref Value self, OpCode op) => false;
		public virtual bool Binary(ref Value lhs, OpCode op, ref Value rhs) => false;
		public virtual bool Call(ref Value result, object self, Arguments args, bool create = false) => false;

		public virtual IEnumerable<Value> Enumerate(object self) => null;
		public virtual IEnumerable<string> EnumerateProperties(object self)
		{
			yield break;
		}

		public virtual int IndexFind(ref Value self, Arguments args)
		{
			if (args.Length == 0)
				return -1;
			ref var index = ref args.GetRef(0);
			int at;
			if (index.IsNumber)
				return -1;
			if (!index.desc.Convert(ref index, String))
				return -1;
			var name = index.obj.ToString();
			at = Find(self.obj, name, true);
			if (at < 0 || args.Length == 1)
				return at;
			if (!Get(ref self, at))
				return -1;
			return self.desc.IndexFind(ref self, new Arguments(args, args.Length-1));
		}

		static protected InvalidOperationException InvalidOperation(string msg)
			=> new InvalidOperationException(msg);
		static protected InvalidOperationException InvalidOperation(string msg, params object[] args)
			=> new InvalidOperationException(string.Format(Value.Culture, msg, args));
	}
}
