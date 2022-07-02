namespace Console_Monsters.NPCs;

public class Scientist : NPCBase
{
	public Scientist()
	{
		Sprite = IdleFront;
	}
	public override string? Name => "Scientist";

	public static readonly string IdleFront =
		@" ╭───╮ " + '\n' +
		@" ├■_■┤ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
	public static readonly string IdleLeft =
		@" ╭══╮  " + '\n' +
		@" │■-│  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │__│  ";
	public static readonly string IdleRight =
		@"  ╭══╮ " + '\n' +
		@"  │ ■│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ";
}


