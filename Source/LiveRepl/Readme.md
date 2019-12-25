[![Dynawing-Launch-Labelled.jpg](https://i.postimg.cc/5N92LpCd/Dynawing-Launch-Labelled.jpg)](https://postimg.cc/Lh7SMtDN)

**LiveRepl** is the main user interface for the project. It is used to write, load, and evaluate scripts that interact with KSP.

**LiveRepl** is shown or hidden by clicking the LiveRepl Icon on the toolbar (which is just a small icon with the words "Live Repl")

**LiveRepl** will intercept keyboard button presses when you have the mouse over the window.

## **LiveRepl** consists of:
- An **Editor** for saving, loading, writing, and executing files.
- A **Repl Input Area** for executing little snippets of code (for example, for setting or evaluating global variables). `enter` submits the code to be executed. `shift+enter` creates a new line.
- An **Output Area** for displaying the results of executing code in the Editor or Repl Input Area.
- A **Font Selector** which shows the current font used in the previous 3 parts. The Font Selector can be clicked to show a list of fonts in the Completion Area, which can be clicked to select a new font.
- A **Scriptname Input area** which can be clicked to select a new script to load from a list in the scripts directory. Or one can type a new name for a script and click the `Save` button to create a new file with that name.
- A **Completion Area** which shows intellisense results for code when the editor or Repl Input Area is selected, a list of fonts to select from when the Font Selector is selected, or a list of files when the Scriptname Input Area is selected.
- A bunch of buttons to perform various actions.

## Buttons:
- `Clear Repl` for clearing the contents of the output area which is right below the button.
- `Show Hotkeys` will print all the hotkeys that are available into the **Output Area**.
- `<<` will hide the output section of **LiveRepl**.
- `>>` will hide the editor section of **LiveRepl**.
- `Run` will execute the code that is in the editor, and save it to the filename shown in the **Scriptname Input Area**. If the filename is blank, it will save it to a new file named `untitled.X` where `X` is either `lua` or `ros`, depending on which script engine is selected.
- `Terminate` will cancel execution of a currently running script. Most buttons and functionality are disabled when scripts are running, but the `Terminate` button is still functional.
- `Reset Engine` will set all the global variables in the selected script engine to their defaults.
- `Reset Autopilot` will disable the autopilot without resetting the engine.
- `Lua` will select `Kerbalua` as the current script engine.
- `ROS` will select `RedOnionScript` as the current script engine.
- `Save` will save the contents of the editor to the filename shown in the **Scriptname Input Area**. If the filename is blank, it will save it to a new file named `untitled.X` where `X` is either `lua` or `ros`, depending on which script engine is selected.
- `Load` will load, into the editor, the file with the filename that is written in the **Scriptname Input Area**.

## Hotkeys
There are a number of hotkeys for using **LiveRepl** without taking your hands off the keyboard. These can be shown by clicking the `Show Hotkeys` button. You can navigate between the Repl Input Area, Editor, Scriptname Input Area, and Completion Area and also move around within these parts using hotkeys.

## Limitations
There are some limitations with **LiveRepl** which may be fixed over time.
- All versions prior to 0.4.7 had a bug where all the Input Control Locks where cleared by **LiveRepl** whenever the mouse left the window bounds. This bug could allow some user input when it is not appropriate, for example, when time warping.
- Only one **LiveRepl** can be open at a time. We are intending to eventually in create a more flexible system that allows multiple **LiveRepl**'s and multiple scripts running at a time.
- All scripts are in one big directory. We are planning on eventually having some sort of file navigation mode and also allowing this editor to be used for general editing of files inside the KSP directory, not just user scripts.
- LiveRepl cannot really handle large text files. And by large, I only mean about 19kb (from my testing)