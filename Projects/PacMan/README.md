<h1 align="center">
	PacMan
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord&logoColor=ffffff&color=7389D8" title="Go To Discord Server" alt="Discord"/></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

PacMan is a game where you eat dots. Eat as many dots as you can while avoiding the Ghosts. If the ghosts catch you, you die.

```
    ╔═══════════════════╦═══════════════════╗
    ║ · · · · · · · · · ║ · · · · · · · · · ║
    ║ · ╔═╗ · ╔═════╗ · ║ · ╔═════╗ · ╔═╗ · ║
    ║ + ╚═╝ · ╚═════╝ · ╨ · ╚═════╝ · ╚═╝ + ║
    ║ · · · · · · · · · · · · · · · · · · · ║
    ║ · ═══ · ╥ · ══════╦══════ · ╥ · ═══ · ║
    ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
    ╚═════╗ · ╠══════   ╨   ══════╣ · ╔═════╝
          ║ · ║                   ║ · ║
    ══════╝ · ╨   ╔════---════╗   ╨ · ╚══════
            ·     ║ █ █   █ █ ║     ·        
    ══════╗ · ╥   ║           ║   ╥ · ╔══════
          ║ · ║   ╚═══════════╝   ║ · ║
          ║ · ║       READY       ║ · ║
    ╔═════╝ · ╨   ══════╦══════   ╨ · ╚═════╗
    ║ · · · · · · · · · ║ · · · · · · · · · ║
    ║ · ══╗ · ═══════ · ╨ · ═══════ · ╔══ · ║
    ║ + · ║ · · · · · · █ · · · · · · ║ · + ║
    ╠══ · ╨ · ╥ · ══════╦══════ · ╥ · ╨ · ══╣
    ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
    ║ · ══════╩══════ · ╨ · ══════╩══════ · ║
    ║ · · · · · · · · · · · · · · · · · · · ║
    ╚═══════════════════════════════════════╝
```

## Input

The **arrow keys (↑, ↓, ←, →)** are used to move. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.

## Dependencies

Don't forget these dependencies if you copy the code:

- [Towel](https://github.com/ZacharyPatten/Towel) nuget package _(referenced in [PacMan.csproj](PacMan.csproj))_
