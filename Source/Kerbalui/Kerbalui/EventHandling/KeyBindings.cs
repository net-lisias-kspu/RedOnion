﻿using System;
using UnityEngine;
using System.Collections.Generic;
using Kerbalui.Util;

namespace Kerbalui.EventHandling
{
	public class KeyBindings : Dictionary<EventKey, Action>
	{
		public bool ExecuteAndConsumeIfMatched(Event event1)
		{
			if (event1.type == EventType.KeyDown)
			{
				if (TryGetValue(GetEventKey(event1), out Action action))
				{
					action.Invoke();
					GUILibUtil.ConsumeAndMarkNextCharEvent(event1);
					return true;
				}
			}

			return false;
		}

		EventKey GetEventKey(Event event1)
		{
			return new EventKey(event1.keyCode, event1.control, event1.shift, event1.alt);
		}
	}
}
