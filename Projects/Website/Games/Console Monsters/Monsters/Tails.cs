//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Tails : MonsterBase
{
	public Tails()
	{
		Name = "Fox";
	}

	public override string[] Sprite => (
		@"  ┌─┐─┐       ┐┐┐   " + '\n' +
		@" ┌─╲ ╲╲─┐  <>╭\\\╮<>" + '\n' +
		@"┌─╲ \ \\╲┐   │^_^│  " + '\n' +
		@" ╲ \   >╭────╯  ┬╯  " + '\n' +
		@"  \___/ ├~╭┬──╮~┤│  " + '\n' +
		@"        ╰─╯╯  ╰─╯╯  ").Split('\n');
}
