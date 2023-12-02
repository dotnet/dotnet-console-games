//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Fox : MonsterBase
{
	public Fox()
	{
		Name = "Fox";
	}

	public override string[] Sprite => (
		@" /\                  " + '\n' +
		@"/~~\  <‾__>╭───╮<__‾>" + '\n' +
		@"\   \      │^_^│     " + '\n' +
		@" \___>╭────╯~~┬╯     " + '\n' +
		@"      │ ╭┬──╮ ││     " + '\n' +
		@"      ╰─╯╯  ╰─╯╯     ").Split('\n');
}
