//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Chopster : MonsterBase
{
	public Chopster()
	{
		Name = "Chopster";
	}

public override string[] Sprite => (
	"  ╔═════╗  " + '\n' +
	"╷╷║     ║╷╷" + '\n' +
	"└┴╢  ╳  ╟┴┘" + '\n' +
	" ═╩╤═══╤╩═ " + '\n' +
	"   │°ᴗ°│   " + '\n' +
	"  ╭╰───╯╮  " + '\n' +
	"  │├───┤│  " + '\n' +
	"  ╹╰┬─┬╯╹  " + '\n' +
	"   ━┘ └━   ").Split('\n');
}
