//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class Goat : MonsterBase
{
	public Goat()
	{
		Name = "Goat";
	}

	public override string[] Sprite => (
		@"     ╭╭╮_   " + '\n' +
		@"     ╰╰╷^╰─╮" + '\n' +
		@"*~~~~~~╭───╯" + '\n' +
		@" ~~~~~~~    " + '\n' +
		@" ╰╯╯ ╰╰╯    ").Split('\n');
}
