<h1 align="center">
	Tents
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Tents%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Tents" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Tents is a randomly generated 6x6 grid where you need to place one tent next to trees. The goal is to have a tent above, below, left, or right of each tree and to have the expected number of tents in each row and column. However, the tents may not touch each other even diagonally.

```
  ╔══════╦══════╦══════╦══════╦══════╦══════╗
  ║ (@@) ║      ║      ║      ║ (@@) ║  \/  ║
  ║(@@@@)║      ║      ║      ║(@@@@)║  /\  ║ 1
  ║  ||  ║      ║      ║      ║  ||  ║ //\\ ║
  ╠══════╬══════╬══════╬══════╬══════╬══════╣
  ║  \/  ║ (@@) ║  \/  ║      ║      ║      ║
  ║  /\  ║(@@@@)║  /\  ║      ║      ║      ║ 2
  ║ //\\ ║  ||  ║ //\\ ║      ║      ║      ║
  ╠══════╬══════╬══════╬══════╬══════╬══════╣
  ║      ║      ║      ║      ║      ║      ║
  ║      ║      ║      ║      ║      ║      ║ 0
  ║      ║      ║      ║      ║      ║      ║
  ╠══════╬══════╬══════╬══════╬══════╬══════╣
  ║ (@@) ║  \/  ║      ║ (@@) ║      ║      ║
  ║(@@@@)║  /\  ║      ║(@@@@)║      ║      ║ 2
  ║  ||  ║ //\\ ║      ║  ||  ║      ║      ║
  ╠══════╬══════╬══════╬══════╬══════╬══════╣
  ║      ║      ║      ║      ║      ║      ║
  ║      ║      ║      ║      ║      ║      ║ 0
  ║      ║      ║      ║      ║      ║      ║
  ╠══════╬══════╬══════╬══════╬══════╬══════╣
  ║ (@@) ║      ║      ║      ║ (@@) ║  \/  ║
  ║(@@@@)║      ║      ║      ║(@@@@)║  /\  ║ 2
  ║  ||  ║      ║      ║      ║  ||  ║ //\\ ║
  ╚══════╩══════╩══════╩══════╩══════╩══════╝
     1      2      1      0      1      2
```

## Input

- `↑`, `↓`, `←`, `→`: tile selection
- `enter`: place/remove tree and confirm
- `escape`: exit game

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Tents.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Tents)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Tents)
