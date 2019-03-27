using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedOnion.Script
{
	public class RuntimeError : Exception
	{
		public CompiledCode Code { get; }
		public int CodeAt { get; }

		private int _lineNumber = -1;
		public int LineNumber
		{
			get
			{
				if (_lineNumber < 0)
				{
					int it = Array.BinarySearch(Code.LineMap, CodeAt-1);
					if (it < 0)
					{
						it = ~it;
						if (it > 0)
							it--;
						_lineNumber = it;
					}
				}
				return _lineNumber;
			}
		}

		public string Line => Code.Lines[LineNumber].Text;
		public int Position => Code.Lines[LineNumber].Position;

		public RuntimeError(CompiledCode code, int at, Exception innerException, string message)
			: base(message ?? innerException.Message, innerException)
		{
			Code = code;
			CodeAt = at;
		}
		public RuntimeError(CompiledCode code, int at, Exception innerException)
			: base(innerException.Message, innerException)
		{
			Code = code;
			CodeAt = at;
		}
		public RuntimeError(CompiledCode code, int at, string message)
			: base(message, null)
		{
			Code = code;
			CodeAt = at;
		}
		public RuntimeError(CompiledCode code, int at, string message, params object[] args)
			: base(string.Format(Value.Culture, message, args), null)
		{
			Code = code;
			CodeAt = at;
		}
	}
}