﻿using LiveRepl.UI.Base;
using LiveRepl.UI.Controls;
using LiveRepl.UI.Decorators;
using UnityEngine;

namespace LiveRepl.UI.CustomParts
{
	/// <summary>
	/// The Group that holds the Editor and related functionality.
	/// </summary>
	public class EditorGroup : Group
	{
		public ContentGroup contentGroup;

		public FileIOGroup fileIOGroup;
		public EditingArea editingArea;

		public EditorGroup(ContentGroup contentGroup)
		{
			this.contentGroup=contentGroup;

			RegisterForUpdate(fileIOGroup=new FileIOGroup(this));
			RegisterForUpdate(editingArea=new EditingArea(new TextArea()));
		}

		public override void Update()
		{
			if (Event.current.type==EventType.KeyDown)
			{
				SetRect(rect);
			}
			base.Update();
		}

		protected override void SetChildRects()
		{
			fileIOGroup.SetRect(new Rect(0, 0, rect.width, 20));
			//TODO: TabsGroup
			editingArea.SetRect(new Rect(0, fileIOGroup.rect.height, rect.width, rect.height-fileIOGroup.rect.height));
			//TODO: EditorStatusGroup
		}
	}
}