<h1 align="center">
	Sudoku
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Sudoku%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Sudoku is a randomly generated number puzzle game.The goal is to fill in the entire board with the numbers 1 through 9. However, you cannot duplicate the values within the same row, column, or 3x3 square.

```
╔═══════╦═══════╦═══════╗
║ 7 6 2 ║ 9 1 5 ║ 8 4 3 ║
║ 5 4 3 ║ 7 8 6 ║ 2 1 9 ║
║ 9 1 8 ║ 2 3 4 ║ 5 7 6 ║
╠═══════╬═══════╬═══════╣
║ 4 3 1 ║ 5 9 8 ║ 7 6 2 ║
║ 6 2 5 ║ 4 7 3 ║ 1 9 8 ║
║ 8 7 9 ║ 1 6 2 ║ 3 5 4 ║
╠═══════╬═══════╬═══════╣
║ 3 9 6 ║ 8 5 1 ║ 4 2 7 ║
║ 1 8 4 ║ 6 2 7 ║ 9 3 5 ║
║ 2 5 7 ║ 3 4 9 ║ 6 8 1 ║
╚═══════╩═══════╩═══════╝
```

## Input

The **arrow keys(↑, ↓, ←, →)** are used to change the selected cell.The **1 - 9 number keys** are used to insert a value into the current cell(if the move is valid). The **\[delete\] and \[backspace\] keys** will remove values from the board. The **\[end\] key** will generate a new puzzle if you get stuck. The **\[enter\] key** will generate a new puzzle after you win.The **\[escape\] key** will exit the game.

## Notes

At the top of the **[source code](Program.cs)** you will see compiler directive(s):
- `#define DebugAlgorithm`: Uncomment this directive and you can watch the sudoku generation algorithm step-by-step.

## Dependencies

Don't forget these dependencies if you copy the code:

- [Towel](https://github.com/ZacharyPatten/Towel) nuget package _(referenced in [Sudoku.csproj](Sudoku.csproj))_

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Sudoku" alt="Play Now">
		<sub><img height="40"src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>
