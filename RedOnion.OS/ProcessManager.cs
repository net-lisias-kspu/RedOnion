using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RedOnion.OS
{
	public class ProcessManager
	{
		public ProcessManager()
		{
		}

		static public ProcessManager instance;
		static public ProcessManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ProcessManager();
				}
				return instance;
			}
		}

		public void Reset()
		{
			processes.Clear();
		}

		public float UpdateMillis = 20;
		List<ROProcess> processes = new List<ROProcess>();

		public void RegisterProcess(ROProcess process)
		{
			processes.Add(process);
		}

		public void FixedUpdate()
		{
			// Just going to start with the normal crude behavior of
			// running each process until it completes for simplicity.
			//UnityEngine.Debug.Log("asdf " + processes.Count);

			if (processes.Count > 0)
			{
				processes[0].FixedUpdate(UpdateMillis);
			}
		}
	}
}
