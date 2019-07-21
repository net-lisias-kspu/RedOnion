using System;
using System.IO;

namespace RedOnion.Utility.Settings
{
	public class GlobalSettings
	{
		static public string BaseScriptsPath => Path.Combine(KSPUtil.ApplicationRootPath, "GameData/RedOnion/Scripts");
	}
}
