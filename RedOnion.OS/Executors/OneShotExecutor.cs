﻿namespace RedOnion.OS.Executors
{
	public class OneShotExecutor : PriorityExecutor
	{
		int numSkips = 0;

		public override void Execute(double timeLimitMillis)
		{
			base.Execute(timeLimitMillis);
			if (timeLimitMillis < 0)
			{
				if (executeList.Count > 0)
				{
					if (numSkips < ExecutionManager.MaxOneShotSkips)
					{
						numSkips++;
					}
					else
					{
						numSkips = 0;
						for (int i = executeList.Count; i >= 0; i--)
						{
							if (executeList[i].IsSleeping())
							{
								executeList.RemoveAt(i);
							}
							else
							{
								executeList[i].Execute(ExecutionManager.OneShotForceExecuteTime);
								executeList.RemoveAt(i);
								break;
							}
						}
					}
				}
			}
		}
	}
}