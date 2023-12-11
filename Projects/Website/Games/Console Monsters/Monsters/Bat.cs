//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class Bat : MonsterBase
{
	public Bat()
	{
		Name = "Bat";
	}

	public override string[] Sprite => (
		@"       ◦╮ ╭◦       " + '\n' +
		@" /‾‾‾\ ╭┴─┴╮ /‾‾‾\ " + '\n' +
		@"/ ( ( >│^_^│< ) ) \" + '\n' +
		@"\/\/\/ ╰┬─┬╯ \/\/\/" + '\n' +
		@"        │ │        " + '\n' +
		@"        ╹ ╹        ").Split('\n');
}
