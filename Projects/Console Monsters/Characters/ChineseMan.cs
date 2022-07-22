namespace Console_Monsters.Characters;

public class ChineseMan : CharacterBase
{
	public ChineseMan()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
		{
			"Chinese Man says:" +
			"I'm a copy paste file"
		};
	}

	public override string? Name => "Chinese Man";

	public static readonly string IdleFront =
		@"/_____\" + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
}
