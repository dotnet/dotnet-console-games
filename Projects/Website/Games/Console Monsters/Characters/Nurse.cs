using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Items;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Characters;
using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Screens.Menus;
using Website.Games.Console_Monsters.Enums;
using Website.Games.Console_Monsters.Utilities;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Characters;

public class Nurse : CharacterBase
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


