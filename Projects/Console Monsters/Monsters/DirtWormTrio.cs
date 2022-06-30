namespace Console_Monsters.Monsters;

internal class DirtWormTrio : MonsterBase
{
	public DirtWormTrio()
	{
		Sprite = (
			" ╭───╮ ╭───╮ " + '\n' +
			" │‾o‾│ │o_o│ " + '\n' +
			" │  ╭┴─┴╮  │ " + '\n' +
			" │  │^_^│  │ " + '\n' +
			" │  │   │  │ " + '\n' +
			" │  │   │  │ " + '\n' +
			"─┴──┴───┴──┴─").Split('\n');
	}
}
