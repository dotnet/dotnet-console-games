<h1 align="center">
	Tanks
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FTanks%2FTanks.csproj&logo=.net" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Tanks%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Tanks is a game where you play as a tank and try to shoot and destroy other tanks before you get destroyed yourself. You always start in the top left corner. You may only shoot one bullet at a time; aim carefully. The other three tanks are controlled by basic contitional logic (AI).

```
╔═════════════════════════════════════════════════════════════════════════╗
║                                                                         ║
║                                                     ___                 ║
║                                                    |_O_|                ║
║                                    ║               [ooo]                ║
║                    ___             ║                                    ║
║                   |_O_|            ║                                    ║
║                   [ooo]                              v                  ║
║                                                                         ║
║                                                                         ║
║                                                                         ║
║                    v                                                    ║
║                                                                         ║
║     ═════                                                     ═════     ║
║               ^                                                         ║
║                                                                         ║
║                                                                         ║
║                                                             __          ║
║                                              <            =|__|         ║
║                                                           [ooo]         ║
║              _^_                                                        ║
║             |___|                  ║                                    ║
║             [ooo]                  ║                                    ║
║                                    ║                                    ║
║                                                                         ║
║                                                                         ║
║                                                                         ║
╚═════════════════════════════════════════════════════════════════════════╝
```

## Input

The **(W, A, S, D) keys** are used move your tank. The **arrow keys (↑, ↓, ←, →)** are used to shoot bullets. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.
