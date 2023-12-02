//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Characters;

public class Monk : CharacterBase
{
	public Monk()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
		{
			"Monk Says:",
			"Hello! I am a Danish Monk. :P",
		};
	}
	public override string? Name => "Scientist";

	public static readonly string IdleFront =
		@" ╭───╮ " + '\n' +
		@" │^_^│ " + '\n' +
		@"╭╰─┬─╯╮" + '\n' +
		@"╰┬─┼─┬╯" + '\n' +
		@"╰──┴──╯";
}


