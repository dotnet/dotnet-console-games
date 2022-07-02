﻿namespace Console_Monsters.NPCs;

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


