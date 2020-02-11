# Simon

![](https://github.com/ZacharyPatten/dotnet-console-games/workflows/Simon%20Build/badge.svg)

**[Source Code](Program.cs)**

Simon is a pattern memory game. The game will generate a random series of directional inputs, and you try to repeat the pattern. Every time you successfully repeat the pattern, the pattern will grow making it harder to remember. Get the pattern wrong at any time you lose.

```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝   ║
    ║       ║    ║       ║
    ║   ╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

Up (↑):
```cs
    
           ╔══════╗
           ║██████║
           ╚╗████╔╝
    ╔═══╗   ╚╗██╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝   ║
    ║       ║    ║       ║
    ║   ╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

Right (→):
```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝███║
    ║       ║    ║███████║
    ║   ╔═══╝╔══╗╚═══╗███║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

Down (↓):
```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║   ╚═══╗╚══╝╔═══╝   ║
    ║       ║    ║       ║
    ║   ╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝██╚╗   ╚═══╝
           ╔╝████╚╗
           ║██████║
           ╚══════╝
```

Left (←):
```cs
           ╔══════╗
           ║      ║
           ╚╗    ╔╝
    ╔═══╗   ╚╗  ╔╝   ╔═══╗
    ║███╚═══╗╚══╝╔═══╝   ║
    ║███████║    ║       ║
    ║███╔═══╝╔══╗╚═══╗   ║
    ╚═══╝   ╔╝  ╚╗   ╚═══╝
           ╔╝    ╚╗
           ║      ║
           ╚══════╝
```

## Input
The **arrow keys (↑, ↓, ←, →)** are used to repeat the randomized pattern. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.
