<h1 align="center">
	Wordle
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Wordle%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Wordle" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Wordle is a game where you try to guess a 5 letter word. After each attempt you make to guess the word, the letters you guessed will be colored:

- green: this letter is correct and in the correct position
- yellow: this letter is in the word but not at this position
- gray: this letter is not in the word

Use these colored clues to guess the word. Each guess you make must be a valid word. Use up all your attempts without guessing the word and you lose.

```cs
 ╔═══╦═══╦═══╦═══╦═══╗
 ║ H ║ O ║ U ║ S ║ E ║
 ╠═══╬═══╬═══╬═══╬═══╣
 ║ P ║ L ║ A ║ C ║ E ║
 ╠═══╬═══╬═══╬═══╬═══╣
 ║ S ║ E ║ A ║ R ║ S ║
 ╠═══╬═══╬═══╬═══╬═══╣
 ║   ║   ║   ║   ║   ║
 ╠═══╬═══╬═══╬═══╬═══╣
 ║   ║   ║   ║   ║   ║
 ╠═══╬═══╬═══╬═══╬═══╣
 ║   ║   ║   ║   ║   ║
 ╚═══╩═══╩═══╩═══╩═══╝
```

## Input

 - a b, c, ... y, z: input letters
 - left/right arrow: move cursor
 - enter: submit or confirm
 - escape: exit game

## Downloads

[win-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/win-x64/Wordle.exe)

[linux-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/linux-x64/Wordle)

[osx-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/osx-x64/Wordle)
