﻿using UnityEngine;
using System.Collections.Generic;
using System;
using Kerbalui.EventHandling;
using Kerbalui.Controls.Abstract;
using Kerbalui.Decorators;
using Kerbalui.Controls;

namespace LiveRepl.UI.EditorParts 
{
    public class Editor:EditingAreaScroller
    {
		public EditorGroup editorGroup;

		/// <summary>
		/// These bindings intentionally shadow the base class bindings.
		/// </summary>
		public KeyBindings keybindings = new KeyBindings();

		public Editor(EditorGroup editorGroup) : base(new EditingArea(new TextArea()))
		{
			this.editorGroup=editorGroup;
			//TODO: Define keybindings here.
		}

		protected override void DecoratorUpdate()
		{
			if (HasFocus()) 
			{
				keybindings.ExecuteAndConsumeIfMatched(Event.current);
			} 

			base.DecoratorUpdate();

			editorGroup.editorStatusLabel.UpdateCursorInfo(editingArea.LineNumber, editingArea.ColumnNumber);

			if (editingArea.ReceivedInput)
			{
				editorGroup.fileIOGroup.editorChangesIndicator.Changed();
				editorGroup.needsResize=true;
			}
		}
	}
}
