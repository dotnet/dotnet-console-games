﻿using System;
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

public class MartialArtist : NPCBase
{
	public MartialArtist()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
		{
			"Martial Artist says:" +
			"Wanna learn some martial arts?"
		};
	}

	public override string? Name => "Martial Artist";

	public static readonly string IdleFront =
		@" ╭───╮ " + '\n' +
		@" │^_^│ " + '\n' +
		@"╭╰─┬─╯╮" + '\n' +
		@"╰┬─┴─┬╯" + '\n' +
		@"/_/‾\_\";
}
