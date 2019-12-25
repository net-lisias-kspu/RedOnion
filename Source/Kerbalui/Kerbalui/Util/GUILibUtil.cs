﻿using System;
using System.Collections.Generic;
using System.IO;
using RedOnion.KSP.Settings;
using UnityEngine;

namespace Kerbalui.Util
{
	public class GUILibUtil
	{
		/// <summary>
		/// Checks whether the mouse is currently in the given local rect coordinates
		/// using the less buggy Mouse.screenPos rather than Event.current.mousePosition
		/// This is only relevant if called in Render
		/// </summary>
		/// <param name="rect">Rect.</param>
		public static bool MouseInRect(Rect rect)
		{
			Rect absoluteRect = new Rect();
			Vector2 absoluteRectStart = GUIUtility.GUIToScreenPoint(new Vector2(rect.x, rect.y));
			//Debug.Log(absoluteRectStart);
			absoluteRect.x = absoluteRectStart.x;
			absoluteRect.y = absoluteRectStart.y;
			absoluteRect.width = rect.width;
			absoluteRect.height = rect.height;


			//Debug.Log($"{absoluteRect.Contains(Mouse.screenPos)},{Mouse.screenPos},{absoluteRect}");
			return absoluteRect.Contains(Mouse.screenPos);
		}

		public static Vector2 MouseRelativeToRect(Rect rect)
		{
			Rect absoluteRect = new Rect();
			Vector2 absoluteRectStart = GUIUtility.GUIToScreenPoint(new Vector2(rect.x, rect.y));
			absoluteRect.x = absoluteRectStart.x;
			absoluteRect.y = absoluteRectStart.y;
			absoluteRect.width = rect.width;
			absoluteRect.height = rect.height;

			Vector2 relativeMousePos = new Vector2(Mouse.screenPos.x - absoluteRect.x, Mouse.screenPos.y - absoluteRect.y);
			//Debug.Log($"{absoluteRect.Contains(Mouse.screenPos)},{Mouse.screenPos},{absoluteRect}");
			return relativeMousePos;
		}



		public struct FontEntry
		{
			public string name;
			public int size;

			public FontEntry(string name, int size)
			{
				this.name=name;
				this.size=size;
			}
		}
		// I'm assuming that Font.CreateDynamicFontFromOSFont is an expensive operation, so I'm gonna cache the created fonts
		static public Dictionary<FontEntry,Font> FontCache=new Dictionary<FontEntry, Font>();
		/// <summary>
		/// Gets the font. Uses a FontCache as Font.CreateDynamicFontFromOSFont may be an expensive operation.
		/// </summary>
		/// <returns>The font.</returns>
		/// <param name="fontname">Font name.</param>
		/// <param name="size">Size.</param>
		static public Font GetFont(string fontname,int size)
		{
			var fontEntry=new FontEntry(fontname,size);
			Font font=null;
			if (FontCache.ContainsKey(fontEntry))
			{
				font=FontCache[fontEntry];
			}
			else
			{
				font=Font.CreateDynamicFontFromOSFont(fontname, size);
				FontCache[fontEntry]=font;
			}
			return font;
		}

		static public string[] windowsMonotypeFonts={"Courier New", "Consolas" };
		static public string[] linuxMonotypeFonts={"Ubuntu Mono", "Noto Mono", "FreeMono", "DejaVu Sans Mono"};
		static Font monoSpaceFont = null;
		static public Font GetMonoSpaceFont()
		{
			if (monoSpaceFont == null)
			{
				string fontName=GetMonoSpaceFontName();
				monoSpaceFont=Font.CreateDynamicFontFromOSFont(fontName, 14);
			}
			if (monoSpaceFont==null)
			{
				throw new Exception("Could not find a font");
			}
			return monoSpaceFont;
		}

		static public string GetMonoSpaceFontName()
		{
			HashSet<string> fontNames=new HashSet<string>(Font.GetOSInstalledFontNames());

			foreach (var fontName in windowsMonotypeFonts)
			{

				if (fontNames.Contains(fontName))
				{
					return fontName;
				}
			}

			foreach (var fontName in linuxMonotypeFonts)
			{
				if (fontNames.Contains(fontName))
				{
					//Debug.Log("returning "+fontName);
					return fontName;
				}
			}

			foreach (var fontName in fontNames)
			{
				if (fontName.EndsWith("Mono", StringComparison.CurrentCulture))
				{
					return fontName;
				}
			}

			foreach (var fontName in fontNames)
			{
				if (fontName.Contains("Mono"))
				{
					return fontName;
				}
			}
			foreach (var fontName in fontNames)
			{
				return fontName;
			}

			return "";
		}

		static bool consumeNextCharEvent;
		/// <summary>
		/// Consumes the current event, assumed to be a keycode event
		/// and marks any followup character event to also be consumed.
		/// </summary>
		/// <param name="event1">Event1.</param>
		static public void ConsumeAndMarkNextCharEvent(Event event1)
		{
			if (event1.keyCode != KeyCode.None)
			{
				event1.Use();
				consumeNextCharEvent = true;
			}
		}
		/// <summary>
		/// Consumes the current char event that was marked. If it is not
		/// a character event, the previous event did not have a followup char
		/// event and so this event will be ignored and the next char event
		/// will not be marked. Always call this function prior to intercepting
		/// input.
		/// </summary>
		/// <param name="event1">Event1.</param>
		static public void ConsumeMarkedCharEvent(Event event1)
		{
			if(Event.current.type == EventType.KeyDown)
			{
				if (consumeNextCharEvent && event1.keyCode == KeyCode.None)
				{
					event1.Use();
				}
				if (event1.type != EventType.Used)
				{
					consumeNextCharEvent = false;
				}
			}
		}
	}
}
