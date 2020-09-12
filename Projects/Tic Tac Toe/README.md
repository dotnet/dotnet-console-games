<h1 align="center">
	Tic Tac Toe
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FTic%2520Tac%2520Toe%2FTic%2520Tac%2520Toe.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Tic%20Tac%20Toe%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

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

The **arrow keys (↑, ↓, ←, →)** are used to change the selected cell on the grid. The **enter** key is used to mark the selected cell with your mark. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.
