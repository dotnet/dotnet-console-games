<h1 align="center">
	Memory
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FMaze%2FMaze.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Maze%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord&logoColor=ffffff&color=7389D8" title="Go To Discord Server" alt="Discord"/></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Memory is a game where you flip over cards and try to find all the matches. Cards will only stay flipped over when you find their match. Flip over all the cards and you win.

```
 Memory


   ██ ██ ██ ██ ██ 10 21 12 ██ ██

   ██ ██ ██ ██ ██ ██ ██ 12 15 ██

   09 01 ██ 10 ██ ██ 21 ██ ██ ██

   ██ ██ ██ 09 ██ ██ ██ ██ 05 ██

   ██ 01 ██ ██ ██ ██ 05 15 ██ ██
```

## Input

The **arrow keys (↑, ↓, ←, →)** are used to select a card. The **enter** key is used to confirm a selection and acknoledge prompts. The **escape** key may be used to close the game at any time.

## Dependencies

Don't forget these dependencies if you copy the code:

- [Towel](https://github.com/ZacharyPatten/Towel) nuget package _(referenced in [Memory.csproj](Memory.csproj))_
