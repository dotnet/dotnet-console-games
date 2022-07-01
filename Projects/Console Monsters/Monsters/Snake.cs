namespace Console_Monsters.Monsters;

internal class BiggerSnake : MonsterBase
{
	public BiggerSnake()
	{
		Name = "Bigger Snake";
	}

	public override string[] Sprite => (
		"                 ╔════╗" + '\n' +
		"                 ║ ‾o‾║" + '\n' +
		"                 ╚═╗╔═╝" + '\n' +
		"╔═══╗ ╔═══╗ ╔═══╗  ║║  " + '\n' +
		"║║‾║║_║║‾║║_║║‾║║__║║  " + '\n' +
		"╚╝ ╚═══╝ ╚═══╝ ╚════╝  ").Split('\n');
}
