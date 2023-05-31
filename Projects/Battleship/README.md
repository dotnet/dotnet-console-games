<h1 align="center">
	Battleship
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games"><img src="../../.github/resources/github-repo-black.svg" alt="GitHub repo"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/"><img src="../../.github/resources/language-csharp.svg" alt="Language C#"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Battleship%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"></a>
	<a href="../../LICENSE"><img src="../../.github/resources/license-MIT-green.svg" alt="License"></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Battleship" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" alt="Play Now"></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

This is a guessing game where you will place your battle ships on a grid, and then shoot locations of the enemy grid trying to find and sink all of their ships. The first player to sink all the enemy ships wins.

```
  Battleship

  ┌──┬──┬──┬──┬──┬──┬──┬──┬──┬──┐  ┌──┬──┬──┬──┬──┬──┬──┬──┬──┬──┐
  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │  │  │  │  │  │XX│  │  │  │  │  │  │  │  │  │  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │##│  │  │  │  │  │  │  │XX│  │  │  │  │  │  │XX│  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │  │  │  │  │  │XX│  │  │  │  │  │XX│##│  │  │  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │  │  │  │  │  │  │  │  │  │  │  │XX│  │  │XX│  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │  │  │  │XX│  │  │  │  │  │##│  │  │  │  │  │  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │  │  │  │  │  │  │  │  │  │  │  │XX│  │  │  │  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │  │  │  │XX│  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │
  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤  ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤
  │  │  │XX│  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │
  └──┴──┴──┴──┴──┴──┴──┴──┴──┴──┘  └──┴──┴──┴──┴──┴──┴──┴──┴──┴──┘

  Choose your shots.

  Hit: ##
  Miss: XX
  Use arrow keys to aim.
  Use [enter] to place the ship in a valid location.
```

## Input

- `enter`: confirm boat or shot location
- `↑`, `↓`, `←`, `→`: move boat or aim shot
- `spacebar`: rotate ship
- `escape`: exit game

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Battleship.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Battleship)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Battleship)
