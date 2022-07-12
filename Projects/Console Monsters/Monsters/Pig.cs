namespace Console_Monsters.Monsters;

public class Pig : MonsterBase
{
	public Pig()
	{
		Name = "Pig";
	}

	public override string[] Sprite => (
		@"/\__/\     " + '\n' +
		@"│^oo^├───╮ " + '\n' +
		@"╰─╤═─╯   │~" + '\n' +
		@"  ╰╥╥──╥╥╯ ").Split('\n');
}
