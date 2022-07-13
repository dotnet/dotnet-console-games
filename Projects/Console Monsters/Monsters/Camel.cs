namespace Console_Monsters.Monsters;

internal class Camel : MonsterBase
{
	public Camel()
	{
		Name = "Camel";
	}

	public override string[] Sprite => (
		@"         ╭─╮  " + "\n" +
		@"         │^╰─╮" + "\n" +
		@"    ╭─╮  │ ╭─╯" + "\n" +
		@"╭╭──╯~╰──╯ │  " + "\n" +
		@"╯│        ╭╯  " + "\n" +
		@" │╭┬────┬╮│   " + "\n" +
		@" │││    │││   " + "\n" +
		@" ╰╯╯    ╰╰╯   ").Split('\n');
}
