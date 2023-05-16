<h1 align="center">
	Maze
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Maze%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Maze" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

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

- `↑`, `↓`, `←`, `→`: movement
- `escape`: exit game

## Notes

At the top of the **[source code](Program.cs)** you will see compiler directive(s):
- `#define MazeGenertorLoop`: Uncomment this directive and you can watch the code generate mazes infinitely inside a `while (true)` loop.
- `#define DebugRandomMazeGeneration`: Uncomment this directive and you can watch the maze generation algorithm step-by-step.
- `#define UsePrims`: Uncomment this directive to use an alternative algorithm for generating mazes.

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Maze.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Maze)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Maze)
