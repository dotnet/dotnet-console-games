﻿//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Marsupial : MonsterBase
{
	public Marsupial()
	{
		Name = "Marsupial";
	}

	public override string[] Sprite => (
		@"  ╭─────╮     " + "\n" +
		@"  │ ‾o‾ │     " + "\n" +
		@"╭─╰─────╯─╮   " + "\n" +
		@"│o╷╭───╮╷o│   " + "\n" +
		@"╰═╯│^_^│╰═╯   " + "\n" +
		@"╭╯╶┤┌─┐├╴╰╮   " + "\n" +
		@"│  ╰╯ ╰╯  ├──╮" + "\n" +
		@"│o╭─────╮o├──╯" + "\n" +
		@"╰═╯     ╰═╯   ").Split('\n');
}
