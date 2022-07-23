<h1 align="center">
	Minesweeper
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Minesweeper%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Minesweeper" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

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

- `↑`, `↓`, `←`, `→`: change the selected tile
- `enter`: reveal the selected tile
- `escape`: exit game

> resizing the console window will cause the game to close

## Downloads

[win-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/win-x64/Minesweeper.exe)

[linux-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/linux-x64/Minesweeper)

[osx-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/osx-x64/Minesweeper)
