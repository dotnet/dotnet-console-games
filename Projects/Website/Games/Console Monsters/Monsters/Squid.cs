//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Squid : MonsterBase
{
	public Squid()
	{
		Name = "Squid";
	}

	public override string[] Sprite => (
		@" ╭───╮  " + "\n" +
		@" │^_^│  " + "\n" +
		@"╭╰───╯─╮" + "\n" +
		@"│╭╮╭╮╭╮│" + "\n" +
		@"││││││││" + "\n" +
		@"╰╰╰╯╰╯╯╯").Split('\n');
}
