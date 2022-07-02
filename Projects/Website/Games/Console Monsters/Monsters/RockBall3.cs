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

public class RockBall3 : MonsterBase
{
	public RockBall3()
	{
		Name = "Rock Ball";
	}

	public override string[] Sprite => (
		"╭───╮             ╭───╮" + '\n' +
		"╰╮ ╮╯  ╭───────╮  ╰╭ ╭╯" + '\n' +
		" │ │╭─╮│       │╭─╮│ │ " + '\n' +
		" │ ╰╯ ╰┤  ‾o‾  ├╯ ╰╯ │ " + '\n' +
		" ╰─────┤       ├─────╯ " + '\n' +
		"       ╰───────╯       ").Split('\n');
}
