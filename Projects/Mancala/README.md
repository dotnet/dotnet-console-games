<h1 align="center">
	Mancala
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Mancala%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Mancala" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Mancala is a game with seeds, pits, and stores. On your turn you will choose one of your pits that contains seeds, and 
those seeds will be distributed in a counter-clockwise manor to all pits and your store until they run out. Your goal is
so have the most seeds in your store at the end of the game.

Special Rules:
- While distributing seeds, if the last seed goes into the moving player's store the player gets to move again.
- While distrubuting seeds, if the last seed goes into an empty pit on the moving player's side and
  the mirror pit on the opponent's side is not empty, the seeds in both pits will be added to the moving player's store.

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

- `←`, `→`, `A`, `D`: move selected pit
- `enter`: confirm
- `escape`: exit game

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Mancala.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Mancala)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Mancala)
