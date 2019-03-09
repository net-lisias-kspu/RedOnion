using System;
using System.Reflection;
using NUnit.Framework;
using RedOnion.Script;
using RedOnion.Script.ReflectedObjects;

namespace RedOnion.ScriptNUnit
{
	[TestFixture]
	public class ObjectReflectionTests : EngineTestsBase
	{
		[TearDown]
		public void ResetEngine() => Reset();

		public class PointClass
		{
			public int x, y;
			// these should shadow the fields
			// thanks to ordinal compare in ReflectedType.MemberComparer
			public int X { get => x; set => x = value; }
			public int Y { get => y; set => y = value; }

			public void MakeZero() => x = y = 0;
			public void MakeSame(int v) => x = y = v;

			public string name;
		}

		[Test]
		public void ObjectReflection_01_Methods()
		{
			var creator = new ReflectedType(this, typeof(PointClass));
			Root[typeof(PointClass)] = creator;
			Root.Set("Point", new Value(creator));

			Test("var pt = new point");
			var pt = Result.Native as PointClass;
			Assert.NotNull(pt);
			Test("pt.makeSame 1");
			Assert.AreEqual(1, pt.x);
			Assert.AreEqual(1, pt.y);
			Test("pt.makeZero()");
			Assert.AreEqual(0, pt.x);
			Assert.AreEqual(0, pt.y);
		}

		[Test]
		public void ObjectReflection_02_FieldsAndProps()
		{
			var creator = new ReflectedType(this, typeof(PointClass));
			Root[typeof(PointClass)] = creator;
			Root.Set("Point", new Value(creator));
			Test("var pt = new point");
			var pt = Result.Native as PointClass;
			Assert.NotNull(pt);
			Test(0, "pt.x");
			Test(1, "pt.y = 1");
			Test(1, "pt.y");
			Test("point", "pt.name = \"point\"");
			Test("point", "pt.name");
		}

		public struct Rect
		{
			public float X, Y, Width, Height;
			public float Left { get => X; set => X = value; }
			public float Right { get => X+Width; set => Width = value-X; }
			public float Top { get => Y; set => Y = value; }
			public float Bottom { get => Y+Height; set => Height = value-Y; }
		}

		public static class RectFunctions
		{
			public static float Area(Rect rc) => rc.Width*rc.Height;
		}

		[Test]
		public void ObjectReflection_03_Structure()
		{
			var creator = new ReflectedType(this, typeof(Rect));
			Root[typeof(Rect)] = creator;
			Root.Set("Rect", new Value(creator));
			Test("var rc = new rect");
			Assert.True(Result.Native is Rect);
			Test("rc.x = 10");
			Test(10, "rc.x");
			Test("rc.right = 30");
			Test(30, "rc.right");
			Test(20, "rc.width");

			Root.Set("area", new Value(new ReflectedFunction(this, null, "area",
				new MethodInfo[] { typeof(RectFunctions).GetMethod("Area") })));
			Test("rc.height = 10");
			Test(200, "area rc");
		}

	}
}