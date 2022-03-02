<h1 align="center">
	Sliding Puzzle
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Sliding%20Puzzle%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Sliding Puzzle is a game where you start with a randomized NxN board that has one empty space in place of a tile. Tiles adjacent to the empty space may be slid into the empty space. You goal is to get all tiles in least-to_greatest order and the empty space in the lower right corner.

```
  ╔═════════════╗
  ║ 05 09 02 14 ║
  ║ 03 10    08 ║
  ║ 07 15 01 11 ║
  ║ 12 13 06 04 ║
  ╚═════════════╝
```

## Input

- `1`, `2`, and `3`: choose options in the menu
- [`↑`, `↓`, `←`, `→`] or [`W`, `A`, `S`, `D`]: slide tiles into the empty space
- `enter`: return to menu after solving a puzzle
- `escape`: return to menu or close game

## Dependencies

Don't forget these dependencies if you copy the code:

- [Towel](https://github.com/ZacharyPatten/Towel) nuget package _(referenced in [Sliding Puzzle.csproj](Sliding%20Puzzle.csproj))_

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Sliding%20Puzzle" alt="Play Now">
		<sub><img height="40"src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>
