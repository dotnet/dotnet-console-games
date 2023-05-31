<h1 align="center">
	Tanks
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games"><img src="../../.github/resources/github-repo-black.svg" alt="GitHub repo"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/"><img src="../../.github/resources/language-csharp.svg" alt="Language C#"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Tanks%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"></a>
	<a href="../../LICENSE"><img src="../../.github/resources/license-MIT-green.svg" alt="License"></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://dotnet.github.io/dotnet-console-games/Tanks" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" alt="Play Now"></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

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

- `W`, `A`, `S`, `D`: movement
- `↑`, `↓`, `←`, `→`: shoot
- `escape`: exit game

> resizing the console window will cause the game to close

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Tanks.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Tanks)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Tanks)
