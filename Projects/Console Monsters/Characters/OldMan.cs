﻿namespace Console_Monsters.NPCs;

public class OldMan : CharacterBase
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
