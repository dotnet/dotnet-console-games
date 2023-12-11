//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class WaterSnail : MonsterBase
{
	public WaterSnail()
	{
		Name = "Water Snail";
	}

	public override string[] Sprite => (
		@"    ╭─┬──┬─╮" + '\n' +
		@"◦╮╭◦├ ╭─┴╮ ┤" + '\n' +
		@"╭┴┴─┼ ╰┬ ├ │" + '\n' +
		@"│^_^╰┬┼─┬╯ ┤" + '\n' +
		@"╰┬┬┬┬┴─┴─┴─╯").Split('\n');
}
