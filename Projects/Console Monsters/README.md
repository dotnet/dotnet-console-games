<h1 align="center">
	Console Monsters
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download" title="Target Framework" alt="Target Framework"><img src="../../.github/resources/dotnet-badge.svg" /></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Console%20Monsters%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Console%20Monsters" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

> **Note** This game was a *Community&nbsp;Collaboration!

> **Warning** This game is still a work-in-progress. The game is being developed in the [`console-monsters`](https://github.com/ZacharyPatten/dotnet-console-games/tree/console-monsters/Projects/Console%20Monsters) branch and will be occasionally merged into the `main` branch. If you would like to help out with the development of this game, please [join the discord server](https://discord.gg/4XbQbwF) to discuss. :)

Console Monsters is a role playing game where you catch and train monsters.

```
╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                       .   .                                                                                                                  .   . ║
║               ╔═════╗.  . .                                                            ////||||||||||||||||||||||||||||\\\\                 .  . . ║
║               ╠═════╣ .   .                                                           /////||||||||||||||||||||||||||||\\\\\                 .   . ║
║               ║     ║.  .  .                                                         //////||||||||||||||||||||||||||||\\\\\\               .  .  .║
║               ╩     ╩ .   .                                                         ///////||||||||||||||||||||||||||||\\\\\\\               .   . ║
║                       .   .                                     ┬──┬─┐              │                                        │               .   . ║
║               ╔═════╗.  . .                                    ╭┴──┴╮│              │                                        │              .  . . ║
║               ╠═════╣ .   .               ╦═════╦╦═════╦╦═════╦│Sign││              │       ▐█ ▐█  ▐█ ▐█  ▐█ ▐█  ▐█ ▐█       │               .   . ║
║               ║     ║.  .  .              ╬═════╬╬═════╬╬═════╬╰────╯│              │                                        │              .  .  .║
║               ╩     ╩ .   .               ╩     ╩╩     ╩╩     ╩      │              │                                        │               .   . ║
║                       .   .                .   .  .   .  .   .  .   .               │                                        │               .   . ║
║               ╔═════╗.  . .               .  . . .  . . .  . . .  . .               │                                        │              .  . . ║
║               ╠═════╣ .   .                .   .  .   .  .   .  .   .               │       ▐█ ▐█  ▐█ ▐█  ▐█ ▐█  ▐█ ▐█       │               .   . ║
║               ║     ║.  .  .              .  .  ..  .  ..  .  ..  .  .              │                                        │              .  .  .║
║               ╩     ╩ .   .                .   .  .   .  .   .  .   .               │                                        │               .   . ║
║                       .   .                .   .  .   .  .   .  .   .               │             ╔═════╗                    │               .   . ║
║               ╔═════╗.  . .               .  . . .  . . .  . . .  . .               │             ║ ■■■ ║                    │              .  . . ║
║               ╠═════╣ .   .                .   .  .   .  .   .  .   .               │       ▐█ ▐█ ║    o║ ▐█ ▐█  ▐█ ▐█       │               .   . ║
║               ║     ║.  .  .              .  .  ..  .  ..  .  ..  .  .              │             ║     ║                    │              .  .  .║
║               ╩     ╩ .   .                .   .  .   .  .   .  .   .               └─────────────╚═════╝────────────────────┘               .   . ║
║                       .   .                                            ╭═══╮               /_____\                                           .   . ║
║               ╔═════╗.  . .                                            │'_'│                │'_'│                                           .  . . ║
║               ╠═════╣ .   .                                           ╭╰───╯╮              ╭╰───╯╮                                           .   . ║
║               ║     ║.  .  .                                          │├───┤│              ╰├───┤╯                                          .  .  .║
║               ╩     ╩ .   .                                            │_|_│                │_|_│                                            .   . ║
║                       .   .                                                                                                                  .   . ║
║               ╔═════╗.  . .                                                                                                                 .  . . ║
║               ╠═════╣ .   .                                                                                                                  .   . ║
║               ║     ║.  .  .                                                                                                                .  .  .║
║               ╩     ╩ .   .                                                                                                                  .   . ║
║                       .   .                ╭───╮                                                          ┬──┬─┐                             .   . ║
║               ╔═════╗.  . .                ├■_■┤                                                         ╭┴──┴╮│                            .  . . ║
║               ╠═════╣ .   .               ╭╰───╯╮                                   ╦═════╦╦═════╦╦═════╦│Sign││╦═════╦╦═════╦               .   . ║
║               ║     ║.  .  .              ╰├───┤╯                                   ╬═════╬╬═════╬╬═════╬╰────╯│╬═════╬╬═════╬              .  .  .║
║               ╩     ╩ .   .                │_|_│                                    ╩     ╩╩     ╩╩     ╩      │╩     ╩╩     ╩               .   . ║
║                       .   .  .   .  .   . ╔═════╗╔═════╗╔═════╗╔═════╗               .   .  .   .  .   .  .   .  .   .  .   .                .   . ║
║               ╔═════╗.  . . .  . . .  . . ║█████║║█████║║█████║║█████║              .  . . .  . . .  . . .  . . .  . . .  . .               .  . . ║
║               ╠═════╣ .   .  .   .  .   . ║█████║║█████║║█████║║█████║               .   .  .   .  .   .  .   .  .   .  .   .                .   . ║
║               ║     ║.  .  ..  .  ..  .  .║█████║║█████║║█████║║█████║              .  .  ..  .  ..  .  ..  .  ..  .  ..  .  .              .  .  .║
║               ╩     ╩ .   .  .   .  .   . ╚═════╝╚═════╝╚═════╝╚═════╝               .   .  .   .  .   .  .   .  .   .  .   .                .   . ║
║                       .   .  .   .  .   . ╔═════╗~~~~~~~~~~~~~~╔═════╗               .   .  .   .  .   .  .   .  .   .  .   .                .   . ║
║               ╔═════╗.  . . .  . . .  . . ║█████║~~~~~~~~~~~~~~║█████║              .  . . .  . . .  . . .  . . .  . . .  . .               .  . . ║
║               ╠═════╣ .   .  .   .  .   . ║█████║~~~~~~~~~~~~~~║█████║               .   .  .   .  .   .  .   .  .   .  .   .                .   . ║
║               ║     ║.  .  ..  .  ..  .  .║█████║~~~~~~~~~~~~~~║█████║              .  .  ..  .  ..  .  ..  .  ..  .  ..  .  .              .  .  .║
╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
 [↑, W, ←, A, ↓, S, →, D]: Move, [E]: Interact, [B]: Status, [Escape]: Menu                                                                           
```

## Input

This game supports re-mapping input keys in the options menu. Here are the default key mappings:

- `↑`, `↓`, `←`, `→`: movement
- `enter`: confirm
- `E`: action
- `B`: status
- `escape`: open menu, back, or close

## Downloads

[win-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/win-x64/Console%20Monsters.exe)

[linux-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/linux-x64/Console%20Monsters)

[osx-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/osx-x64/Console%20Monsters)

## Contributors

Here is the first pull request from each contributor of `Console Monsters`:

- https://github.com/ZacharyPatten/dotnet-console-games/pull/45
- https://github.com/ZacharyPatten/dotnet-console-games/pull/53
- https://github.com/ZacharyPatten/dotnet-console-games/pull/58
- https://github.com/ZacharyPatten/dotnet-console-games/pull/60
- https://github.com/ZacharyPatten/dotnet-console-games/pull/61
- https://github.com/ZacharyPatten/dotnet-console-games/pull/64