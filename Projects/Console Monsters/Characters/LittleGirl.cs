namespace Console_Monsters.Characters;

public class LittleGirl : CharacterBase
{
	public LittleGirl()
	{
		Sprite = IdleFront;

		Dialogue =
		[
			"Little Girl says:" +
			"I'm a copy paste file"
		];
	}

	public override string? Name => "Little Girl";

	public static readonly string IdleFront =
		@"╭╭───╮╮" + '\n' +
		@" │^_^│ " + '\n' +
		@"╭┴───┴╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
}
