using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters._using;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.NPCs;
using System.Collections.Generic;

namespace Website.Games.Console_Monsters.Monsters;

public class DirtWormTrio : MonsterBase
{
	public DirtWormTrio()
	{
		Name = "Dirt Worm Trio";
	}

	public override string[] Sprite => (
		" ╭───╮       " + '\n' +
		" │‾o‾│ ╭───╮ " + '\n' +
		" │   │ │o_o│ " + '\n' +
		" │  ╭┴─┴╮  │ " + '\n' +
		" │  │^_^│  │ " + '\n' +
		" │  │   │  │ " + '\n' +
		" │  │   │  │ " + '\n' +
		"─┴──┴───┴──┴─").Split('\n');
}
