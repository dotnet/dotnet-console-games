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

namespace Website.Games.Console_Monsters.NPCs;

public class OldMan : NPCBase
{
	public OldMan()
	{
		Sprite = IdleFront;
	}

	public override string? Name => "Old Man";

	public static readonly string IdleFront =
		@" ╭───╮ " + '\n' +
		@" │‾_‾│ " + '\n' +
		@"╭╰─▼─╯╮" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
}
