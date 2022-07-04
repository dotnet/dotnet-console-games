namespace Console_Monsters.Monsters;

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
