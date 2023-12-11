//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Characters;

public class Nurse : CharacterBase
{
	public Nurse()
	{
		Sprite = Idle1;

		Dialogue =
		[
			"Nurse Says:",
			"Hello! I have healed your monsters:P",
		];
	}
	public override string? Name => "Nurse";

	public static readonly string Idle1 =
		@"╭─────╮" + '\n' +
		@"│╭───╮│" + '\n' +
		@"╰│^_^│╯" + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"███████";
}


