<h1 align="center">
	Maze
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FMaze%2FMaze.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Maze%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Maze is a pretty self explanatory game. Solve the randomly generated maze. The maze size if coded to be 8x20. You always start in the top left and the end is always in the bottom right.

```
████████████████████████████████████████████████████████████
█S██    ██ ██          ██ ██    ██       ██       ██ ██    █
█ ██ █████ █████ █████ ██ ██ ██ ██ █████ ██ ██ ██ ██ ██ ████
█ ██ █████ █████ █████ ██ ██ ██ ██ █████ ██ ██ ██ ██ ██ ████
█       ██    ██    ██       ██ ██    ██ ██ ██ ██    ██    █
█ █████ ██ ████████ ██ ████████ ██ ██ ██ ██ ████████ ██ ██ █
█ █████ ██ ████████ ██ ████████ ██ ██ ██ ██ ████████ ██ ██ █
█    ██ ██    ██ ██ ██       ██ ██ ██ ██ ██    ██       ██ █
█ ████████ ██ ██ ██ ██████████████ █████ ██ ██ ███████████ █
█ ████████ ██ ██ ██ ██████████████ █████ ██ ██ ███████████ █
█       ██ ██    ██                ██ ██    ██       ██ ██ █
█ █████ ██ █████ ████████ █████ █████ █████████████████ ██ █
█ █████ ██ █████ ████████ █████ █████ █████████████████ ██ █
█    ██       ██ ██ ██       ██    ██    ██ ██          ██ █
████ ██████████████ ██ ███████████ ██ ██ ██ ██ ████████ ██ █
████ ██████████████ ██ ███████████ ██ ██ ██ ██ ████████ ██ █
█    ██    ██    ██ ██    ██ ██ ██ ██ ██ ██    ██    ██    █
█ ██ █████ ██ ██ ██ █████ ██ ██ █████ ██ █████ ██ ██ ███████
█ ██ █████ ██ ██ ██ █████ ██ ██ █████ ██ █████ ██ ██ ███████
█ ██    ██    ██ ██    ██       ██ ██ ██       ██ ██       █
████ █████ █████ ██ ███████████ ██ ██ █████ █████ ██ █████ █
████ █████ █████ ██ ███████████ ██ ██ █████ █████ ██ █████ █
█          ██                      ██    ██       ██    ██E█
████████████████████████████████████████████████████████████
```

## Input

The **arrow keys (↑, ↓, ←, →)** are used to change the direction you are moving. The **escape** key may be used to close the game at any time.

## Notes

At the top of the **[source code](Program.cs)** you will see compiler directive(s):
- `#define MazeGenertorLoop`: Uncomment this directive and you can watch the code generate mazes infinitely inside a `while (true)` loop.
- `#define DebugRandomMazeGeneration`: Uncomment this directive and you can watch the maze generation algorithm step-by-step.
- `#define UsePrims`: Uncomment this directive to use an alternative algorithm for generating mazes.

## Dependencies

Don't forget these dependencies if you copy the code:

- [Towel](https://github.com/ZacharyPatten/Towel) nuget package _(referenced in [Maze.csproj](Maze.csproj))_
