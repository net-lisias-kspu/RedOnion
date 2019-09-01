using RedOnion.UI.Components;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UUI = UnityEngine.UI;

namespace RedOnion.UI
{
	public class TextBox : Element
	{
		public InputField Core { get; private set; }
		public Label Label { get; private set; }
		protected BackgroundImage Image { get; private set; }

		public TextBox()
		{
			Image = GameObject.AddComponent<BackgroundImage>();
			Image.sprite = Skin.textField.normal.background;
			Image.type = UUI.Image.Type.Sliced;
			Core = GameObject.AddComponent<InputField>();
			Core.TextBox = this;
			Label = Add(new Label()
			{
				Anchors = Anchors.Fill,
				TextColor = Skin.textField.normal.textColor,
				TextAlign = TextAnchor.UpperLeft
			});
			Core.textComponent = Label.Core;
		}
		public TextBox(string text) : this() => Text = text;

		protected override void Dispose(bool disposing)
		{
			if (!disposing || GameObject == null)
				return;
			Core = null;
			Label.Dispose();
			Label = null;
			Image = null;
			base.Dispose(true);
		}

		public bool MultiLine
		{
			get => Core.multiLine;
			set => Core.lineType = value
				? UUI.InputField.LineType.MultiLineNewline
				: UUI.InputField.LineType.SingleLine;
		}

		public string Text
		{
			get => Core.text ?? "";
			set => Core.text = value ?? "";
		}
		public Color TextColor
		{
			get => Label.TextColor;
			set => Label.TextColor = value;
		}
		public TextAnchor TextAlign
		{
			get => Label.TextAlign;
			set => Label.TextAlign = value;
		}

		public int FontSize
		{
			get => Label.FontSize;
			set => Label.FontSize = value;
		}
		public FontStyle FontStyle
		{
			get => Label.FontStyle;
			set => Label.FontStyle = value;
		}

		public Event<string> Submitted
		{
			get => new Event<string>(Core.onEndEdit);
			set { }
		}
		public Event<string> Changed
		{
			get => new Event<string>(Core.onValueChanged);
			set { }
		}
		public Event<TextBox> Enter
		{
			get => new Event<TextBox>(Core.onSelected);
			set { }
		}
		public Event<TextBox> Exit
		{
			get => new Event<TextBox>(Core.onDeselected);
			set { }
		}

		public int CaretPosition
		{
			get => Core.caretPosition;
			set => Core.caretPosition = value;
		}
		public int SelectionStart
		{
			get => Core.selectionAnchorPosition;
			set => Core.selectionAnchorPosition = value;
		}
		public int SelectionEnd
		{
			get => Core.selectionFocusPosition;
			set => Core.selectionFocusPosition = value;
		}
		public int CharacterLimit
		{
			get => Core.characterLimit;
			set => Core.characterLimit = value;
		}
		public bool Focused
		{
			get => Core.isFocused;
			set
			{
				if (value) Core.ActivateInputField();
				else Core.DeactivateInputField();
			}
		}
		public bool ReadOnly
		{
			get => Core.readOnly;
			set => Core.readOnly = value;
		}
	}
}
