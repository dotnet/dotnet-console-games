<h1 align="center">
	Wumpus World
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Wumpus%20World%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Wumpus%20World" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Wumpus World is a game where you explore a dark, dangerous cave in search of gold. The cave is a 4x4 grid and is randomized upon entering it. Being unable to see in the darkness, you must guess where to move and hope for the best. Step into a pit and you will fall to your death. Wake up the Wumpus and he will eat you alive. Find the gold, and you win. Although you cannot see, your other senses to help guide you. If you are adjacent to a pit, you will feel a breeze. If you are adjacent to the Wumpus, you can smell his foul odor.

### Example

||A|B|C|D|
|-|-|-|-|-|
|**0**|**Pit**|**Gold**<br>_breeze_|_breeze_|**Pit**|
|**1**|_breeze_|_foul odor_||_breeze_|
|**2**|_foul odor_|**Wumpus**|_foul odor_<br>_breeze_||
|**3**|**You**|_foul odor_<br>_breeze_|**Pit**|_breeze_|

- If you moved onto the `A2` cell, you would smell a foul odor
- If you moved onto the `A1` cell, you would feel a breeze
- If you moved onto the `B3` cell, you would smell a foul odor and feel a breeze
- If you moved onto the `B2` cell, the Wumpus would eat you
- If you moved onto the `A0`, `C3`, or `D0` cells, you would fall to your death
- If you try to move outside the grid, you will hit a wall and remain where you were
- If you moved onto the `B0` cell, find the gold and win the game

## Input

This game uses commands. Type out the command you wish to use and press `enter`.

Commands:
- `quit`
- `info`
- `up`
- `down`
- `left`
- `right`
- `yes`
- `no`

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Wumpus%20World.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Wumpus%20World)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Wumpus%20World)
