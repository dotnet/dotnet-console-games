<h1 align="center">
	Simon
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FSimon%2FSimon.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Simon%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord&logoColor=ffffff&color=7389D8" title="Go To Discord Server" alt="Discord"/></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Simon is a pattern memory game. The game will generate a random series of directional inputs, and you try to repeat the pattern. Every time you successfully repeat the pattern, the pattern will grow making it harder to remember. Get the pattern wrong at any time you lose.

```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝   ║
    ║       ║    ║       ║
    ║   ╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

Up (↑):
```cs
    
           ╔══════╗
           ║██████║
           ╚╗████╔╝
    ╔═══╗   ╚╗██╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝   ║
    ║       ║    ║       ║
    ║   ╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

Right (→):
```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝███║
    ║       ║    ║███████║
    ║   ╔═══╝╔══╗╚═══╗███║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

Down (↓):
```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝   ║
    ║       ║    ║       ║
    ║   ╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝██╚╗   ╚═══╝
           ╔╝████╚╗
           ║██████║
           ╚══════╝
```

Left (←):
```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║███╚═══╗╚══╝╔═══╝   ║
    ║███████║    ║       ║
    ║███╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

## Input
The **arrow keys (↑, ↓, ←, →)** are used to repeat the randomized pattern. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.
