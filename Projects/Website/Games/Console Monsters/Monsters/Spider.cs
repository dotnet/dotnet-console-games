//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

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
