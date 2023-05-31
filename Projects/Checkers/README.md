<h1 align="center">
	Checkers
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games"><img src="../../.github/resources/github-repo-black.svg" alt="GitHub repo"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/"><img src="../../.github/resources/language-csharp.svg" alt="Language C#"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Checkers%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"></a>
	<a href="../../LICENSE"><img src="../../.github/resources/license-MIT-green.svg" alt="License"></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Checkers" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" alt="Play Now"></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

> **Note** This game was a *[Community Contribution](https://github.com/dotnet/dotnet-console-games/pull/40)!

Checkers is played on an 8x8 board between two sides commonly known as black
and white. The objective is simple - capture all your opponent's pieces. An
alternative way to win is to trap your opponent so that they have no valid
moves left.

Black starts first and players take it in turns to move their pieces forward
across the board diagonally. Should a piece reach the other side of the board
the piece becomes a king and can then move diagonally backwards as well as
forwards.

Pieces are captured by jumping over them diagonally. More than one enemy piece
can be captured in the same turn by the same piece. If you can capture a piece
you must capture a piece.

Moves are selected with the arrow keys. Use the [enter] button to select the
from and to squares. Invalid moves are ignored.

```
    ╔═══════════════════╗
  8 ║  . ◙ . ◙ . ◙ . ◙  ║ ○ = Black
  7 ║  ◙ . ◙ . ◙ . ◙ .  ║ ☺ = Black King
  6 ║  . ◙ . ◙ . . . ◙  ║ ◙ = White
  5 ║  . . . . . . ◙ .  ║ ☻ = White King
  4 ║  . . . ○ . . . .  ║
  3 ║  ○ . ○ . . . ○ .  ║ Taken:
  2 ║  . ○ . ○ . ○ . ○  ║ 0 x ◙
  1 ║  ○ . ○ . ○ . ○ .  ║ 0 x ○
    ╚═══════════════════╝
       A B C D E F G H
```

## Input

- `0`, `1`, `2`: select number of human players in menu
- `↑`, `↓`, `←`, `→`: move selection
- `enter`: confirm
- `escape`: cancel current move

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Checkers.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Checkers)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Checkers)
