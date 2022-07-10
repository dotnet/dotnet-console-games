namespace Console_Monsters.NPCs;

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
