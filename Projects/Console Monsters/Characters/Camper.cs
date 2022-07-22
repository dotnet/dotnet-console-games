namespace Console_Monsters.Characters;

public class Camper : CharacterBase
{
	public Camper()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
		{
			"Camper:",
			"Nothing better than warming up by a fire.",
		};
	}

	public override string? Name => "Camper";

	public static readonly string IdleFront =
		@"  ((())" + '\n' +
		@" ((( ^│" + '\n' +
		@"  ╭╰─┬╯" + '\n' +
		@"  │╰─│─" + '\n' +
		@" ╔═╗ ╮╮";
}
