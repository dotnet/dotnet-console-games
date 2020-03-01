# Maze

![](https://github.com/ZacharyPatten/dotnet-console-games/workflows/Maze%20Build/badge.svg)

**[Source Code](Program.cs)**

Maze is a pretty self explanatory game. Solve the randomly generated maze. The maze size if coded to be 8x20. You always start in the top left and the end is always in the bottom right.

```
████████████████████████████████████████████████████████████
█S██    ██ ██          ██ ██    ██       ██       ██ ██    █
█ ██ █████ █████ █████ ██ ██ ██ ██ █████ ██ ██ ██ ██ ██ ████
█ ██ █████ █████ █████ ██ ██ ██ ██ █████ ██ ██ ██ ██ ██ ████
█       ██    ██    ██       ██ ██    ██ ██ ██ ██    ██    █
█ █████ ██ ████████ ██ ████████ ██ ██ ██ ██ ████████ ██ ██ █
█ █████ ██ ████████ ██ ████████ ██ ██ ██ ██ ████████ ██ ██ █
█    ██ ██    ██ ██ ██       ██ ██ ██ ██ ██    ██       ██ █
█ ████████ ██ ██ ██ ██████████████ █████ ██ ██ ███████████ █
█ ████████ ██ ██ ██ ██████████████ █████ ██ ██ ███████████ █
█       ██ ██    ██                ██ ██    ██       ██ ██ █
█ █████ ██ █████ ████████ █████ █████ █████████████████ ██ █
█ █████ ██ █████ ████████ █████ █████ █████████████████ ██ █
█    ██       ██ ██ ██       ██    ██    ██ ██          ██ █
████ ██████████████ ██ ███████████ ██ ██ ██ ██ ████████ ██ █
████ ██████████████ ██ ███████████ ██ ██ ██ ██ ████████ ██ █
█    ██    ██    ██ ██    ██ ██ ██ ██ ██ ██    ██    ██    █
█ ██ █████ ██ ██ ██ █████ ██ ██ █████ ██ █████ ██ ██ ███████
█ ██ █████ ██ ██ ██ █████ ██ ██ █████ ██ █████ ██ ██ ███████
█ ██    ██    ██ ██    ██       ██ ██ ██       ██ ██       █
████ █████ █████ ██ ███████████ ██ ██ █████ █████ ██ █████ █
████ █████ █████ ██ ███████████ ██ ██ █████ █████ ██ █████ █
█          ██                      ██    ██       ██    ██E█
████████████████████████████████████████████████████████████
```

## Input

The **arrow keys (↑, ↓, ←, →)** are used to change the direction you are moving. The **escape** key may be used to close the game at any time.

## Notes

At the top of the **[source code](Program.cs)** you will see two compiler directives...
- `#define MazeGenertorLoop`: Uncomment this directive and you can watch the code generate mazes infinitely inside a `while (true)` loop.
- `#define DebugRandomMazeGeneration`: Uncomment this directive and you can watch the maze generation algorithm step-by-step.
- `#define UsePrims`: Uncomment this directive to use an alternative algorithm for generating mazes.

## Dependencies

Don't forget these dependencies if you copy the code:

- [Towel](https://github.com/ZacharyPatten/Towel) nuget package _(referenced in [Maze.csproj](Maze.csproj))_
