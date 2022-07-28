namespace Console_Monsters.Monsters;

public class Scorpion : MonsterBase
{
	public Scorpion()
	{
		Name = "Scorpion";
	}

	public override string[] Sprite => (
		@"   (╰┬╯)   " + "\n" +
		@"    )-(    " + "\n" +
		@"    (-)    " + "\n" +
		@"V ╭─┴─┴─╮ V" + "\n" +
		@"╚═│ ^_^ │═╝" + "\n" +
		@" ┌╰┬───┬╯┐ ").Split('\n');
}
