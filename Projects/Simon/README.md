<h1 align="center">
	Simon
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Simon%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Simon" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

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

- `↑`, `↓`, `←`, `→`: push the buttons to repeat the pattern
- `escape`: exit game

> resizing the console window will cause the game to close

## Downloads

[win-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/win-x64/Simon.exe)

[linux-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/linux-x64/Simon)

[osx-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/osx-x64/Simon)
