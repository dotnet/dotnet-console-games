namespace Console_Monsters.Characters;

public class name : CharacterBase
{
	public name()
	{
		Sprite = IdleFront;

		Dialogue = new string[]
		{
			"name says:" +
			"I'm a copy paste file"
		};
	}

	public override string? Name => "name";

	public static readonly string IdleFront =
		@"COPY" + '\n' +
		@"COPY" + '\n' +
		@"COPY" + '\n' +
		@"COPY" + '\n' +
		@"COPY";
}
