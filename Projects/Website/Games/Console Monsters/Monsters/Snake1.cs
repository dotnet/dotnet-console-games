//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class Snake1 : MonsterBase
{
	public Snake1()
	{
		Name = "Snake";
	}

	public override string[] Sprite => (
		"           ╭────╮" + '\n' +
		"           │ ^_^│" + '\n' +
		"╭───╮ ╭───╮╰─╮╭─╯" + '\n' +
		"││‾││_││‾││__││  " + '\n' +
		"╰╯ ╰───╯ ╰────╯  ").Split('\n');
}
