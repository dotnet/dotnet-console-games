namespace Console_Monsters.Monsters;

internal class DirtWorm : MonsterBase
{
	public DirtWorm()
	{
		Name = "Dirt Worm";
	}

	public override string[] Sprite => (
		"  ╭───╮ " + '\n' +
		"  │^_^│ " + '\n' +
		"  │   │ " + '\n' +
		"  │   │ " + '\n' +
		" ─┴───┴─").Split('\n');
}
