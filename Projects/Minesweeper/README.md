<h1 align="center">
	Minesweeper
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FMinesweeper%2FMinesweeper.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Minesweeper%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Minesweeper is a board game where you try to reveal the entire board without revealing a mine. The number of a tile represents the number of mines that are adjacent to it.

```
 1██████1  1████████████████████████████1                  1█1 1██████2
 11211111  11███████████████████████████1 111  11211       111 1██████2   111
            1█████████████████████████21112█1112███1          11█████21   1█211
     111    1█████████████████████████2  1█████████2  111     1████321    2███1
 111 1█1111 1111█112████████████11112█2  112███████1 12█1111112████1 111  1██21
 1█1 1111█1    2█2 111██████████1   111    1███████112██████████1111 1█1  12█1
 111    111    1█1   1████313███1  111 111 112██████████████████1    111   2█2
11 111 111     11211 1█1111 2███1  1█1 1█1   1████████████████321111       2█2
█2 1█1 1█1       1█1 111    3██21  111 111   111███████████1111 1██1  111  2█2
█2 111 111       111    111 2█31 111           1████22112321    1█21 12█1112█111
11              111     1█11221  1█1   111     2████1          1221  1██████████
                1█1     1111█1   111   1█1     2████1      11113█2   1██████████
11111      111112█21       1█1         111     12█211      1█████2 111██████████
████21   112███████1       1█1   111            111        2███221 2████████████
█████1   1█21112███21    112█1   1█1  111                  1███1   2████████████
█████1   111   1111█1    2█311   111  1█1          111  1111███1  12████████████
█████11           111    2█2         1221       1111█1  1█████21  1█████████████
██████1      111    111  111     111 1█1        1█1112111█████2  12██2322███████
██████1111 112█1    1█1          1█1 1█1       1221  1████████2  1███1  1███████
█████████1 2█311 111111      111 111 1█1      12█1   1████████211112█1  113█████
█████████1 2█2   1█1         1█1     1█1  111 1█21  11213████████1 111    3█████
█████████1 111   111         111     111 12█1 111   1█1 3████████1        2█████
█████████2           111               112██2       111 2████21211   1221 2█████
█████████2         112█1   111      1111████21   111    1111█1       1██1 2█████
█████████1         1███1   1█1      1████████1   1█1       1█1       1██1 2█████
```

## Input

The **arrow keys (↑, ↓, ←, →)** are used to change the selected tile on the board. The **enter** key is used to reveal the selected tile. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.