//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Camel : MonsterBase
{
	public Camel()
	{
		Name = "Camel";
	}

	public override string[] Sprite => (
		@"         ╭─╮  " + "\n" +
		@"         │^╰─╮" + "\n" +
		@"    ╭─╮  │ ╭─╯" + "\n" +
		@"╭╭──╯~╰──╯ │  " + "\n" +
		@"╯│        ╭╯  " + "\n" +
		@" │╭┬────┬╮│   " + "\n" +
		@" │││    │││   " + "\n" +
		@" ╰╯╯    ╰╰╯   ").Split('\n');
}
