﻿using UnityEngine;
using Kerbalua.Other;
using System.Collections.Generic;
using System;
using System.IO;
using RedOnion.KSP.Autopilot;
using RedOnion.Utility.Settings;
using RedOnion.KSP.API;
using Kerbalui.Gui;
using LiveRepl.Other;
using RedOnion.OS;
using RedOnion.OS.Repl;
using Kerbalua.MoonSharp;

namespace LiveRepl
{
	public class ScriptWindow
	{
		const int maxOutputBytes = 80000;
		public string Title = "Live REPL";

		public Editor editor = new Editor();
		public Repl repl = new Repl();

		public CompletionBox completionBox = new CompletionBox();
		public AutoLayoutBox widgetBar = new AutoLayoutBox();
		public RecentFilesList recentFiles;

		//public ReplEvaluator currentReplEvaluator;

		public ReplProcess currentReplProcess;
		public ReplProcess lastOutputtedProcess;
		public Dictionary<string, ReplProcess> defaultReplProcesses = new Dictionary<string, ReplProcess>();
//		public Dictionary<string, ReplEvaluator> replEvaluators = new Dictionary<string, ReplEvaluator>();

		const int windowID = 0;
		const int modalID = 1;
		Rect mainWindowRect;
		bool editorVisible = true;
		bool replVisible = true;

		Rect widgetBarRect;
		Rect replRect;
		Rect completionBoxRect;
		Rect editorRect;
		//Rect scriptNameRect;
		ScriptNameInputArea scriptIOTextArea = new ScriptNameInputArea();
		// Should be a label but I haven't made a label yet.
		TextArea replEvaluatorLabel = new TextArea();

		public string saveLoadFilename = "untitled.b";

		const float titleHeight = 20;

		//List<Evaluation> evaluationList = new List<Evaluation>();
		bool inputIsLocked;
		//bool evaluationNotFinished = false;

		public KeyBindings GlobalKeyBindings = new KeyBindings();

		internal void OnDestroy()
		{
			SavedSettings.SaveSetting("WindowPositionX", mainWindowRect.x.ToString());
			SavedSettings.SaveSetting("WindowPositionY", mainWindowRect.y.ToString());
			//Debug.Log("On Destroy was called");
		}

		/// <summary>
		/// This manages completion. Keeps track of what is was focused and
		/// manages the completion interaction between an control that can
		/// use completion, and the completion 
		/// </summary>
		CompletionManager completionManager;

		public void SetCurrentReplProcess(string engineName)
		{
			currentReplProcess = defaultReplProcesses[engineName];
			replEvaluatorLabel.content.text = engineName;
		}


		//void RunStartupScripts(string engineName)
		//{
		//	if(engineName=="Lua Engine")
		//	{
		//		RunLuaStartupScripts();
		//	}
		//	if(engineName=="ROS Engine")
		//	{
		//		RunRosStartupScripts();
		//	}
		//}

		//void RunLuaStartupScripts()
		//{
		//	var scriptnames = AutoRun.Instance.scripts();
		//	foreach(var scriptname in scriptnames)
		//	{
		//		string extension = Path.GetExtension(scriptname).ToLower();
		//		string basename = Path.GetFileNameWithoutExtension(scriptname);
		//		if (extension==".lua")
		//		{
		//			repl.outputBox.AddFileContent("loading "+scriptname+"...");
		//			var newEvaluation = new Evaluation("require(\"" + basename + "\")",scriptname,replEvaluators["Lua Engine"]);
		//			evaluationList.Add(newEvaluation);
		//		}
		//	}
		//}

		//void RunRosStartupScripts()
		//{
		//	var scriptnames = AutoRun.Instance.scripts();
		//	foreach (var scriptname in scriptnames)
		//	{
		//		string extension = Path.GetExtension(scriptname).ToLower();
		//		string basename = Path.GetFileNameWithoutExtension(scriptname);
		//		if (extension == ".ros")
		//		{
		//			repl.outputBox.AddFileContent("loading " + scriptname + "...");
		//			var newEvaluation = new Evaluation("run \"" + scriptname+"\"", scriptname, replEvaluators["ROS Engine"]);
		//			evaluationList.Add(newEvaluation);
		//		}
		//	}
		//}

