using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedOnion.Script.BasicObjects;

namespace RedOnion.Script.Completion
{
	public class CompletionRoot : BasicObject, IEngineRoot
	{
		protected IEngineRoot linked;
		public CompletionRoot(IEngine engine, IEngineRoot linkedRoot = null)
			: base(engine) => linked = linkedRoot;

		public IObject this[Type type]
		{
			get => null;
			set { }
		}

		public IObject Box(Value value)
		{
			for (; ; )
			{
				switch (value.Type)
				{
				case ValueKind.Object:
					return (IObject)value.ptr;
				case ValueKind.Reference:
					value = ((IProperties)value.ptr).Get(value.str);
					continue;
				case ValueKind.String:
					return new StringObj(Engine, null, value.str);
				default:
					if (value.IsNumber)
						return new NumberObj(Engine, null, value);
					throw new NotImplementedException();
				}
			}
		}

		public IObject Create(
			CompiledCode code, int codeAt, int codeSize, int typeAt,
			ArgumentInfo[] args, string body = null, IObject scope = null)
			=> null;
		public IObject GetType(OpCode OpCode)
			=> null;
		public IObject GetType(OpCode OpCode, Value value)
			=> null;
		public IObject GetType(OpCode OpCode, params Value[] par)
			=> null;
	}
}