﻿//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Characters;

public class Scientist : CharacterBase
{
	public Scientist()
	{
		Sprite = IdleFront;

		Dialogue =
			[
				"Scientist Says:",
				"Hello! I am a scientist. :P",
			];
	}
	public override string? Name => "Scientist";

	public static readonly string IdleFront =
		@" ╭───╮ " + '\n' +
		@" ├■_■┤ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
	public static readonly string IdleLeft =
		@" ╭══╮  " + '\n' +
		@" │■-│  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │__│  ";
	public static readonly string IdleRight =
		@"  ╭══╮ " + '\n' +
		@"  │ ■│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ";
}


