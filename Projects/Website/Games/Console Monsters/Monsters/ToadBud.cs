//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class ToadBud : MonsterBase
{
	public ToadBud()
	{
		Name = "Toad Bud";
	}

	public override string[] Sprite => (
		"  ╭─────╮  " + '\n' +
		" ╭│ ^_^ │╮ " + '\n' +
		"╭─╰─────╯─╮" + '\n' +
		"│ ╭─────╮ │" + '\n' +
		"╰─╯─╯ ╰─╰─╯").Split('\n');
}