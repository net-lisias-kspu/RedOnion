using System;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using RedOnion.Collections;

namespace RedOnion.ROS.Parsing
{
	partial class Parser
	{
		/// <summary>
		/// Value buffer/stack (variables, expression trees in postfix notation)
		/// </summary>
		public ListCore<byte> values = new ListCore<byte>(256);
		/// <summary>
		/// Strings associated with value buffer/stack
		/// </summary>
		public ListCore<string> stringValues = new ListCore<string>(64);

		/// <summary>
		/// Push simple value (this, null, false, ...)
		/// </summary>
		protected void Push(ExCode op)
		{
			var start = values.size;
			ValuesReserve(5);
			ValuesPush((byte)op);
			ValuesPush(start);
		}

		/// <summary>
		/// Push string value (string, identifier)
		/// </summary>
		protected void Push(ExCode op, string value)
		{
			var start = values.size;
			int i;
			uint u;
			ulong ul;
			long ll;
			double d;
			char c;
			switch (op)
			{
			case ExCode.Identifier:
				ValuesReserve(9);
				ValuesPush(value);
				ValuesPush((byte)op);
				ValuesPush(start);
				return;

			case ExCode.Char:
				if (value.Length == 4 && value[0] == '\'' && value[3] == '\''
					&& value[1] >= (char)0xD800 && value[1] <= (char)0xDFFF
					&& value[2] >= (char)0xD800 && value[2] <= (char)0xDFFF)
				{
					int hi = value[1];
					int lo = value[2];
					if (hi >= 0xDC00 && lo < 0xDC00)
					{
						hi = value[2];
						lo = value[1];
					}
					ValuesReserve(9);
					ValuesPush(0x010000 + ((hi & 0x3FF) << 10) + (lo & 0x3FF));
					ValuesPush(OpCode.LongChar);
					ValuesPush(start);
					return;
				}
				if (value.Length == 4 && value[0] == '\'' && value[1] == '\\' && value[3] == '\'')
				{
					c = value[2];
					switch (c)
					{
					case 'r':
						c = '\r';
						break;
					case 'n':
						c = '\n';
						break;
					case 't':
						c = '\t';
						break;
					}
				}
				else
				{
					if (value.Length != 3 || value[0] != '\'' || value[2] != '\'')
						throw new InvalidOperationException();
					c = value[1];
				}
				if ((char)(byte)c == c)
				{
					ValuesReserve(6);
					ValuesPush((byte)c);
					ValuesPush((byte)op);
					ValuesPush(start);
					return;
				}
				ValuesReserve(7);
				ValuesPush((byte)c);
				ValuesPush((byte)(c >> 8));
				ValuesPush(OpCode.WideChar);
				ValuesPush(start);
				return;

			case ExCode.String:
				if (value[0] == '@')
				{
					if (value[1] != '"' || value[value.Length - 1] != '"')
						throw new InvalidOperationException();
					value = value.Substring(2, value.Length - 3);
					ValuesReserve(9);
					ValuesPush(value);
					ValuesPush((byte)op);
					ValuesPush(start);
					return;
				}
				if (value[0] != '"' || value[value.Length - 1] != '"')
					throw new InvalidOperationException();
				_stringBuilder.Length = 0;
				var n = value.Length - 1;
				for (i = 1; i < n;)
				{
					c = value[i++];
					switch (c)
					{
					default:
						_stringBuilder.Append(c);
						continue;
					case '\\':
						if (i >= n)
							throw new BadEscapeSequence(this);
						c = value[i++];
						switch (c)
						{
						default:
							_stringBuilder.Append(c);
							continue;
						case 'r':
							_stringBuilder.Append('\r');
							continue;
						case 'n':
							_stringBuilder.Append('\n');
							continue;
						case 't':
							_stringBuilder.Append('\t');
							continue;
						case 'u':
							if (i + 4 >= n)
								throw new BadEscapeSequence(this);
							var a = Nibble(value[i++]);
							var b = Nibble(value[i++]);
							var x = Nibble(value[i++]);
							var y = Nibble(value[i++]);
							c = (char)(y | (x << 4) | (b << 8) | (a << 12));
							_stringBuilder.Append(c);
							continue;
						}
					}
				}
				value = _stringBuilder.ToString();
				_stringBuilder.Length = 0;
				ValuesReserve(9);
				ValuesPush(value);
				ValuesPush((byte)op);
				ValuesPush(start);
				return;

			case ExCode.Number:
				var style = NumberStyles.Number;
				if (value.Length > 2 && value[0] == '0' && (value[1] == 'x' || value[1] == 'X'))
				{
					style = NumberStyles.HexNumber;
					value = value.Substring(2, value.Length - 2);
				}
				else if (value[0] == '.')
				{
					value = "0" + value;
				}
				var last = char.ToLower(value[value.Length - 1]);
				if (last == 'u')
				{
					value = value.Substring(0, value.Length - 1);
					if (uint.TryParse(value, style, Culture, out u))
					{
						ValuesReserve(5 + 4);
						ValuesPush(u);
						ValuesPush(OpCode.UInt);
						ValuesPush(start);
						return;
					}
					if (ulong.TryParse(value, style, Culture, out ul))
					{
						ValuesReserve(5 + 8);
						ValuesPush(ul);
						ValuesPush(OpCode.ULong);
						ValuesPush(start);
						return;
					}
					throw new InvalidOperationException();
				}
				if (last == 'l')
				{
					value = value.Substring(0, value.Length - 1);
					last = char.ToLower(value[value.Length - 1]);
					if (last == 'u')
					{
						value = value.Substring(0, value.Length - 1);
						ul = ulong.Parse(value, style, Culture);
						ValuesReserve(5 + 8);
						ValuesPush(ul);
						ValuesPush(OpCode.ULong);
						ValuesPush(start);
						return;
					}
					ll = long.Parse(value, style, Culture);
					ValuesReserve(5 + 8);
					ValuesPush(ll);
					ValuesPush(OpCode.Long);
					ValuesPush(start);
					return;
				}
				if (last == 'f' && style != NumberStyles.HexNumber)
				{
					value = value.Substring(0, value.Length - 1);
					if (double.TryParse(value, NumberStyles.Float, Culture, out d))
					{
						ValuesReserve(5 + 4);
						ValuesPush((float)d);
						ValuesPush(OpCode.Float);
						ValuesPush(start);
						return;
					}
					throw new InvalidOperationException();
				}
				if (value.IndexOf('.') < 0)
				{
					if (int.TryParse(value, style, Culture, out i))
					{
						ValuesReserve(5 + 4);
						ValuesPush(i);
						ValuesPush(OpCode.Int);
						ValuesPush(start);
						return;
					}
					if (long.TryParse(value, style, Culture, out ll))
					{
						ValuesReserve(5 + 8);
						ValuesPush(ll);
						ValuesPush(OpCode.Long);
						ValuesPush(start);
						return;
					}
				}
				d = double.Parse(value, NumberStyles.Float, Culture);
				ValuesReserve(5 + 8);
				ValuesPush(d);
				ValuesPush(OpCode.Double);
				ValuesPush(start);
				return;
			}
			throw new NotImplementedException(string.Format(Value.Culture,
				"Parser.Push {0:04X} {1} with string", (ushort)op, op));
		}
		private StringBuilder _stringBuilder = new StringBuilder();
		private int Nibble(char value)
		{
			var c = (int)value;
			if (c >= '0')
			{
				if (c <= '9')
					return c - '0';
				if (c >= 'a')
					c -= 'a' - 'A';
				if (c > 'A' && c < 'F')
					return c - 'A' + 10;
			}
			throw new BadNibbleCharacter(this);
		}


