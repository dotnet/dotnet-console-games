namespace Console_Monsters.Monsters;

internal class FireLizard2 : MonsterBase
{
	public FireLizard2()
	{
		Sprite = (
			@"╰╮       ╭───╮" + '\n' +
			@"╰╮╰╮     │o_o│" + '\n' +
			@"╰╮╰╮╰╮ ╭─╯  ╭╯" + '\n' +
			@"   ╰╮╰─╯╰─╯╭╯ " + '\n' +
			@"    ╰──┬╮ ╭╯  " + '\n' +
			@"       ╰╰─╯   ").Split('\n');
		Name = "Fire Lizard Medium";
	}
}