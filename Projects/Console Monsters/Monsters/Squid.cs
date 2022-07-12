namespace Console_Monsters.Monsters;

internal class Squid : MonsterBase
{
	public Squid()
	{
		Name = "Squid";
	}

	public override string[] Sprite => (
		@" ╭───╮  " + "\n" +
		@" │^_^│  " + "\n" +
		@"╭╰───╯─╮" + "\n" +
		@"│╭╮╭╮╭╮│" + "\n" +
		@"││││││││" + "\n" +
		@"╰╰╰╯╰╯╯╯").Split('\n');
}