		/// <summary>
		/// Peek (read but not pop) top integer from value buffer/stack
		/// </summary>
		protected int TopInt()
			=> TopInt(values.size);

		/// <summary>
		/// Peek (read but not pop) top integer from value buffer/stack with end at @top
		/// </summary>
		/// <param name="top">Index after the integer</param>
		protected int TopInt(int top)
		{
			Debug.Assert(values.size >= top && top >= 4);
			int value = values.items[--top];
			value = (value << 8) | values.items[--top];
			value = (value << 8) | values.items[--top];
			return (value << 8) | values.items[--top];
		}

		/// <summary>
		/// Ensure value buffer/stack capacity
		/// </summary>
		protected void ValuesReserve(int size)
		{
			values.EnsureCapacity(values.size + size);
		}

		/// <summary>
		/// Pop top byte from value buffer/stack
		/// </summary>
		protected byte PopByte()
		{
			Debug.Assert(values.size > 0);
			return values.items[--values.size];
		}

		/// <summary>
		/// Pop top integer from value buffer/stack
		/// </summary>
		protected int PopInt()
		{
			Debug.Assert(values.size >= 4);
			var v = (int)values.items[--values.size];
			v = (v << 8) | values.items[--values.size];
			v = (v << 8) | values.items[--values.size];
			return (v << 8) | values.items[--values.size];
		}

