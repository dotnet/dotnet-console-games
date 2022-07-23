<h1 align="center">
	Blackjack
</h1>

<p align="center">
	<a href="https://github.com/ZacharyPatten/dotnet-console-games" alt="GitHub repo"><img alt="flat" src="../../.github/resources/github-repo-black.svg"></a>
	<a href="https://docs.microsoft.com/en-us/dotnet/csharp/" alt="GitHub repo"><img alt="Language C#" src="../../.github/resources/language-csharp.svg"></a>
	<a href="https://dotnet.microsoft.com/download"><img src="../../.github/resources/dotnet-badge.svg" title="Target Framework" alt="Target Framework"></a>
	<a href="https://github.com/ZacharyPatten/dotnet-console-games/actions"><img src="https://github.com/ZacharyPatten/dotnet-console-games/workflows/Blackjack%20Build/badge.svg" title="Goto Build" alt="Build"></a>
	<a href="https://discord.gg/4XbQbwF" alt="Discord"><img src="../../.github/resources/discord-badge.svg" title="Go To Discord Server" alt="Discord"/></a>
	<a href="../../LICENSE" alt="license"><img src="../../.github/resources/license-MIT-green.svg" /></a>
</p>

<p align="center">
	You can play this game in your browser:
	<br />
	<a href="https://zacharypatten.github.io/dotnet-console-games/Blackjack" alt="Play Now">
		<sub><img height="40"src="../../.github/resources/play-badge.svg" title="Play Now" alt="Play Now"/></sub>
	</a>
	<br />
	<sup>Hosted On GitHub Pages</sup>
</p>

This is the Blackjack card game. It is played with a standard 52 count playing card deck. The goal is to get closer to 21 than the dealer without busting (going over 21). At the start of each round you will place a bet, then the dealer will deal you two cards and himself two cards (one face up and one face down). Then you will choose your move: stay, hit, double down, or split (up to 3 times) until your turn is complete. Then the dealer will reveal his face down card and draw cards as needed in attempts to be closer to 21 as you.

Bets must be in multiples of $2, because if you are dealt a blackjack you will get payed out 3:2.

The dealer will shuffle the discard pile into the deck when there is a need to draw a card but the deck is empty.

The dealer must draw until he has a hand score of at least 17.

If you double down you are dealt one additional card on the hand and then that hand is locked in.

Card Suits...
- H: Hearts
- C: Clubs
- D: Diamonds
- S: Spades

Card Values...
- Ace: 1 or 11
- Jack, Queen, or King: 10
- _Other (2-10)_: face value (eg. 3H -> 3, 8C -> 8)

```
  Blackjack

  Cards In Dealer Deck: 36
  Cards In Discard Pile: 9
  Your Money: $90

  Dealer Hand (24):
    ┌────┌────┌────┌────┌───────┐
    │5H  │7D  │AS  │AH  │JH     │
    │    │    │    │    │       │
    │    │    │    │    │       │
    │    │    │    │    │       │
    │    │    │    │    │     JH│
    └────└────└────└────└───────┘

  Your Hand:
    ┌────┌───────┐
    │9H  │AC     │
    │    │       │
  > │    │       │
    │    │       │
    │    │     AC│
    └────└───────┘
    Hand Score: 20
    Hand Bet: $10

  Win! The dealer busted (24).
  Your bet was payed out.
  Press [enter] to continue...
```

## Input

- `enter`: confirm
- `↑`: increase bet
- `↓`: decrease bet
- `1`: stay
- `2`: hit
- `3`: double down
- `4`: split
- `escape`: close the game

## Downloads

[win-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/win-x64/Blackjack.exe)

[linux-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/linux-x64/Blackjack)

[osx-x64](https://github.com/ZacharyPatten/dotnet-console-games/raw/binaries/osx-x64/Blackjack)
