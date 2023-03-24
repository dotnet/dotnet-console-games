<h1 align="center">
	Yahtzee
</h1>

<p align="center">
	<a href="https://github.com/dotnet/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/dotnet/dotnet-console-games/actions"><img src="https://github.com/dotnet/dotnet-console-games/workflows/Yahtzee%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Yahtzee" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

Yahtzee is a dice roll-and-write game. You roll 5 dice, choose any number of those dice to re-roll up to 2 times, and then choose one of 13 lines to fill in on your score sheet. Aim to get the highest total score by the end of the game.

```
  Yahtzee
  ╒════════ Score Sheet ════════╕
  ├─────── Upper Section ───────┤
  │   Aces...............[    ] │
  │   Twos...............[    ] │
  │   Threes.............[    ] │
  │   Fours..............[   8] │
  │   Fives..............[  15] │
  │   Sixes..............[    ] │
  │   Aces-Sixes Bonus...[    ] │
  ├─────── Lower Section ───────┤
  │   3 of a Kind........[  25] │
  │   4 of a Kind........[  21] │
  │   Full House.........[    ] │
  │   Small Straight.....[    ] │
  │   Large Straight.....[    ] │
  │   Yahtzee............[    ] │
  │   Chance.............[    ] │
  │   Yahtzee Bonus......[    ] │
  │   Total..............[    ] │
  └─────────────────────────────┘
        ╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗
  Dice: ║ 3 ║║ 6 ║║ 2 ║║ 2 ║║ 5 ║
        ╚═══╝╚═══╝╚═══╝╚═══╝╚═══╝
```

Score Sheet Explanations:
- Upper Section
  - Aces: total of all dice with 1 values (example `1-2-2-4-6 = 1 points` or `1-1-1-3-5 = 3 points`)
  - Twos: total of all dice with 2 values (example `2-1-5-3-5 = 2 points` or `2-2-2-2-4 = 8 points`)
  - Threes: total of all dice with 3 values (example `3-1-1-4-5 = 3 points` or `3-3-3-2-4 = 9 points`)
  - Fours: total of all dice with 4 values (example `4-1-3-2-5 = 4 points` or `4-4-3-2-5 = 8 points`)
  - Fives: total of all dice with 5 values (example `5-1-3-2-6 = 5 points` or `5-5-5-2-1 = 15 points`)
  - Sixes: total of all dice with 6 values (example `6-1-2-3-4 = 6 points` or `6-6-6-6-1 = 24 points`)
  - _[automatic] Aces-Sixes Bonus: gain a 35 bonus if you score at least 63 total in the rest of the upper section_
- Lower Section
  - 3 of a Kind: gain points equal to the sum of the dice values if you have at least 3 of the same value (example `1-1-1-2-2 = 7 points` or `5-5-5-2-2 = 19 points`)
  - 4 of a Kind: gain points equal to the sum of the dice values if you have at least 4 of the same value (example `1-1-1-1-2 = 6 points` or `5-5-5-5-2 = 22 points`)
  - Full House: gain 25 points if you roll 2 of one value and 3 of another value (example `1-1-2-2-2` or `3-3-5-5-5` or `6-6-2-2-2`)
  - Small Straight: gain 30 points if you roll a run of 4 (example `1-2-3-4-*` or `2-3-4-5-*` or `3-4-5-6-*`)
  - Large Straight: gain 40 points if you roll a run of 5 (example `1-2-3-4-5`  or `2-3-4-5-6`)
  - Yahtzee: gain 50 points if you roll 5 of the same value (example `1-1-1-1-1` or `2-2-2-2-2`)
  - Chance: gain points equal to the sum of the dice values (example `1-1-1-1-1 = 5 points` or `1-2-3-4-5 = 15 points` or `5-5-5-6-6 = 27 points`)
  - _[automatic] Yahtzee Bonus: gain a 100 bonus for each Yahtzee you roll after having already scored a 50-point Yahtzee_
  - _[automatic] Total: your calculated total score at the end of the game_

## Input

- `enter`: confirm
- `space`: roll dice
- `↑`, `←`, `→`: select dice to re-roll
- `↑`, `↓`: select score sheet item
- `escape`: exit game

## Downloads

[win-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/win-x64/Yahtzee.exe)

[linux-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/linux-x64/Yahtzee)

[osx-x64](https://github.com/dotnet/dotnet-console-games/raw/binaries/osx-x64/Yahtzee)
