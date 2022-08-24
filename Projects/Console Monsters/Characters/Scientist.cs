namespace Console_Monsters.Characters;

public class Scientist : CharacterBase
{
	public Scientist()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
			{
				"Scientist Says:",
				"Hello! I am a scientist. :P",
			};
	}
	public override string? Name => "Scientist";

	private static readonly string IdleFront =
		@" ╭───╮ " + '\n' +
		@" ├■_■┤ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
	private static readonly string IdleLeft =
		@" ╭══╮  " + '\n' +
		@" │■-│  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │__│  ";
	private static readonly string IdleRight =
		@"  ╭══╮ " + '\n' +
		@"  │ ■│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ";
}


