using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kerbalui.Types
{
	/// <summary>
	/// Base class for a Window
	/// </summary>
	public abstract class Window:Element
	{
		public readonly int windowID;

		public GUIContent titleContent=new GUIContent("");
		public GUIStyle titleStyle;
		Rect titleRect;
		Element content;
		GUIStyle windowStyle;

		public Window(string title)
		{
			titleContent.text=title;
			titleStyle=new GUIStyle(GUI.skin.label);
			windowStyle=new GUIStyle(GUI.skin.window);
			windowID=GUIUtility.GetControlID(FocusType.Keyboard);
		}

		protected void AssignContent(Element content)
		{
			this.content=content;
		}

		//bool firstRunPassed=false;
		//bool secondRunPassed=false;
		protected override void TypeSpecificUpdate()
		{
			rect=GUI.Window(windowID, rect, PointlessFunc, titleContent, windowStyle);
		}

		void PointlessFunc(int id)
		{
			GUI.DragWindow(titleRect);
			WindowsUpdate();
		}

		protected virtual void WindowsUpdate()
		{
			if (content!=null)
			{
				content.Update();
			}
		}

		public override void SetRect(Rect newRect)
		{
			base.SetRect(newRect);
			titleStyle.fontSize=(int)(KerbaluiSettings.DefaultFontsize*KerbaluiSettings.UI_SCALE);
			windowStyle.fontSize=titleStyle.fontSize;
			titleRect=new Rect(0, 0, newRect.width, Math.Max(titleStyle.CalcSize(titleContent).y, 10));
			content.SetRect(new Rect(0, titleRect.height, newRect.width, newRect.height-titleRect.height));
		}
	}
}
