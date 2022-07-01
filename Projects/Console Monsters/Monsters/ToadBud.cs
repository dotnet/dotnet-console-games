namespace Console_Monsters.Monsters;

internal class ToadBud : MonsterBase
{
	public ToadBud()
	{
		Sprite = (
			"  ╭─────╮  " + '\n' +
			" ╭│ ^_^ │╮ " + '\n' +
			"╭─╰─────╯─╮" + '\n' +
			"│ ╭─────╮ │" + '\n' +
			"╰─╯─╯ ╰─╰─╯").Split('\n');
		Name = "Toad Bud";
	}
}