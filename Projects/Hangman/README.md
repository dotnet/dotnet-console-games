# Hangman

![](https://github.com/ZacharyPatten/dotnet-console-games/workflows/Hangman%20Build/badge.svg)

**[Source Code](Program.cs)**

Hangman is a game that chooses a random word from the english language, and you try to guess the word. If you make too many incorrect guesses you lose.

**_This game is depended on a "Words.txt" embedded resource file to provide the pool of possible random words. If you copy the code without the embedded resource it will not work._**

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