		/// <summary>
		/// Push single byte to value buffer/stack
		/// </summary>
		protected void Push(byte value)
		{
			ValuesReserve(1);
			ValuesPush(value);
		}

		/// <summary>
		/// Push single integer to value buffer/stack
		/// </summary>
		protected void Push(int value)
		{
			ValuesReserve(4);
			ValuesPush(value);
		}

		/// <summary>
		/// Push string (sequence of characters) to value buffer/stack
		/// </summary>
		protected void Push(string value)
		{
			ValuesReserve(4);
			ValuesPush(value);
		}

		/// <summary>
		/// Push sequence of bytes
		/// </summary>
		protected void Push(byte[] bytes)
		{
			ValuesReserve(bytes.Length);
			ValuesPush(bytes);
		}
		/// <summary>
		/// Push sequence of bytes
		/// </summary>
		protected void Push(byte[] bytes, int i, int n)
		{
			ValuesReserve(n);
			ValuesPush(bytes, i, n);
		}

		/// <summary>
		/// Push single unsigned integer to value buffer/stack
		/// </summary>
		protected void Push(uint value)
		{
			ValuesReserve(4);
			ValuesPush(value);
		}

		/// <summary>
		/// Push single long integer to value buffer/stack
		/// </summary>
		protected void Push(long value)
		{
			ValuesReserve(8);
			ValuesPush(value);
		}

		/// <summary>
		/// Push single long unsigned integer to value buffer/stack
		/// </summary>
		protected void Push(ulong value)
		{
			ValuesReserve(8);
			ValuesPush(value);
		}

		/// <summary>
		/// Push single short integer to value buffer/stack
		/// </summary>
		protected void Push(short value)
		{
			ValuesReserve(2);
			ValuesPush(value);
		}

		/// <summary>
		/// Push single short unsigned integer to value buffer/stack
		/// </summary>
		protected void Push(ushort value)
		{
			ValuesReserve(2);
			ValuesPush(value);
		}

		/// <summary>
		/// Push single/float number to value buffer/stack
		/// </summary>
		protected void Push(float value)
		{
			ValuesReserve(4);
			ValuesPush(value);
		}

		/// <summary>
		/// Push double (fp) number to value buffer/stack
		/// </summary>
		protected void Push(double value)
		{
			ValuesReserve(8);
			ValuesPush(value);
		}

		internal void ValuesPush(byte value)
			=> values.items[values.size++] = value;
		internal void ValuesPush(OpCode value)
			=> values.items[values.size++] = (byte)value;

		internal void ValuesPush(int value)
		{
			values.items[values.size++] = (byte)value;
			values.items[values.size++] = (byte)(value >> 8);
			values.items[values.size++] = (byte)(value >> 16);
			values.items[values.size++] = (byte)(value >> 24);
		}

		internal void ValuesPush(string value)
		{
			if (value.Length == 0)
				ValuesPush(-1);
			else
			{
				ValuesPush(stringValues.size);
				stringValues.Push(value);
			}
		}

		internal void ValuesPush(byte[] bytes)
		{
			Array.Copy(bytes, 0, values.items, values.size, bytes.Length);
			values.size += bytes.Length;
		}
		internal void ValuesPush(byte[] bytes, int i, int n)
		{
			Array.Copy(bytes, i, values.items, values.size, n);
			values.size += n;
		}

		internal void ValuesPush(uint value)
		{
			values.items[values.size++] = (byte)value;
			values.items[values.size++] = (byte)(value >> 8);
			values.items[values.size++] = (byte)(value >> 16);
			values.items[values.size++] = (byte)(value >> 24);
		}

		internal void ValuesPush(ulong value)
		{
			ValuesPush((uint)value);
			ValuesPush((uint)(value >> 32));
		}

		internal void ValuesPush(long value)
		{
			ValuesPush((ulong)value);
		}

		internal void ValuesPush(short value)
		{
			values.items[values.size++] = (byte)value;
			values.items[values.size++] = (byte)(value >> 8);
		}

		internal void ValuesPush(ushort value)
		{
			values.items[values.size++] = (byte)value;
			values.items[values.size++] = (byte)(value >> 8);
		}

		internal unsafe void ValuesPush(float value)
			=> ValuesPush(*(uint*)&value);
		internal unsafe void ValuesPush(double value)
			=> ValuesPush(*(ulong*)&value);
	}
}
