namespace Console_Monsters.Monsters;

internal class Snake : MonsterBase
{
	public Snake()
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
