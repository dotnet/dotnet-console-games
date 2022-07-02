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

public class FireLizard3 : MonsterBase
{
	public FireLizard3()
	{
		Name = "Fire Lizard Large";
	}

	public override string[] Sprite => (
		"     ╭─╮─╮      " + '\n' +
		"    ╭╯ ╰╮╰╮     " + '\n' +
		"   ╭╯   ╰╮│╭───╮" + '\n' +
		"   ╰╮    │││‾o‾│" + '\n' +
		"╰╮  ╰─╮  │││   │" + '\n' +
		"╰╮╰╮  ╰╮ ╰┴╯│ ││" + '\n' +
		"╰╮╰╮╰╮ ╰──╮ ╰─╯│" + '\n' +
		"   ╰──────╯   ╭╯" + '\n' +
		"         ╰┬╮ ╭╯ " + '\n' +
		"          ╰╰─╯  ").Split('\n');
}