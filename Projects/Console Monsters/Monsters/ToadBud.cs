namespace Console_Monsters.Monsters;

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