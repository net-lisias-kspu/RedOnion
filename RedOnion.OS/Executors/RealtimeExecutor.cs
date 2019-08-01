using System.Collections.Generic;
using System.Diagnostics;

namespace RedOnion.OS.Executors
{
	public class RealtimeExecutor : PriorityExecutor
	{
		Stopwatch stopwatch = new Stopwatch();

		public override void Execute(double timeLimitMillis)
		{
			base.Execute(timeLimitMillis);
		}
	}
}