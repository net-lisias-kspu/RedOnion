using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using NUnit.Framework;
using RedOnion.KSP.API;
using RedOnion.KSP.MoonSharp.CommonAPI;
using UnityEngine;
using static KerbaluaNUnit.CommonAPITestUtil;

namespace KerbaluaNUnit
{
	[TestFixture()]
	public class LUA_CommonAPIInstantiationTests
	{
		Script script;
		Table globals;
		CommonAPICreator creator;
		Table apiTable;

		void Setup()
		{
			script = new Script(CoreModules.Preset_Complete);
			UserData.RegistrationPolicy = InteropRegistrationPolicy.Automatic;
			globals = script.Globals;
			creator=new CommonAPICreator(script);
			apiTable=creator.Create();
		}

		[Test()]
		public void LUA_CommonAPIInstantiationTests_Creation()
		{
			Setup();
		}

		[Test()]
		public void LUA_CommonAPIInstantiationTests_Field()
		{
			Setup();
			StaticCheck(apiTable, "pid");
		}

		[Test()]
		public void LUA_CommonAPIInstantiationTests_Property()
		{
			Setup();
			var t1=NamespaceCheck(apiTable,"time");
			MemberCheck(t1.MetaTable, "now", typeof(CallbackFunction));
		}

		[Test()]
		public void LUA_CommonAPIInstantiationTests_NamespaceField()
		{
			Setup();
			var t1=NamespaceCheck(apiTable,"ui");
			StaticCheck(t1, "Color");
		}

		[Test()]
		public void LUA_CommonAPIInstantiationTests_NamespaceMethod()
		{
			Setup();
			var t1=NamespaceCheck(apiTable,"autorun"); ;
			MemberCheck(t1, "save", typeof(CallbackFunction));
		}
	}
}
