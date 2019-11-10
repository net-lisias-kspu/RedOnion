using System;
using UnityEngine;
using LiveRepl.UI;

namespace LiveRepl.Main {
	public class KerbaluaRepl {
		public UI.LiveRepl liveRepl;

		public KerbaluaRepl()
		{
			InputLockManager.ClearControlLocks();

			liveRepl = new UI.LiveRepl(); //(new Rect(100, 100, 1000, 600));
		}

		public void Print(string str)
		{
			liveRepl.repl.outputBox.content.text += "\n" + str;
		}

		public void FixedUpdate()
		{
			liveRepl.FixedUpdate();
		}

		public void Render(bool guiActive)
		{

			if (!guiActive) return;

			try {
				//TestWindow.TestGUI();
				liveRepl.Update();
			} catch (Exception e) {
				//Debug.Log("yes");
				Debug.Log(e);
			}
		}

		public void OnDestroy()
		{
			liveRepl.OnDestroy();
		}
	}
}
