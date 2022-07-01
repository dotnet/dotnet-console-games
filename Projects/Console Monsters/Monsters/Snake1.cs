namespace Console_Monsters.Monsters;

internal class Snake1 : MonsterBase
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
