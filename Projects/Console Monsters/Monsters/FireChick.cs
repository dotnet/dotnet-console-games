namespace Console_Monsters.Monsters;

public class FireChick : MonsterBase
{
	public FireChick()
	{
		Name = "Fire Chick";
	}

	public override string[] Sprite => (
		@" \│/  " + "\n" +
		@"╭─╨──╮" + "\n" +
		@"│^__^│" + "\n" +
		@"╰W  W╯" + "\n" +
		@" ╰┬┬╯ " + "\n" +
		@"  ╛╘  ").Split('\n');
}