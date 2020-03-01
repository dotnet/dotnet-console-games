# Wumpus World

![](https://github.com/ZacharyPatten/dotnet-console-games/workflows/Wumpus%20World%20Build/badge.svg)

**[Source Code](Program.cs)**

Wumpus World is a game where you explore a dark, dangerous cave in search of gold. The cave is a 4x4 grid and is randomized upon entering it. Being unable to see in the darkness, you must guess where to move and hope for the best. Step into a pit and you will fall to your death. Wake up the Wumpus and he will eat you alive. Find the gold, and you win. Although you cannot see, your other senses to help guide you. If you are adjacent to a pit, you will feel a breeze. If you are adjacent to the Wumpus, you can smell his foul odor.

### Example

||A|B|C|D|
|-|-|-|-|-|
|**0**|**Pit**|**Gold**<br>_breeze_|_breeze_|**Pit**|
|**1**|_breeze_|_foul odor_||_breeze_|
|**2**|_foul odor_|**Wumpus**|_foul odor_<br>_breeze_||
|**3**|**You**|_foul odor_<br>_breeze_|**Pit**|_breeze_|

- If you moved onto the `A2` cell, you would smell a foul odor
- If you moved onto the `A1` cell, you would feel a breeze
- If you moved onto the `B3` cell, you would smell a foul odor and feel a breeze
- If you moved onto the `B2` cell, the Wumpus would eat you
- If you moved onto the `A0`, `C3`, or `D0` cells, you would fall to your death
- If you try to move outside the grid, you will hit a wall and remain where you were
- If you moved onto the `B0` cell, find the gold and win the game

## Input

This game uses commands. Each screen will give you the list of available commands you may use at the time.

- `quit`
- `info`
- `up`
- `down`
- `left`
- `right`
- `yes`
- `no`

## Dependencies

Don't forget these dependencies if you copy the code:

- [Towel](https://github.com/ZacharyPatten/Towel) nuget package _(referenced in [Wumpus World.csproj](Wumpus%20World.csproj))_