		public void FixedUpdate()
		{

		}

		public ScriptWindow(Rect param_mainWindowRect)
		{
			editorVisible = bool.Parse(SavedSettings.LoadSetting("editorVisible", "true"));

			defaultReplProcesses["Kerbalua"] = new ReplProcess(new KerbaluaScript());
			ProcessManager.Instance.RegisterProcess(defaultReplProcesses["Kerbalua"]);
			string lastEngineName = SavedSettings.LoadSetting("lastEngine", "Kerbalua");
			if (defaultReplProcesses.ContainsKey(lastEngineName))
			{
				currentReplProcess = defaultReplProcesses[lastEngineName];
				replEvaluatorLabel.content.text = lastEngineName;
			}
			else
			{
				foreach (var evaluatorName in defaultReplProcesses.Keys)
				{
					currentReplProcess = defaultReplProcesses[evaluatorName];
					replEvaluatorLabel.content.text = evaluatorName;
					SavedSettings.SaveSetting("lastEngine", evaluatorName);
					break;
				}
			}

			recentFiles = new RecentFilesList((string filename) =>
			{
				scriptIOTextArea.content.text = filename;
				editor.content.text = scriptIOTextArea.Load();
			});

			mainWindowRect = param_mainWindowRect;
			mainWindowRect.x = float.Parse(SavedSettings.LoadSetting("WindowPositionX", param_mainWindowRect.x.ToString()));
			mainWindowRect.y = float.Parse(SavedSettings.LoadSetting("WindowPositionY", param_mainWindowRect.y.ToString()));
			completionManager = new CompletionManager(completionBox);
			completionManager.AddCompletable(scriptIOTextArea);
			completionManager.AddCompletable(new EditingAreaCompletionAdapter(editor, this));
			completionManager.AddCompletable(new EditingAreaCompletionAdapter(repl.inputBox, this));

			editor.content.text = scriptIOTextArea.Load();

			editorRect = new Rect(
				0,
				titleHeight,
				500,
				param_mainWindowRect.height - titleHeight
				);
			widgetBarRect = new Rect(
				0,
				titleHeight,
				100,
				(param_mainWindowRect.height - titleHeight) / 2
				);
			replRect = new Rect(
				0,
				titleHeight,
				400,
				param_mainWindowRect.height - titleHeight
				);
			completionBoxRect = new Rect(
				0,
				titleHeight,
				150,
				param_mainWindowRect.height - titleHeight
				);

			widgetBarRect.x = editorRect.width;
			replRect.x = widgetBarRect.x + widgetBarRect.width;
			completionBoxRect.x = replRect.x + replRect.width;
			mainWindowRect.width = widgetBarRect.width + replRect.width + editorRect.width + completionBoxRect.width;

			widgetBar.renderables.Add(new Button("<<", () => {
				editorVisible = !editorVisible;
				SavedSettings.SaveSetting("editorVisible", editorVisible.ToString());
				}));
			widgetBar.renderables.Add(new Button(">>", () => {
				replVisible = !replVisible;
				SavedSettings.SaveSetting("editorVisible", editorVisible.ToString());
			}));

			widgetBar.renderables.Add(new Button("Save", () =>
			{
				scriptIOTextArea.Save(editor.content.text);
			}));
			widgetBar.renderables.Add(new Button("Load", () =>
			{
				editor.content.text = scriptIOTextArea.Load();
			}));
			widgetBar.renderables.Add(new Button("Run Script", () =>
			{
				scriptIOTextArea.Save(editor.content.text);
				currentReplProcess.EvaluateFile(scriptIOTextArea.content.text);
			}));
			widgetBar.renderables.Add(new Button("Reset Engine", () =>
			{
				currentReplProcess.ResetProcess();
			}));
			widgetBar.renderables.Add(new Button("Show Hotkeys", () =>
			{
				PrintHotkeysInOutputArea();
			}));

			foreach (var engineName in defaultReplProcesses.Keys)
			{
				widgetBar.renderables.Add(new Button(engineName, () =>
				{
					SetCurrentReplProcess(engineName);
					SavedSettings.SaveSetting("lastEngine", engineName);
				}));
			}
			widgetBar.renderables.Add(replEvaluatorLabel);
			widgetBar.renderables.Add(new Button("Kill Ctrl", () => { FlightControl.GetInstance().Shutdown(); }));

			InitializeKeyBindings();
		}

