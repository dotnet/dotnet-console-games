<h1 align="center">
	Pong
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat"></a>
	<a href="#" alt="GitHub repo"><img alt="Language C#" src="https://img.shields.io/badge/language-C%23-%23178600"></a>
	<a href="#"><img src="https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FZacharyPatten%2Fdotnet-console-games%2Fmaster%2FProjects%2FPong%2FPong.csproj" title="Target Framework" alt="Target Framework"></a>
	<a href="#"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Pong%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="chat on Discord"><img src="https://img.shields.io/discord/557244925712924684?logo=discord" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/blob/master/LICENSE" alt="license"><img src="https://img.shields.io/badge/license-MIT-green.svg" /></a>
</p>

**[Source Code](Program.cs)**

Pong is a game where you try to prevent a ball from getting past you. You may only move your paddle vertically. The first player to score 3 points wins.

```
                  ^               
                 / \              
 █              /   \             
 █             /     \            
 █#           /       \           
 █ \         /         O        █ 
    \       /                   █ 
     \     /                    █ 
      \   /                     █ 
       \ /                        
        V                         
```

## Input

The **up arrow (↑)** and **down arrow (↓)** keys are used to move tyour paddle. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.