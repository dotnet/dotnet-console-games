<h1 align="center">
	Lights Out
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games"><img src="../../.github/resources/github-repo-black.svg" alt="GitHub repo"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/"><img src="../../.github/resources/language-csharp.svg" alt="Language C#"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Lights%20Out%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"></a>
	<a href="../../LICENSE"><img src="../../.github/resources/license-MIT-green.svg" alt="License"></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Lights%20Out" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" alt="Play Now"></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Lights Out is a puzzle game in which you try to turn off all the lights. However, when you toggle a light on/off you will also toggle the lights adjacent to it.

```cs
╭───╮╭───╮╭───╮╭───╮╭───╮
│   ││   ││   ││   ││   │
╰───╯╰───╯╰───╯╰───╯╰───╯
╭───╮╭───╮╭───╮╭───╮╭───╮
│   ││   ││   ││   ││   │
╰───╯╰───╯╰───╯╰───╯╰───╯
╭───╮╭───╮╭───╮╭───╮╭───╮
│███││   ││███││   ││███│
╰───╯╰───╯╰───╯╰───╯╰───╯
╭───╮╔═╤═╗╭───╮╭───╮╭───╮
│   │╟   ╢│   ││   ││   │
╰───╯╚═╧═╝╰───╯╰───╯╰───╯
╭───╮╭───╮╭───╮╭───╮╭───╮
│   ││   ││   ││   ││   │
╰───╯╰───╯╰───╯╰───╯╰───╯

Turn off all the lights. Level 1.
```

## Input

- `↑`, `↓`, `←`, `→`: move cursor
- `enter`: flip lights & move to next level
- `backspace`: restart level
- `escape`: exit game

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Lights%20Out.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Lights%20Out)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Lights%20Out)
