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

public class Nurse : NPCBase
{
	public Nurse()
	{
		Sprite = Idle1;

		Dialogue = new string[]
		{
			"Nurse Says:",
			"Hello! I have healed your monsters:P",
		};
	}
	public override string? Name => "Nurse";

	public static readonly string Idle1 =
		@"╭─────╮" + '\n' +
		@"│╭───╮│" + '\n' +
		@"╰│^_^│╯" + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"███████";
}


