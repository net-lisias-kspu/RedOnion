using System;
using LiveRepl.UI.Base;
using UnityEngine;

namespace LiveRepl.UI.Base
{
	/// <summary>
	/// A Control that contains content.
	/// </summary>
	public abstract class ContentControl:Control
	{
		public GUIContent content=new GUIContent("");
		public GUIStyle style;

		/// <summary>
		/// Returns default style if style is null
		/// </summary>
		/// <value>The style.</value>
		public GUIStyle StyleOrDefault
		{
			get
			{
				if (style==null)
				{
					return DefaultStyle();
				}
				return style;
			}
		}

		/// <summary>
		/// Subclasses inherit this to provide default styles for the Style property to return
		/// by default for their control.
		/// </summary>
		/// <returns>The style.</returns>
		public abstract GUIStyle DefaultStyle();

		public void InitStyle()
		{
			if (style==null)
			{
				DefaultStyle();
			}
		}
	}
}