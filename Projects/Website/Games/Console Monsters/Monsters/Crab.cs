//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Crab : MonsterBase
{
	public Crab()
	{
		Name = "Crab";
	}

	public override string[] Sprite => (
		@" _╭─────╮_ " + "\n" +
		@"//│ ^_^ │\\" + "\n" +
		@"//╰╥───╥╯\\" + "\n" +
		@"  ╰‾╯ ╰‾╯  ").Split('\n');
}
