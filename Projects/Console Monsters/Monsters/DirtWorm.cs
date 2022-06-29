namespace Console_Monsters.Monsters;

internal class DirtWorm : MonsterBase
{
	public DirtWorm()
	{
		Sprite = (
			"     ╭───╮     " + '\n' +
			"     │^_^│     " + '\n' +
			"     │   │     " + '\n' +
			"     │   │     " + '\n' +
			"    ─┴───┴─    ").Split('\n');
	}
}
