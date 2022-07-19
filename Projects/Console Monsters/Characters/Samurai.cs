namespace Console_Monsters.Characters;

public class Samurai : CharacterBase
{
	public Samurai()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
		{
			"Samurai says:" +
			"I'm a copy paste file"
		};
	}

	public override string? Name => "Samurai";

	public static readonly string IdleFront =
		@" /███\ " + '\n' +
		@"/│'_'│\" + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├─\─┤│" + '\n' +
		@" │_|_│ ";
}
