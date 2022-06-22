<h1 align="center">
	Checkers
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Checkers%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

> **Note** This game was a *[Community Contribution](https://github.com/ZacharyPatten/dotnet-console-games/pull/40)!

Checkers is a classic 1v1 board game where you try to take all of your opponent's pieces. You may move pieces diagonally, and you may take your opponent's piece by jumping over their pieces diagonally. You may jump multiple pieces in a single move. You may not move a piece onto a tile that already has a piece on it.

```
 ╔═══════════════════╗
8║  . ◙ . ◙ . ◙ . ◙  ║ ○ = Black
7║  ◙ . ◙ . ◙ . ◙ .  ║ ☺ = Black King
6║  . ◙ . ◙ . . . ◙  ║ ◙ = White
5║  . . . . . . ◙ .  ║ ☻ = White King
4║  . . . ○ . . . .  ║
3║  ○ . ○ . . . ○ .  ║ Taken:
2║  . ○ . ○ . ○ . ○  ║ 0 x ◙
1║  ○ . ○ . ○ . ○ .  ║ 0 x ○
 ╚═══════════════════╝
    A B C D E F G H
```

## Input

- `0`, `1`, `2`: select number of human players in menu
- `↑`, `↓`, `←`, `→`: move selection
- `enter`: confirm
- `escape`: cancel current move

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Checkers" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>