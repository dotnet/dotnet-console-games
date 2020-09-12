<h1 align="center">
	2048
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2F2048%2F2048.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/2048%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

2048 is a sliding number puzzle game. Slide all the tiles on the board in one direction: up, down, left, or right. If two like numbers collide, they will combine into a higher value tile. The goal is to get a "2048" value tile on the board, but you can even keep playing after that to try to get a high score.

```
2048

╔════════════════════════════════╗
║                                ║
║    16       2      32       4  ║
║                                ║
║     4      32       8       2  ║
║                                ║
║     8       4               4  ║
║                                ║
║     4                          ║
║                                ║
╚════════════════════════════════╝
Score: 774
```

## Input

The **arrow keys (↑, ↓, ←, →)** are used to slide the tiles on the board. The **[end] key** will end the current game and start a new one. The **escape** key may be used to close the game.