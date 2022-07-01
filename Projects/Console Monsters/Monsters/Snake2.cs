namespace Console_Monsters.Monsters;

internal class Snake2 : MonsterBase
{
	public Snake2()
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
