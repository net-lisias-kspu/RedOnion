﻿## Red Onion UI

This is an attempt to create real Unity UI library (not IMGUI) that is relatively easy to use.
It should be similar to WinForms with top-down coordinate system (unlike Unity where Y is up).

[Generated documentation here](../RedOnion.KSP/Namespaces/UI.md)

## Element

`UI.Element` is the base class for all UI elements / controls. It manages `UnityEngine.GameObject`
and its `RectTransform`, provides layout settings and basic `AddElement` to add child elements.

* `Name` - optional name of the element (see Panel below).
* `Parent` - parent element (inside which this element is)
* `MinWidth` - minimal width if set to non-negative number (reads `float.NaN` otherwise,
  which means that the minimal width is not set - assigning negative number will have same result).
* `MinHeight` - minimal height (same negative/`float.NaN` logic as above and for many below).
* `PreferWidth` and `PreferHeight` - preferred width/height if set (the layout will use this if possible).
* `FlexWidth` and `FlexHeight` - flexible width/height if inside horizontal/vertical layout.
* `Layout` - `Layout.None`, `Layout.Horizontal` or `Layout.Vertical` for now, this property is protected.

## Panel

`UI.Panel` is the basis for more complex layout. You will usually nest few panels with alternating
`Layout.Horizontal` and `Layout.Vertical` (do not forget to assign its `Layout` property,
it is set to `Layout.None` when the panel is created).

* `Layout` - public property here, works as in `Element`, use `Layout.Horizontal` or `Layout.Vertical`.
* `Color` - color of the panel (background, transparent by default).
* `Texture` - texture of the panel (background, no texture by default).
* `LayoutPadding` - the combined inner padding and spacing (6 floats in total, all set to `3f` by default).
* `InnerPadding` (4 floats), `InnerSpacing` (2), `Padding` (1) and `Spacing` (1) access the above.
* `ChildAnchors` - this currently controls layout's `childAlignment` and
  `childForceExpandWidth/Height`, but plan is to use custom `LayoutComponent`.
  You can try `Anchors.Fill` (to make all inner elements fill their cell)
  or `Anchors.MiddleLeft/MiddleCenter/UpperLeft...`

**Methods**
* `Panel(string name = null)` - create the panel, optionally providing its name.
* `Element Add(Element element)` - add new element, returns the argument.
* `Element Remove(Element element)` - remove the element, returns the argument.
* `void Add(params Element[] elements)` - add multiple elements.
* `void Remove(params Element[] elements)` - remove multiple elements.
* `Element this[string name]` - find element by name (direct children only).
* `IEnumerator<Element> GetEnumerator()` - enumerate elements (`foreach`).

## Window

`UI.Window` is the root element that will host all inner elements.
It integrates frame with header (title and close button) and content panel.
It is the only UI element that is not derived from `UI.Element` (which may change)
and redirects most methods and properties to its content panel
(`Parent` of contained elements currently point here, which may change),
some to the frame. It is draggable, has `CanvasGroup` and `ContentSizeFitter`
initially set to `FitMode.PreferredSize` for both height and width,
which means it will autosize to fit the content (with minimal W:160,H:120 by default).

* `Name` - optional name of the window (redirected to `Frame.Name`).
* `Title` - the text in the header/title of the window.
* `Alpha` - alpha/transparency for the whole window (1f = solid, 0f = invisible, .9f by default).
* `Position`, `X` and `Y` - position of the center of the window relative to the center of the screen,
  Y is inverted from Unity, so that positive values move down (negative move up).
  It uses `Frame.RectTransform.anchoredPosition`, so,
  if you change the pivot/anchors, you change this as well.
* `Size`, `Width` and `Height` - this not only changes the size of the window,
  but also switchest the `ContentSizeFitter` between `Unconstrained` and `PreferredSize` modes.
  `Unconstrained` is selected if the value is at least current `MinWidth`/`MinHeight`,
  `PreferredSize` otherwise (when you e.g. set it to zero or even negative, the
  `MinWidth`/`MinHeight` will be used instead).
* `MinWidth` and `MinHeight` - minimal width and height (160x120 by default, 80x80 is total minimum).
* `FrameColor`, `FrameTexture`, `HeaderColor`, `HeaderTexture`, `TitleColor`, `ContentColor`, `ContentTexture`
* `Layout`, `LayoutPadding`, `InnerPadding`, `InnerSpacing`, `Padding`, `Spacing` - content panel.

**Events**
* `Closed` - invoked by the close button and/or `Close()` method

**Methods**
* `Window(Layout layout)` - create the window with desired layout of the content panel.
* `Window(string name = null, Layout layout = Layout.Vertical)` - the default constructor.
* `Dispose()` - close the window permanently, dispose all resources.
* `Show()` - show the window (it is created visible now which we may decide to change).
* `Hide()` - hide the window.
* `Close()` - calls `Hide()` and then `Closed` event, the close button uses this.

## Label

`UI.Label` is simple line of text used to label other elements.

* `Text` - text of the label.
* `TextColor` - color of the text (foreground, `Color.black` by default).
* `TextAlign` - align of the text (`TextAnchor.MiddleLeft` by default).
* `FontSize` - the size of the font (`14` by default).
* `FontStyle` - font style (`FontStyle.Normal` by default).

## Button

`UI.Button` is there to perform actions when clicked.

* `Text` - optional text on the button.
* `IconTexture` - optional icon on the button.
* `LayoutPadding`, `InnerPadding`, `InnerSpacing`, `Padding`, `Spacing` - the usual.

**Events**
* `Click` - invoked when clicked.

## TextBox

`UI.TextBox` is there to get some input from the user.

* `Text`, `TextColor`, `TextAlign`, `FontSize`, `FontStyle` - as in `Label`.
* `Multiline` - multi/single-line switch (single line by default).
* `ReadOnly`, `Focused`, `CaretPosition`, `SelectionStart`, `SelectionEnd`, `CharacterLimit`

**Events**
* `Changed` - the text got changed.
* `Submitted` - the text got submitted (ENTER).

**WIP ;)**
