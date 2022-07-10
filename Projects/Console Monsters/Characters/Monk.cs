namespace Console_Monsters.NPCs;

public class Monk : NPCBase
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


