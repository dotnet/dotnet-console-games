namespace Console_Monsters.Monsters;

internal class BiggerSnake : MonsterBase
{
	public BiggerSnake()
	{
		Sprite = (
			"                     ╔════╗    " + '\n' +
			"                     ║ ‾o‾║    " + '\n' +
			"                     ╚═╗╔═╝    " + '\n' +
			"    ╔═══╗ ╔═══╗ ╔═══╗  ║║      " + '\n' +
			"    ║║‾║║_║║‾║║_║║‾║║__║║      " + '\n' +
			"    ╚╝ ╚═══╝ ╚═══╝ ╚════╝      ").Split('\n');
	}
}
