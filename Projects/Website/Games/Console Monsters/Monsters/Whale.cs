//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class Whale : MonsterBase
{
	public Whale()
	{
		Name = "Whale";
	}

	public override string[] Sprite => (
		@" __    ______ " + '\n' +
		@"╵╮ ╲__╱   ^_^╲" + '\n' +
		@"╷╯ ╱──╲ ╰─╯  ╱" + '\n' +
		@" ‾‾    ‾‾‾‾‾‾ ").Split('\n');
}
