<h1 align="center">
	Snake
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Snake%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Snake" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Snake is a game where you play as a snake and try to eat as much food as possible. Once you start moving you may not stop. You lose if you fall out of bounds or attempt to eat yourself. Every time you eat food, you grow in length, making it harder to avoid auto-cannibalism. The bounds are the edges of the console window.

Food: `+`

Snake (size 1): `^`

Snake (size 29):
```
<<<<<<^
v     ^
v   >>>
v
v>>>>>>>>>>>>>
```

## Input

- `↑`, `↓`, `←`, `→`: change the direction you are moving
- `escape`: exit game

> resizing the console window will cause the game to close

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Snake.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Snake)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Snake)
