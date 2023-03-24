<h1 align="center">
	Tic Tac Toe
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Tic%20Tac%20Toe%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Tic%20Tac%20Toe" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Tic Tac Toe is a game made of a 3x3 grid where 2 players take turns marking vacant cells in attempts to form a line using 3 of their markers. X's and O's are usually used for the markers. Any row, column, or diagonal of the grid may be used to form a 3 marker line win condition. If there are no remaining vacant cells and neither player has 3 markers in a line, it is a draw.

#### New Game (Empty Grid)

||0|1|2|
|-|-|-|-|
|**0**||||
|**1**||||
|**2**||||

#### Win Condition Examples

X Wins (Row):

||0|1|2|
|-|-|-|-|
|**0**|O||O|
|**1**|O|X||
|**2**|~~X~~|~~X~~|~~X~~|

O Wins (Column):

||0|1|2|
|-|-|-|-|
|**0**|||~~O~~|
|**1**|X||~~O~~|
|**2**|X|X|~~O~~|

X Wins (Diagonal):

||0|1|2|
|-|-|-|-|
|**0**|O|O|~~X~~|
|**1**||~~X~~||
|**2**|~~X~~|||

#### Draw Condition Examples

||0|1|2|
|-|-|-|-|
|**0**|X|O|X|
|**1**|O|O|X|
|**2**|X|X|O|

## Input


- `↑`, `↓`, `←`, `→`: change the selected cell
- `enter`: mark the selected cell
- `escape`: exit game

> resizing the console window will cause the game to close

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Tic%20Tac%20Toe.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Tic%20Tac%20Toe)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Tic%20Tac%20Toe)
