using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using RedOnion.KSP.CommonAPIHelpers;
using RedOnion.KSP.Completion;

namespace RedOnion.KSP.API
{
	[Description("A dictionary mapping body names to CelestialBody instances")]
	public class BodiesDictionary //: ScriptStringKeyedConstDictionary<CelestialBody>
	{
		public BodiesDictionary()
		{
		}

		static BodiesDictionary instance = null;
		public static BodiesDictionary Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new BodiesDictionary();
					//var bodiesArray = FlightGlobals.Bodies;
					//foreach(var body in bodiesArray)
					//{
					//	instance.Add(body.bodyName.ToLower(), body);
					//}
				}
				return instance;
			}
		}
	}
}
