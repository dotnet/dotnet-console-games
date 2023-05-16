namespace Console_Monsters.Monsters;

public class FireLizard2 : MonsterBase
{
	public FireLizard2()
	{
		Name = "Fire Reptile";
	}

	public override string[] Sprite => (
		"╰╮       ╭───╮" + '\n' +
		"╰╮╰╮     │o_o│" + '\n' +
		"╰╮╰╮╰╮ ╭─╯  ╭╯" + '\n' +
		"   ╰╮╰─╯╰─╯╭╯ " + '\n' +
		"    ╰──┬╮ ╭╯  " + '\n' +
		"       ╰╰─╯   ").Split('\n');
}