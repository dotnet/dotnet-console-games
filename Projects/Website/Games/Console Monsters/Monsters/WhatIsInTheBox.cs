//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class WhatIsInTheBox : MonsterBase
{
	public WhatIsInTheBox()
	{
		Name = "What's In The Box";
	}

	public override string[] Sprite => (
		"█████" + '\n' +
		"█^-^█" + '\n' +
		"█████").Split('\n');
}
