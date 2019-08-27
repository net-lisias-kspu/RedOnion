using System;
using System.Collections.Generic;
using System.Diagnostics;
using RedOnion.OS.Executors;

namespace RedOnion.OS
{
	public class ExecutionManager
	{
		public enum Priority
		{
			REALTIME,
			ONESHOT,
			IDLE,
			MAIN
		}

		static public int MaxIdleSkips = 9;
		static public int MaxOneShotSkips = 1;

		Dictionary<Priority, PriorityExecutor> priorities = new Dictionary<Priority, PriorityExecutor>();

		public ExecutionManager()
		{
			priorities[Priority.REALTIME] = new RealtimeExecutor();
			priorities[Priority.ONESHOT] = new OneShotExecutor();
			priorities[Priority.IDLE] = new IdleExecutor();
			priorities[Priority.MAIN] = new NormalExecutor();
		}

		static public ExecutionManager instance;
		static public ExecutionManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ExecutionManager();
				}
				return instance;
			}
		}

		// Only let the exectors run until this percentage of update execution
		// time is remaining. In other words, only run Realtime at most
		// for the first half of execution time. Only run oneshot until 40%
		// remains (at most). Only run idle until 60% remains.
		static public double RealtimeFractionalLimit = 0.5;
		static public double OneShotFractionalLimit = 0.4;
		static public double IdleFractionalLimit = 0.6;
		static public double OneShotForceExecuteTime = 1;
		static public double IdleForceExecuteTime = 1;

		public void Reset()
		{
			processes.Clear();
		}

		public void RegisterExecutable(Priority priority, IExecutable executable)
		{
			priorities[priority].RegisterExecutable(executable);
		}

		Stopwatch stopwatch = new Stopwatch();
		public void Execute(double timeLimitMillis=20)
		{
			double remainingTime = 0;

			remainingTime = timeLimitMillis;
			var realtimeRuntime = remainingTime - timeLimitMillis * RealtimeFractionalLimit;
			stopwatch.Reset();
			stopwatch.Start();
			priorities[Priority.REALTIME].Execute(realtimeRuntime);
			stopwatch.Stop();

			remainingTime = remainingTime - stopwatch.Elapsed.TotalMilliseconds;
			var oneshotRuntime = remainingTime - timeLimitMillis * OneShotFractionalLimit;
			stopwatch.Reset();
			stopwatch.Start();
			priorities[Priority.ONESHOT].Execute(oneshotRuntime);
			stopwatch.Stop();

			remainingTime = remainingTime - stopwatch.Elapsed.TotalMilliseconds;
			var idleRuntime = remainingTime - timeLimitMillis * IdleFractionalLimit;
			stopwatch.Reset();
			stopwatch.Start();
			priorities[Priority.IDLE].Execute(idleRuntime);
			stopwatch.Stop();

			remainingTime = remainingTime - stopwatch.Elapsed.TotalMilliseconds;
			var normalRuntime = remainingTime;
			stopwatch.Reset();
			stopwatch.Start();
			priorities[Priority.MAIN].Execute(normalRuntime);
			stopwatch.Stop();
		}


		public float UpdateMillis = 20;
		List<ROProcess> processes = new List<ROProcess>();

		public void RegisterProcess(ROProcess process)
		{
			processes.Add(process);
		}

		public void FixedUpdate()
		{
			Execute(20);
		}
	}
}