		void PrintHotkeysInOutputArea()
		{
			string hotkeyText =

@"CURRENT KEYBINDINGS(partially inspired by vim):

Common to Editor and Repl input area:
			shift + space: Intellisense completion. 
ctrl + e: evaluate content. For editor this also saves to the file in the filename input area
tab: indent
shift + tab: unindent
ctrl + tab: make current line's indentation match previous line's indentation
ctrl + j: move cursor left
ctrl + k: move cursor down
ctrl + l: move cursor up
ctrl +;: move cursor right
ctrl + m: move cursor to next tab left
shift + backspace: delete to next tab left
ctrl + backspace: delete to next '.' left
ctrl + comma: move cursor 4 lines down
ctrl + period: move cursor 4 lines up
ctrl +/: move cursor to next tab right
ctrl + Home: move cursor to start
ctrl + End: move cursor to end
Shift plus movement commands selects text.
ctrl + insert: copy selected text
shift + insert: paste selected text
ctrl + u: focus editor
ctrl + i: focus scriptNameTextArea
ctrl + o: focus repl input area
ctrl + p: focus completion/ intellisense box
ctrl + n: insert and move to new line after current line
ctrl + h: insert and move to new line before current line
  When creating a new line, its indentation starts matched with the indentation of the line above it.

Editor only:
ctrl + s: Saves the file in `Scripts` folder within KSP folder based on the name specified in the textarea that is dedicated to this.
ctrl + d: Loads the file from `Scripts` folder within KSP folder based on the name specified in the textarea that is dedicated to this.

REPL input area only:
enter: evaluate content and clear repl input area.
shift + enter: add newline
ctrl +[or ctrl + UpArrow: go to previous history
ctrl + ' or ctrl+DownArrow: go to next history

Completion area only:
ctrl + k: select next line down
ctrl + l: select next line up
ctrl + comma: select line 4 lines down
ctrl + period: select line 4 lines up
ctrl + enter: submit completion

Output Area:
Only allows the following.
ctrl + c: copy selected area.
ctrl + insert: copy selected area.
Any other key gives focus to input box.
";

			repl.outputBox.content.text += "\n" + hotkeyText;
		}

		void InitializeKeyBindings()
		{
			repl.inputBox.KeyBindings.Add(new EventKey(KeyCode.LeftBracket, true), () =>
			{
				//Debug.Log("history up");
				repl.inputBox.content.text = currentReplProcess.SourceHistory.Up();
				repl.inputBox.selectIndex = repl.inputBox.content.text.Length;
				repl.inputBox.cursorIndex = repl.inputBox.content.text.Length;
			});
			repl.inputBox.KeyBindings.Add(new EventKey(KeyCode.Quote, true), () =>
			{
				//Debug.Log("history down");
				repl.inputBox.content.text = currentReplProcess.SourceHistory.Down();
				repl.inputBox.selectIndex = repl.inputBox.content.text.Length;
				repl.inputBox.cursorIndex = repl.inputBox.content.text.Length;
			});
			repl.inputBox.KeyBindings.Add(new EventKey(KeyCode.UpArrow, true), () =>
			{
				//Debug.Log("history up");
				repl.inputBox.content.text = currentReplProcess.SourceHistory.Up();
				repl.inputBox.selectIndex = repl.inputBox.content.text.Length;
				repl.inputBox.cursorIndex = repl.inputBox.content.text.Length;
			});
			repl.inputBox.KeyBindings.Add(new EventKey(KeyCode.DownArrow, true), () =>
			{
				//Debug.Log("history down");
				repl.inputBox.content.text = currentReplProcess.SourceHistory.Down();
				repl.inputBox.selectIndex = repl.inputBox.content.text.Length;
				repl.inputBox.cursorIndex = repl.inputBox.content.text.Length;
			});

			GlobalKeyBindings.Add(new EventKey(KeyCode.U, true), () => editor.GrabFocus());
			GlobalKeyBindings.Add(new EventKey(KeyCode.I, true), () => scriptIOTextArea.GrabFocus());
			GlobalKeyBindings.Add(new EventKey(KeyCode.O, true), () => repl.inputBox.GrabFocus());
			GlobalKeyBindings.Add(new EventKey(KeyCode.P, true), () => completionBox.GrabFocus());
			GlobalKeyBindings.Add(new EventKey(KeyCode.D, true), () =>
			{
				editor.content.text = scriptIOTextArea.Load();
			});
			GlobalKeyBindings.Add(new EventKey(KeyCode.S, true), () =>
			{
				scriptIOTextArea.Save(editor.content.text);
			});
			GlobalKeyBindings.Add(new EventKey(KeyCode.Space, false, true), completionManager.Complete);
			GlobalKeyBindings.Add(new EventKey(KeyCode.Return, true), completionManager.Complete);

			editor.KeyBindings.Add(new EventKey(KeyCode.E, true), () =>
			{
				scriptIOTextArea.Save(editor.content.text);
				currentReplProcess.EvaluateFile(editor.content.text);
			});
			repl.inputBox.KeyBindings.Add(new EventKey(KeyCode.E, true), () =>
			{
				currentReplProcess.EvaluateSource(repl.inputBox.content.text);
			});
			repl.inputBox.KeyBindings.Add(new EventKey(KeyCode.Return), () =>
			{
				currentReplProcess.EvaluateSource(repl.inputBox.content.text, true);
				repl.inputBox.content.text = "";
				completionBox.content.text = "";
			});


		}

		/// <summary>
		/// This updates Rects for boxes that are not inside the main window 
		/// that I want near the main window, so that when the main window is 
		/// moved these boxes stay in the specified position relative to the 
		/// window.
		/// </summary>
		/// <param name="rect">The Rect to be updated.</param>
		/// <param name="xOffset">The desired xOffset from the main window.</param>
		/// <param name="yOffset">The desired yOffset from the main window.</param>
		public Rect UpdateBoxPositionWithWindow(Rect rect, float xOffset, float yOffset = 0)
		{
			rect.x = mainWindowRect.x + xOffset;
			rect.y = mainWindowRect.y + yOffset;
			return rect;
		}

		/// <summary>
		/// Returns the rectangle covering the entire area of the editor/repl/completion
		/// </summary>
		public Rect GetTotalArea()
		{
			return GetCurrentWindowRect();
		}

		public void SetOrReleaseInputLock()
		{
			if (GetTotalArea().Contains(Mouse.screenPos))
			{
				if (!inputIsLocked)
				{
					inputIsLocked = true;
					InputLockManager.SetControlLock(ControlTypes.KEYBOARDINPUT, "kerbalua");
				}
			}
			else
			{
				if (inputIsLocked)
				{
					inputIsLocked = false;
					InputLockManager.ClearControlLocks();
				}
			}
		}

		bool hadMouseDownLastUpdate = false;
		public void Update()
		{
			//UnityEngine.Debug.Log("blah");
			SetOrReleaseInputLock();

			completionManager.Update(hadMouseDownLastUpdate);
			hadMouseDownLastUpdate = false;

			//if (replVisible) {
			//	mainWindowRect.width = buttonBarRect.width + replRect.width;
			//} else {
			//	mainWindowRect.width = buttonBarRect.width;
			//}

			Rect effectiveWindowRect = GetCurrentWindowRect();
			Rect modifiedEffectiveRect = GUI.Window(windowID, effectiveWindowRect, MainWindow, Title);
			mainWindowRect.x += modifiedEffectiveRect.x - effectiveWindowRect.x;
			mainWindowRect.y += modifiedEffectiveRect.y - effectiveWindowRect.y;
			//GlobalKeyBindings.ExecuteAndConsumeIfMatched(Event.current)
		}



		/// <summary>
		/// To make input handling simpler, only the repl and button bar are in
		/// the main window. The other boxes are updated to always be beside this window.
		/// 
		/// This design decision may be reviewed in the future with a better understanding
		/// of IMGUI.
		/// </summary>
		/// <param name="id">Identifier.</param>
		void MainWindow(int id)
		{

			if (!currentReplProcess.IsIdle()
					&& Event.current.type == EventType.KeyDown
					&& Event.current.keyCode == KeyCode.C
					&& Event.current.control)
			{
				GUILibUtil.ConsumeAndMarkNextCharEvent(Event.current);
				currentReplProcess.Interrupt();
				currentReplProcess.PrintError("Execution Manually Terminated.");
			}

			if (!currentReplProcess.IsIdle())
			{
				EventType t = Event.current.type;
				if (t == EventType.KeyDown || t == EventType.MouseDown)
				{
					Event.current.Use();
				}
			}

			if (Event.current.type == EventType.MouseDown)
			{
				hadMouseDownLastUpdate = true;
			}

			Rect effectiveWindowRect = GetCurrentWindowRect();
			GUI.DragWindow(new Rect(0, 0, effectiveWindowRect.width, titleHeight));
			GlobalKeyBindings.ExecuteAndConsumeIfMatched(Event.current);

			Rect currentWidgetBarRect = GetCurrentWidgetBarRect();
			widgetBar.Update(currentWidgetBarRect);
			GUILayout.BeginArea(new Rect(
				currentWidgetBarRect.x,
				currentWidgetBarRect.height,
				currentWidgetBarRect.width,
				25
				));
			{
				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("Tabs");
					if (GUILayout.Button("+"))
					{
						List<string> recentFilesList = new List<string>(SavedSettings.LoadListSetting("recentFiles"));
						if (!recentFilesList.Contains(scriptIOTextArea.content.text))
						{
							recentFilesList.Add(scriptIOTextArea.content.text);
						}
						recentFilesList.RemoveAll((string filename) => !File.Exists(Path.Combine(GlobalSettings.BaseScriptsPath, filename)));
						recentFilesList.Sort((string s1, string s2) =>
						{
							var t1 = Directory.GetLastWriteTime(Path.Combine(GlobalSettings.BaseScriptsPath, s1));
							var t2 = Directory.GetLastWriteTime(Path.Combine(GlobalSettings.BaseScriptsPath, s2));
							if (t1 < t2) return 1;
							if (t1 > t2) return -1;
							return 0;
						});
						if (recentFilesList.Count > 10)
						{
							recentFilesList.RemoveAt(recentFilesList.Count - 1);
						}
						SavedSettings.SaveListSetting("recentFiles", recentFilesList);
					}
					if (GUILayout.Button("-"))
					{
						List<string> recentFilesList = new List<string>(SavedSettings.LoadListSetting("recentFiles"));
						if (!recentFilesList.Contains(scriptIOTextArea.content.text))
						{
							recentFilesList.Add(scriptIOTextArea.content.text);
						}
						recentFilesList.RemoveAll((string filename) => !File.Exists(Path.Combine(GlobalSettings.BaseScriptsPath, filename)));
						recentFilesList.Remove(scriptIOTextArea.content.text);
						if (recentFilesList.Count > 10)
						{
							recentFilesList.RemoveAt(recentFilesList.Count - 1);
						}
						SavedSettings.SaveListSetting("recentFiles", recentFilesList);
					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
			currentWidgetBarRect.y += widgetBarRect.height + 20;
			currentWidgetBarRect.height -= 20;
			recentFiles.Update(currentWidgetBarRect);

			if (replVisible)
			{
				//if(repl.outputBox.HasFocus()) {
				//	repl.inputBox.GrabFocus();
				//}
				if (currentReplProcess.OutputBuffer.FreshOutput 
					|| lastOutputtedProcess != currentReplProcess)
				{
					repl.outputBox.content.text = currentReplProcess.OutputBuffer.GetString();
					repl.outputBox.ResetScroll();
				}

				lastOutputtedProcess = currentReplProcess;

				repl.Update(GetCurrentReplRect(), replVisible);
			}

			if (editorVisible)
			{
				//editorRect = UpdateBoxPositionWithWindow(editorRect, -editorRect.width);
				var scriptIORect = GetCurrentScriptNameRect();
				scriptIORect.width /= 2;
				scriptIOTextArea.Update(scriptIORect, true);
				var editorInfoRect = new Rect(scriptIORect);
				editorInfoRect.x = scriptIORect.x + scriptIORect.width;
				GUI.Label(editorInfoRect, "Line: " + editor.LineNumber+", Column: "+editor.ColumnNumber);
				editor.Update(GetCurrentEditorRect(), editorVisible);
			}

			// Lots of hacks here. I will eventually better understand how this
			// works and replace it.
			if (replVisible || editorVisible)
			{
				bool lastEventWasMouseDown = Event.current.type == EventType.MouseDown;
				string lastControlname = GUI.GetNameOfFocusedControl();
				//completionBoxRect = UpdateBoxPositionWithWindow(completionBoxRect, mainWindowRect.width);
				if (inputIsLocked && Event.current.type == EventType.scrollWheel)
				{
					Event.current.Use();
				}
				completionBox.Update(GetCurrentCompletionBoxRect());
				if (lastEventWasMouseDown && Event.current.type == EventType.Used)
				{
					//Debug.Log("completionBox got a mouse down");
					//Debug.Log("trying to complete");
					Rect currentCompletionBoxRect = GetCurrentCompletionBoxRect();
					Rect rectMinusScrollBar = new Rect(currentCompletionBoxRect)
					{
						width = currentCompletionBoxRect.width - 20,
						height = currentCompletionBoxRect.height - 20
					};
					if (GUILibUtil.MouseInRect(rectMinusScrollBar))
					{
						completionManager.Complete();
						//GUI.FocusControl(lastControlname);
						completionManager.DisplayCurrentCompletions();
					}
				}
			}
		}

		Rect GetCurrentWindowRect()
		{
			Rect currentWindowRect = new Rect(mainWindowRect);
			if (!editorVisible)
			{
				currentWindowRect.x += editorRect.width;
				currentWindowRect.width -= editorRect.width;
			}
			if (!replVisible)
			{
				currentWindowRect.width -= replRect.width;
			}
			if (!replVisible && !editorVisible)
			{
				currentWindowRect.width -= completionBoxRect.width;
			}
			return currentWindowRect;
		}

		Rect GetCurrentEditorRect()
		{
			Rect currentEditorRect = new Rect(editorRect);
			float scriptNameHeight = GetCurrentScriptNameRect().height;
			currentEditorRect.y += scriptNameHeight;
			currentEditorRect.height = currentEditorRect.height - scriptNameHeight;

			return currentEditorRect;
		}

		Rect GetCurrentScriptNameRect()
		{
			Rect currentScriptNameRect = new Rect();
			currentScriptNameRect.y = titleHeight;
			currentScriptNameRect.x = 0;
			currentScriptNameRect.width = editorRect.width;
			currentScriptNameRect.height = 25;
			return currentScriptNameRect;
		}

		Rect GetCurrentWidgetBarRect()
		{
			Rect currentWidgetRect = new Rect(widgetBarRect);
			if (!editorVisible)
			{
				currentWidgetRect.x -= editorRect.width;
			}
			return currentWidgetRect;
		}

		Rect GetCurrentReplRect()
		{
			Rect currentReplRect = new Rect(replRect);
			if (!editorVisible)
			{
				currentReplRect.x -= editorRect.width;
			}
			return currentReplRect;
		}

		Rect GetCurrentCompletionBoxRect()
		{
			Rect currentCompletionBoxRect = new Rect(completionBoxRect);
			if (!editorVisible)
			{
				currentCompletionBoxRect.x -= editorRect.width;
			}
			if (!replVisible)
			{
				currentCompletionBoxRect.x -= replRect.width;
			}

			return currentCompletionBoxRect;
		}
	}
}
