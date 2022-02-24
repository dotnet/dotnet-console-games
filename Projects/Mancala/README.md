<h1 align="center">
	Mancala
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/dotnet-badge.svg title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Mancala%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Mancala is a game with seeds, pits, and stores. On your turn you will choose one of your pits that contains seeds, and 
those seeds will be distributed in a counter-clockwise manor to all pits and your store until they run out. Your goal is
so have the most seeds in your store at the end of the game.

Special Rules:
- While distributing seeds, if the last seed goes into the player's store the player gets to move again before the other.
- While distrubuting seeds, if the last seed goes into an empty pit on the side of the player who is currently moving and
  the mirror pit on the opponent's side is not empty, all of the seeds in that pit and the mirror pit on the opponent's 
  side will be added to the moving player's pit.

```

  Mancala

  ╔══════════════════════════════════╗
  ║ |  |[ 8][ 0][ 1][ 0][ 6][ 7]|  | ║
  ║ |  |                        |  | ║
  ║ | 3|                        | 4| ║
  ║ |  |             \/         |  | ║
  ║ |  |[ 7][ 1][ 0][ 7][ 2][ 2]|  | ║
  ╚══════════════════════════════════╝

```

## Input

- **left and right arrow (←, →) or (a, d) keys**: move selected pit
- **enter**: confirm
- **escape**: close

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Mancala" alt="Play Now">
		<sub><img height="40"src="https://raw.githubusercontent.com/ZacharyPatten/dotnet-console-games/main/.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>
