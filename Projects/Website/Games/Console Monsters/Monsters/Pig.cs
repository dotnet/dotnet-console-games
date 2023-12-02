//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

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
