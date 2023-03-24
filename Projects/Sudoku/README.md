<h1 align="center">
	Sudoku
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Sudoku%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Sudoku" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

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

- `↑`, `↓`, `←`, `→`: change the selected cell
- `1`, `2`, `3`, `4`, `5`, `6`, `7`, `8`, `9`: insert a value into the current cell (if the move is valid)
- `delete`, `backspace`: remove values from the board
- `end`: generate a new puzzle
- `enter`: confirm
- `escape`: exit game

## Notes

At the top of the **[source code](Program.cs)** you will see compiler directive(s):
- `#define DebugAlgorithm`: Uncomment this directive and you can watch the sudoku generation algorithm step-by-step.

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Sudoku.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Sudoku)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Sudoku)
