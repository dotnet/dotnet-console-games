namespace Console_Monsters.Characters;

public class Nurse : CharacterBase
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


