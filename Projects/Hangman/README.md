# Hangman

![](https://github.com/ZacharyPatten/dotnet-console-games/workflows/Hangman%20Build/badge.svg)

**[Source Code](Program.cs)**

Hangman is a game that chooses a random word from the english language, and you try to guess the word. If you make too many incorrect guesses you lose.

```

      ╔═══╗
      |   ║
      O   ║
     /|\  ║
       \  ║
     ███  ║
    ══════╩═══

```

## Input

The **alphabetic keys (a, b, c, ...)** are used to guess letters. The **escape** key may be used to close the game at any time. If you **resize** the console widow the game will be closed.

## Dependencies

Don't forget these dependencies if you copy the code:

- "Words.txt" embedded resource _(included in source)_
