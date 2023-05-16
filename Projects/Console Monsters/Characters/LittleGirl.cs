namespace Console_Monsters.Characters;

public class LittleGirl : CharacterBase
{
	public LittleGirl()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
		{
			"Little Girl says:" +
			"I'm a copy paste file"
		};
	}

	public override string? Name => "Little Girl";

	private static readonly string IdleFront =
		@"╭╭───╮╮" + '\n' +
		@" │^_^│ " + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
}
