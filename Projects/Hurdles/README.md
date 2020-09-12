<h1 align="center">
	Hurdles
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="#" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="#"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FHurdles%2FHurdles.csproj" title="Target Framework" alt="Target Framework"></a>
	<a href="#"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Hurdles%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Hurdles is a game where you run and jump over as many hurdles as possible. You may not change your speed or stop once you have started. If you collide with the center of a hurdle _(the `.` period)_ then your run is over.

```
      __O  
     / /\_,         ___                 ___                 ___
    __/\           |   |               |   |               |   |
        \          | . |               | . |               | . |
```

Remember, the only proper way to hurdle is to front flip...

```
                      __O__   __      __     __\  \O\__
               /O/   /       // \O   //_O\  _O/        \\
   _O          /    //           \\                          O
  |/|_   O    //                                           L|L
  /\    </L  //                                             |_
 /  |    /|                                                /  |
```

## Input

The **up arrow key (↑)** is used to initiate a jump. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.