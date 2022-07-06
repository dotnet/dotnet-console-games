namespace Console_Monsters.Monsters;

public class Magneto : MonsterBase
{
	public Magneto()
	{
		Name = "Magneto";
	}

	public override string[] Sprite => (
		"  ╮     ╭  " + '\n' +
		"╮ ╮╮╰ ╯╭╭ ╭" + '\n' +
		"╯ ╯╯ O ╰╰ ╰" + '\n' +
		"  ╯     ╰  ").Split('\n');
}
