namespace Console_Monsters.Monsters;

public class DirtWorm : MonsterBase
{
	public DirtWorm()
	{
		Name = "Dirt Worm";
	}

	public override string[] Sprite => (
		" ╭───╮ " + '\n' +
		" │^_^│ " + '\n' +
		" │   │ " + '\n' +
		" │   │ " + '\n' +
		"─┴───┴─").Split('\n');
}
