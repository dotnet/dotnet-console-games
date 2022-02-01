<h1 align="center">
	Wordle
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FWordle%2FWordle.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Wordle%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord&logoColor=ffffff&color=7389D8" title="Go To Discord Server" alt="Discord"/></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

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
 - escape: exit

## Dependencies

Don't forget these dependencies if you copy the code:

- "FiveLetterWords.txt" embedded resource _(included in source)_
