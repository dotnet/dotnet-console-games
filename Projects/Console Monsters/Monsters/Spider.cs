namespace Console_Monsters.Monsters;

internal class Spider : MonsterBase
{
	public Spider()
	{
		Name = "Spider";
	}

	public override string[] Sprite => (
		@"╔═╭─────╮═╗" + "\n" +
		@"╔═│╭───╮│═╗" + "\n" +
		@"╔═╰│^ ^│╯═╗" + "\n" +
		@"   ╰╥═╥╯   " + "\n" +
		@"    ╰ ╯    ").Split('\n');
}
