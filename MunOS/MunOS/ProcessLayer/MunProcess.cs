﻿using System;
using System.Collections.Generic;
using System.Linq;
using MunOS.Core;
using UnityEngine;
using static RedOnion.Debugging.QueueLogger;

namespace MunOS.ProcessLayer
{
	public struct MunThreadEntry
	{
		public ExecPriority priority;
		public MunThread thread;

		public MunThreadEntry(ExecPriority priority, MunThread thread)
		{
			this.priority=priority;
			this.thread=thread;
		}
	}

	/// <summary>
	/// A <see cref="MunProcess"/> manages a number of <see cref="MunThread"/>s. It has a list of running threads and
	/// receives notice from a <see cref="MunThread"/> any time it completes execution, whereby the <see cref="MunProcess"/>
	/// removes it from the list of running threads, but not necessarily from the total list of threads this process manages.
	/// In the future, this class may implement functionality to have a thread wait on the completion of another thread, or another
	/// process.
	/// </summary>
	public abstract class MunProcess
	{
		static long nextID=1;
		public readonly long ID=nextID++;
		public static long CurrentID => MunThread.ExecutingThread?.parentProcess?.ID ?? 0;

		public string Name { get; set; } = "";
		protected MunProcess()
		{
		}

		public readonly ProcessOutputBuffer outputBuffer=new ProcessOutputBuffer();
		/// <summary>
		/// A mapping of running threads to <see cref="ExecInfo"/> ID's. Distinct from a thread ID.
		/// Threads can exist outside of <see cref="CoreExecMgr"/>, but these <see cref="ExecInfo"/> IDs are only valid between
		/// the time an IExecutable was added to <see cref="CoreExecMgr"/> and the time it was removed.
		/// Adding the same IExecutable to <see cref="CoreExecMgr"/> again leads to a different <see cref="ExecInfo"/> ID.
		/// Basically these IDs can be used to kill the corresponding threads execution and should not be used for anything else. 
		/// Threads will be removed from this dictionary by MunProcessThreadExecutionComplete, which will be fired whenever
		/// a MunThread completes execution.
		/// </summary>
		protected Dictionary<MunThread,long> runningThreads=new Dictionary<MunThread,long>();

		/// <summary>
		/// Where threads wait to be ran next update if the initQueue is empty.
		/// </summary>
		protected Queue<MunThreadEntry> waitingThreads=new Queue<MunThreadEntry>();

		/// <summary>
		/// Where threads wait in a queue to be executed one by one to initialize the process.
		/// </summary>
		protected Queue<MunThread> initQueue=new Queue<MunThread>();

		public int TotalThreadCount => runningThreads.Count+waitingThreads.Count+initQueue.Count;
		public int RunningThreadsCount => runningThreads.Count;

		/// <summary>
		/// Adds a  thread into the <see cref="waitingThreads"/> queue. This thread will be sent to the
		/// Core next update if no init threads are still running.
		/// </summary>
		/// <param name="priority">Priority.</param>
		/// <param name="thread">Thread.</param>
		public void EnqueueThread(ExecPriority priority, MunThread thread)
		{
			waitingThreads.Enqueue(new MunThreadEntry(priority, thread));
		}

		void ExecuteThread(MunThreadEntry threadEntry)
		{
			long RunningExecutableID=CoreExecMgr.Instance.RegisterExecutable(threadEntry.priority, threadEntry.thread);
			runningThreads[threadEntry.thread]=RunningExecutableID;
			threadEntry.thread.ExecutionComplete+=MunProcessThreadExecutionComplete;
		}

		protected void ExecuteNextInit()
		{
			if (initQueue.Count>0)
			{
				if (!runningThreads.ContainsKey(initQueue.Peek()))
				{
					ExecuteThread(new MunThreadEntry(ExecPriority.MAIN, initQueue.Peek()));
				}
			}
		}

		void HandleInitQueue(MunThread thread, Exception e)
		{
			if (initQueue.Peek()==thread)
			{
				runningThreads.Remove(thread);
				if (e!=null)
				{
					outputBuffer.AddError("Error during init for thread "+thread);
					outputBuffer.AddError(e.Message);
					outputBuffer.AddError("Calling Terminate on Process "+this);
					Terminate();
					return;
				}
				initQueue.Dequeue();
				ExecuteNextInit();
				return;
			}
			else
			{
				outputBuffer.AddError("Error during init for thread "+thread);
				outputBuffer.AddError("Completed thread was not the current init thread");
				outputBuffer.AddError("Calling Terminate on Process "+this);
				Terminate();
				return;
			}
		}

		void MunProcessThreadExecutionComplete(MunThread thread,Exception e)
		{
			if (initQueue.Count>0)
			{
				HandleInitQueue(thread, e);
				return;
			}

			// This thread should always be removed from runningThreads, regarldess
			// of what will be done with the thread because it is no longer running
			// in CoreExecMgr
			runningThreads.Remove(thread);
			if (e!=null)
			{
				outputBuffer.AddError(e.Message);
			}

			ThreadExecutionComplete(thread, e);
		}

