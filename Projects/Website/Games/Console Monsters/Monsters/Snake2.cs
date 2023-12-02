//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class Snake2 : MonsterBase
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
