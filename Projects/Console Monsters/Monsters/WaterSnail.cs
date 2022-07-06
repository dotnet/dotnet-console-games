namespace Console_Monsters.Monsters;

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