		/// <summary>
		/// Called when a <see cref="MunThread"/> completes execution. The parameter e is only non-null if
		/// execution of the thread's <see cref="MunThread.Execute"/> function threw an Exception to the <see cref="CoreExecMgr"/>
		/// </summary>
		/// <param name="thread">The thread for which execution completed.</param>
		/// <param name="e">The exception that was thrown. Null if the thread completed normally or was terminated.</param>
		protected virtual void ThreadExecutionComplete(MunThread thread, Exception e)
		{

		}


		/// <summary>
		/// Event invoked on every physics update (Unity FixedUpdate).
		/// </summary>
		public event Action physicsUpdate;

		public virtual void FixedUpdate()
		{
			if (initQueue.Count == 0)
			{
				// If there are no initThreads, send all the waitingThreads into
				// the Core.
				foreach(var waitingThreadEntry in waitingThreads)
				{
					ExecuteThread(waitingThreadEntry);
				}
				waitingThreads.Clear();
			}

			var physics = physicsUpdate;
			if (physics != null)
			{
				foreach (Action handler in physics.GetInvocationList())
				{
					try
					{
						handler();
					}
					catch (Exception ex)
					{
						physicsUpdate -= handler;
						MunLogger.DebugLogArray($"Exception in process #{ID} physics update: {ex.Message}");
					}
				}
			}
		}

		/// <summary>
		/// Event invoked on every graphics update (Unity Update).
		/// </summary>
		public event Action graphicsUpdate;

		public virtual void Update()
		{
			var graphics = graphicsUpdate;
			if (graphics != null)
			{
				foreach (Action handler in graphics.GetInvocationList())
				{
					try
					{
						handler();
					}
					catch (Exception ex)
					{
						physicsUpdate -= handler;
						MunLogger.DebugLogArray($"Exception in process #{ID} graphics update: {ex.Message}");
					}
				}
			}
		}

		/// <summary>
		/// Process terminated.
		/// Subscribers can use <see cref="ShutdownHook{T}" /> to avoid hard-links.
		/// All subscriptions are removed prior to executing the handlers.
		/// </summary>
		public Action shutdown;

		public virtual void Terminate()
		{
			// first notify all subscribers that this process is shutting down
			var shutdown = this.shutdown;
			MunLogger.DebugLogArray($"Process ID#{ID} terminating. (shutdown: {shutdown?.GetInvocationList().Length ?? 0})");
			if (shutdown != null)
			{
				this.shutdown = null;
				foreach (var fn in shutdown.GetInvocationList())
				{
					try
					{
						fn.DynamicInvoke();
					}
					catch (Exception ex)
					{
						MunLogger.DebugLogArray($"Exception in process #{ID} shutdown: {ex.Message}");
					}
				}
			}

			// each thread will be removed from the list immediatly after
			// being killed.
			var ids=runningThreads.Values.ToList();
			MunLogger.DebugLogArray($"Process ID#{ID} terminating {ids.Count} thread(s).");
			foreach (var id in ids)
			{
				CoreExecMgr.Instance.Kill(id);
			}
			initQueue.Clear();
			waitingThreads.Clear();

			MunLogger.DebugLogArray($"Process ID#{ID} terminated.");
		}

		/// <summary>
		/// Used to subscribe to <see cref="shutdown"/> but avoid direct hard-link
		/// so that the target/subscriber can be garbage-collected.
		/// </summary>
		/// <remarks>
		/// The target/subscriber should dispose this object in its own Dispose() method.
		/// </remarks>
		public class ShutdownHook : ShutdownHook<IDisposable>
		{
			public ShutdownHook(IDisposable target) : base(target) { }
		}
		/// <summary>
		/// Used to subscribe to <see cref="shutdown"/> but avoid direct hard-link
		/// so that the target/subscriber can be garbage-collected.
		/// </summary>
		/// <remarks>
		/// The target/subscriber should dispose this object in its own Dispose() method.
		/// </remarks>
		public class ShutdownHook<T> : IDisposable
			where T : class, IDisposable
		{
			protected WeakReference<T> _target;
			public MunProcess process { get; protected set; }
			public T Target
			{
				get
				{
					T target = null;
					return _target?.TryGetTarget(out target) == true ? target : null;
				}
			}

			public ShutdownHook(T target)
			{
				_target = new WeakReference<T>(target);
				process = MunThread.ExecutingThread.parentProcess;
				process.shutdown += Shutdown;
				MunLogger.DebugLogArray($"ShutdownHook for process ID#{process.ID} created.");
			}
			~ShutdownHook() => Dispose(false);
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				Dispose(true);
			}
			protected virtual void Dispose(bool disposing)
			{
				if (process == null)
					return;
				process.shutdown -= Shutdown;
				process = null;
				_target = null;

			}
			protected virtual void Shutdown()
			{
				var target = Target;
				var tgtstr = target == null ? "target is null" : "disposing target";
				MunLogger.DebugLogArray($"ShutdownHook invoked for process ID#{process.ID}, {tgtstr}.");
				target?.Dispose();
				Dispose();
			}
		}
	}
}