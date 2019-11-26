[![Dynawing-Launch.jpg](https://i.postimg.cc/CMskNBBk/Dynawing-Launch.jpg)](https://postimg.cc/HjjrWLtk)

# RedOnion and Kerbalua

A joint effort of Evan Dickinson and Lukáš Fireš to create
unrestricted scripted environment inside Kerbal Space Program
for all players and even modders wishing to control the game,
vessels, anything, with own script.

## Current Features:
1. Repl/Editor with intellisense.
1. Scripting engines that can interact with Classes from any loaded CLR assembly. 
1. Lua scripting language called Kerbalua built on MoonSharp.
1. Powerful custom language/engine (RedOnionScript) built by Lukáš Fireš.
1. API's for doing things more easily or more safely, including Autopilot API, UI api, and more.

## Goals:
1. Provide ability to produce mods in Kerbalua/RedOnionScript and modify them without restarting the game.
1. Provide ability to debug mod code/scripts without restarting the game.
1. Provide ability to write user scripts, like automatic control of your ship.
1. Provide ability to interact with live game objects in a repl.
1. Provide powerful general editing capabilities inside KSP
1. Provide powerful API for modmakers/Users
1. Help document the KSP API for modmakers.

License: [MIT](https://github.com/evandisoft/RedOnion/blob/master/LICENSE)

Github: [here](https://github.com/evandisoft/RedOnion)

Docs: [here](https://evandisoft.github.io/RedOnion)

**Warning: Do not use scripts from untrusted sources!**
This is not a sandbox, any script has the power to do almost anything a modder could do in C#.
We plan to eventually implement a switch to limit the power
(disable what is marked `[Unsafe]` in the code),
but our goal now is to:

- Allow users to do whatever they wish to do to the game environment to have fun.
- Help modders develop and debug their mods using this mod.
- Help anybody to explore KSP API, their own or other's mods
  (read the license of each mod, we expose only `public` members directly).

## Releases

Your scripts are in GameData/RedOnion/Scripts. **I'm not sure whether CKAN deletes and replaces the entire RedOnion directory. Save your scripts somewhere else before updating to a new version, to be safe**. Then copy back in the ones you want to use. (If the example programs don't run, check out that section in [TroubleShooting](https://evandisoft.github.io/RedOnion/TroubleShooting))

Latest Release (0.4.0), available on:
- [CKAN](https://github.com/KSP-CKAN/CKAN)
- [Spacedock](https://spacedock.info/mod/2116/Red%20Onion)
- [CurseForge](https://www.curseforge.com/kerbal/ksp-mods/redonion)

ChangeLog: [here](https://evandisoft.github.io/RedOnion/ChangeLog)

## Documentation

- [Scripting](https://evandisoft.github.io/RedOnion/ScriptingReadme) - Documentation related to scripting.

- [LiveRepl](https://evandisoft.github.io/RedOnion/LiveRepl/Readme) - The main user interface for the project. Where scripts can be loaded, written, and executed.

- [Troubleshooting](https://evandisoft.github.io/RedOnion/TroubleShooting) - Some possible issues and solutions.

- [Development](https://evandisoft.github.io/RedOnion/DevelopmentReadme) - Main page for development documentation (project structure, implementation explanations, etc).

## Videos are listed [here](https://evandisoft.github.io/RedOnion/Videos)

## Upcoming Features

- [Next Release](https://evandisoft.github.io/RedOnion/ChangeLog#next-release)

- [Planned](https://evandisoft.github.io/RedOnion/ChangeLog#planned-features)

## Feedback

All feedback is appreciated. Feedback can be left on our forum [thread](https://forum.kerbalspaceprogram.com/index.php?/topic/189983-18x-redonion-unrestricted-in-game-scripting-v-040/), as a new [issue](https://github.com/evandisoft/RedOnion/issues), or as a comment on any of our [videos](Videos.md).

## Contributing

We welcome people of all skill levels to contribute or give feedback, ask questions, etc.

Here is more information about [contributing](https://evandisoft.github.io/RedOnion/Contributing).