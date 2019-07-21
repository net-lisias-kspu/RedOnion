using System;
using System.IO;
using RedOnion.Utility.Settings;

namespace RedOnion.Utility
{
	public class IOUtility
	{
		public IOUtility()
		{
		}

		public static string ReadScript(string filename)
		{
			return File.ReadAllText(Path.Combine(GlobalSettings.BaseScriptsPath, filename));
		}
	}
}
