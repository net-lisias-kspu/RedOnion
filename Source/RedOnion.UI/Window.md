## Window

Window is the root of all UI elements.
Make sure to keep reference to it and dispose it when you are done with it.
It may get garbage-collected otherwise, but that can take time and is rather backup measure.


**Constructors:**
- `Window()` - Create new window (immediately visible).
- `Window()`: title string
  - Create new window with a title (immediately visible).
- `Window()`: title string, layout [Layout](Layout.md)
  - Create new window with a title and defined layout (immediately visible).
- `Window()`: layout [Layout](Layout.md), title string
  - Create new window with defined layout and a title (immediately visible).
- `Window()`: layout [Layout](Layout.md)
  - Create new window with defined layout (immediately visible).
- `Window()`: layout [Layout](Layout.md), visible bool
  - Create new window with defined layout and selected visibility.
- `Window()`: visible bool
  - Create new window with selected visibility.
- `Window()`: visible bool, title string
  - Create new window with selected visibility and title.
- `Window()`: visible bool, title string, layout [Layout](Layout.md)
  - Create new window with selected visibility, title and layout.
- `Window()`: visible bool, layout [Layout](Layout.md), title string
  - Create new window with selected visibility, layout and title.
- `Window()`: visible bool, layout [Layout](Layout.md)
  - Create new window with selected visibility and layout.

**Instance Properties:**
- `Active`: bool - Window is set to be visible/active. [`GameObject.activeSelf`](https://docs.unity3d.com/ScriptReference/GameObject-activeSelf.html), [`GameObject.SetActive`](https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html)
- `Visible`: bool - Window is visible (and all parents are). [`GameObject.activeInHierarchy`](https://docs.unity3d.com/ScriptReference/GameObject-activeInHierarchy.html), [`GameObject.SetActive`](https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html)
- `Title`: string - Text in the title of the window.
- `Alpha`: float - Transparency / alpha channel for the whole window (0.0 .. 1.0). Zero means invisible, one for opaque.
- `MinWidth`: float - Minimal width of the window. Default is 160, cannot be set below 80.
- `MinHeight`: float - Minimal height of the window. Default is 120, cannot be set below 40.
- `Position`: Vector2 - Position of the anchor of the window. (The anchor is by default in the center of the window and the window is placed in the center of the screen, which is \[0,0\].) Note that Y-coordinate is inverted from Unity to be top-down (where Unity is bottom-up).
- `X`: float - The X-coordinate of the position of the anchor of the window. (The anchor is by default in the center of the window and 0 is in the center of the screen - you may use `unity.screen.width` to get the limits.)
- `Y`: float - The Y-coordinate of the position of the anchor of the window (inverted from Unity to be top-down, not bottom-up). (The anchor is by default in the center of the window and 0 is in the center of the screen - you may use `unity.screen.height` to get the limits.)
- `Size`: Vector2 - The size of the window (or rather `RectTransform.sizeDelta`). Setting this value above curent `MinWidth`/`MinHeight` sets the integrated `ContentSizeFitter` to `Unconstrained`, which makes the window size be exactly what was specified. Set to `PreferredSize` otherwise, to use the contained elements for size calculations.
- `Width`: float - `Size.x`
- `Height`: float - `Size.y`
- `FrameColor`: Color - Background color of the frame.
- `HeaderColor`: Color - Background color of the header/title.
- `TitleColor`: Color - Foreground color of the title.
- `ContentColor`: Color - Background color of the content panel.
- `Layout`: [Layout](Layout.md) - Layout (how child elements of content panel are placed).
- `LayoutPadding`: [LayoutPadding](LayoutPadding.md) - The combined inner padding and spacing (6 floats in total, all set to `3f` by default).
- `ChildAnchors`: [Anchors](Anchors.md) - This currently controls layout's `childAlignment` and
`childForceExpandWidth/Height`, but plan is to use custom `LayoutComponent`.
You can try `Anchors.Fill` (to make all inner elements fill their cell)
or `Anchors.MiddleLeft/MiddleCenter/UpperLeft...`
- `InnerPadding`: [Padding](Padding.md) - Inner padding (border left empty inside this panel).
- `InnerSpacing`: Vector2 - Inner spacing (between elements).
- `Padding`: float - `InnerPadding.All` - one number if all are the same, or NaN.
- `Spacing`: float - Spacing - one number if both are the same, or NaN.

**Instance Events:**
- `Closed`: Action\[Window\] - The window was closed (by the 'X' button or by call to `Close`).

**Instance Methods:**
- `Dispose()`: void - Dispose the window (and destroy its game object). Call this when you no longer need the window (e.g. from `Closed` event).
- `Show()`: void - Show the window (`Visible = true`).
- `Hide()`: void - Hide the window (`Visible = false`).
- `Close()`: void - Hide and close the window (invokes `Closed` event).
- `Add()`: [Element](Element.md), element [Element](Element.md)
  - Add new element onto this panel.
- `Remove()`: [Element](Element.md), element [Element](Element.md)
  - Remove element from this panel.
- `AddPanel()`: [Panel](Panel.md), layout [Layout](Layout.md)
  - Add new panel with specified layout.
- `AddHorizontal()`: [Panel](Panel.md) - Add new panel with horizontal layout.
- `AddVertical()`: [Panel](Panel.md) - Add new panel with vertical layout.
- `AddLabel()`: [Label](Label.md), text string
  - Add new label with specified text.
- `AddTextBox()`: [TextBox](TextBox.md), text string
  - Add new label with specified text.
- `AddButton()`: [Button](Button.md), text string
  - Add new button with specified text.
- `AddButton()`: [Button](Button.md), text string, click Action\[[Button](Button.md)\]
  - Add new button with specified text and click-action.
- `AddToggle()`: [Button](Button.md), text string
  - Add new toggle-button with specified text.
- `AddToggle()`: [Button](Button.md), text string, click Action\[[Button](Button.md)\]
  - Add new toggle-button with specified text and click-action.
- `AddExclusive()`: [Button](Button.md), text string
  - Add new exclusive toggle-button (radio button) with specified text.
- `AddExclusive()`: [Button](Button.md), text string, click Action\[[Button](Button.md)\]
  - Add new exclusive toggle-button with specified text and click-action.
