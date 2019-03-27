using System;
using NUnit.Framework;
using RedOnion.Script;

namespace RedOnion.ScriptNUnit
{
	public class StatementTestsBase : EngineTestsBase
	{
		public void Test(OpCode code, object value, string script)
		{
			Test(script);
			Assert.AreEqual(code, Exit, "Test: <{0}>", script);
			Assert.AreEqual(value, Result.Native, "Test: <{0}>", script);
		}
		public void Lines(OpCode code, object value, params string[] lines)
			=> Test(code, value, string.Join(Environment.NewLine, lines));
	}

	[TestFixture]
	public class ROS_StatementTests : StatementTestsBase
	{
		[TearDown]
		public void ResetEngine() => Reset();

		[Test]
		public void ROS_EStts01_Return()
		{
			Test(OpCode.Return, null, "return");
			Test(OpCode.Return, 1234, "return 1234");
			Test(OpCode.Return, 12/5, "return 12/5");
		}

		[Test]
		public void ROS_EStts02_For()
		{
			Test(OpCode.Return, "321",
				"var s = \"\"\r\n" +
				"for var i = 3; i; i -= 1; s += i\r\n" +
				"return s");
		}

		[Test]
		public void ROS_EStts03_If()
		{
			Test(OpCode.Return, true, "if true then return true");
			Test(OpCode.Return, false, "if false: return true else: return false");
		}

		[Test]
		public void ROS_EStts04_FalseLoops()
		{
			Test("while false; fail");
			Test("until true; fail");
			Test("for; false;; fail");

			Test("while true; break; fail");
			Test("until false; break; fail");
			Test("for;;; break; fail");
			Expect<InvalidOperationException>("fail; fail");
		}
	}
}
