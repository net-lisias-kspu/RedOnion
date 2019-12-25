﻿**Kerbalui** is a ui library designed for use with **LiveRepl**.

IT WAS NOT DESIGNED FOR OTHER PROJECTS. Many decisions were made for easy implementation in the moment. 

Part of the reason LiveRepl and Kerbalui are in different projects is because LiveRepl will probably someday be switched to use a different UI lib. 

Kerbalui was designed specifically to provide functionality for use with LiveRepl and I have not spent time to make it a general UI Lib. This philosophy applies somewhat: http://c2.com/xp/YouArentGonnaNeedIt.html

Part of the reason I have not tried to make it perfect is that [imgui](https://docs.unity3d.com/Manual/GUIScriptingGuide.html) is hard for me to work with.

**All elements must be constructed within the execution of the MonoBehaviour OnGUI update**. The reason is that certain objects that need to be initialized, like style, AFAIK require calls to `GUI.`, which must be called within the execution of OnGUI.

So, for example, `ScriptWindow`, the class implementing the window that shows up in **LiveRepl**, is constructed like this:

```
void OnGUI()
{
    if (scriptWindow==null)
    {
        scriptWindow=new ScriptWindow(title);
    }
```

And that `ScriptWindow` constructor calls all the other constructors of the child Elements. This may pause the game for a moment, depending on how big and complicated your UI initialization code is.

Is this ideal? No. But it simplifies a lot of aspects of the code and this library is just intended to implement LiveRepl. If it causes problems for a future use case of LiveRepl I will change it.

## General
When an element needs to be resized (based on changes in the contents or whatever) you set its public variable `needsResize` to true. It and all its children will be resized by calling SetRect(rect) where rect is the new rect that they should use.

An element can be disabled by setting its `Active` property to false. This will cause its `Update` to be disabled, as well as the Update of any children, and `Spacers` will allocate it zero space, leading to it being effectively hidden if it is in a Spacer.

## Types
All the following GUI classes extend Element.

- Controls represent a particular imgui control.

- Decorators represent an object which contains a Control and acts as a proxy for it, telling the child object what size to use and calling it's update. Decorators provide some sort of extra functionality that would be inconvenient to provide by extending a Control.

- Groups contain multiple controls and execute all of their Update's in an imgui `GUI.BeginGroup`...`GUI.EndGroup`

- Windows represent a top-level window in the ui system. They contain a title and a content area, which can be a Control, Decorator, or Group.

## Spacers
Spacers are defined in Kerbalui/Layout, and they are Groups that perform automatic layout for their child controls.
`VerticalSpacers` layout objects vertically and `HorizontalSpacers` lay them out horizontally.

With a spacer you add child elements with the following functions:
- AddFixed - This adds a child Control and reserves a fixed space for it
- AddMinSized - This adds a child Control and reserves a minimum space for it based on its content size.
- AddWeighted - This adds a child control and sets a weight for it.

MinSized in the context of a HorizontalSpacer, means only the minimum horizontal space the Element needs. The vertical space that the Element is allocated matches the HorizontalSpacer's. 

It works analogously for the VerticalSpacer.

Layout works by first reserving all the space needed for the fixed and minsized elements, and then dividing up the remaining space allocated to the spacer between the weighted elements based on their weights.

To fill a spot in a spacer with empty space, the Filler class can be used.

It is pretty easy to make a GUI out of nothing but vertical and horizontal spacers.

## Pitfalls
There are some pitfalls of imgui. Some have been fixed with the new version of Unity that KSP is now using.

One remaining one is that different sets of events are sent in Windows, as opposed to Linux (haven't tested on OSX).

So I have a special system for dealing with these events which I won't describe for now, but is the purpose of the `ConsumeAndMarkNextCharEvent` and `ConsumeMarkedCharEvent` calls in `Kerbalui.Util.GUILibUtil.cs`

