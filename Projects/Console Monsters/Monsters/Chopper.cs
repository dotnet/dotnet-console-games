namespace Console_Monsters.Monsters;

public class Chopper : MonsterBase
{
	public Chopper()
	{
		Name = "Chopper";
		//MonsterType = "Grass"; //Temp
	}

	public override string[] Sprite => (
		"   ╔═════╗   " + '\n' +
		"╷╷ ║     ║ ╷╷" + '\n' +
		"└┴─╢  ╳  ╟─┴┘" + '\n' +
		" ══╩╤═══╤╩══ " + '\n' +
		"   │ ∏ ∏ │   " + '\n' +
		"   │  ♥  │   " + '\n' +
		"  ╭ ───── ╮  " + '\n' +
		"  │       │  " + '\n' +
		" ╭╭╮     ╭╭╮ " + '\n' +
		"    │───│    " + '\n' +
		"  │││   │││  " + '\n' +
		"  ╭╭╮   ╭╭╮  ").Split('\n');

}
