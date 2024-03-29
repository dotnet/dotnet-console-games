﻿//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Flower2 : MonsterBase
{
	public Flower2()
	{
		Name = "Flower";
	}

	public override string[] Sprite => (
		@"  ╭──╮╭──╮ " + '\n' +
		@" ╭┴─╭ⱺⱺ╮─┴╮" + '\n' +
		@" ╰┬─╰─○╯─┬╯" + '\n' +
		@"  ╰┬┬╯╰──╯ " + '\n' +
		@"   │├<>    " + '\n' +
		@" <>┤│      " + '\n' +
		@"   ││      " + '\n' +
		@"─^─┴┴^─^─  ").Split('\n');
}
