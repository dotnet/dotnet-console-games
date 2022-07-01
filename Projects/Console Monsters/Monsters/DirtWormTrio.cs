namespace Console_Monsters.Monsters;

internal class DirtWormTrio : MonsterBase
{
	public DirtWormTrio()
	{
		Name = "Dirt Worm Trio";
	}

	public override string[] Sprite => (
		" ╭───╮       " + '\n' +
		" │‾o‾│ ╭───╮ " + '\n' +
		" │   │ │o_o│ " + '\n' +
		" │  ╭┴─┴╮  │ " + '\n' +
		" │  │^_^│  │ " + '\n' +
		" │  │   │  │ " + '\n' +
		" │  │   │  │ " + '\n' +
		"─┴──┴───┴──┴─").Split('\n');
}
