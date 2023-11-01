//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class Cactus : MonsterBase
{
	public Cactus()
	{
		Name = "Cactus";
	}

	public override string[] Sprite => (
		@"     ╭┴─┴╮     " + '\n' +
		@" ╭┴╮╶┤^_^├╴    " + '\n' +
		@"╶┴╷╰┴╯╷ ╷├╴╭┴╮ " + '\n' +
		@"   ‾‾┼ ╷ ╰┴╯╷┴╴" + '\n' +
		@"    ╶┤╷ ╷┼‾‾   " + '\n' +
		@"  ─^─┴─^─┴^─^─ ").Split('\n');
}
