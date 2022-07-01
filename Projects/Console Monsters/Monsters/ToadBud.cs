namespace Console_Monsters.Monsters;

internal class ToadBud : MonsterBase
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