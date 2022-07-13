﻿namespace Console_Monsters.Monsters;

internal class PlantFace : MonsterBase
{
	public PlantFace()
	{
		Name = "Plant Face";
	}

	public override string[] Sprite => (
		@"        //        " + "\n" +
		@"        ││<>      " + "\n" +
		@"/‾‾‾\╭──╯╰──╮     " + "\n" +
		@"\___/│ o  O │/‾‾‾\" + "\n" +
		@"     │╭════╮│\___/" + "\n" +
		@"     ╰╰════╯╯     ").Split('\n');
}
