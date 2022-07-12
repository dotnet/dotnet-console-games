﻿namespace Console_Monsters.Monsters;

public class Duck : MonsterBase
{
	public Duck()
	{
		Name = "Duck";
	}

	public override string[] Sprite => (
		@" ╭─╮    " + '\n' +
		@">╵^│    " + '\n' +
		@" │ ╰┬──╮" + '\n' +
		@" │  ╰──┤" + '\n' +
		@" ╰─┬─┬─╯" + '\n' +
		@"   ^ ^  ").Split('\n');
}
