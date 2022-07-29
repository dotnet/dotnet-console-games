namespace Console_Monsters.Monsters;

internal class BigMouthTurtle : MonsterBase
{
	public BigMouthTurtle()
	{
		Name = "Big Mouth Turtle";
	}

	public override string[] Sprite => (
		@"      ╭─────╮" + "\n" +
		@"╭───╮_│⁰ ˄˄˄┤" + "\n" +
		@"╰U─U╯‾╰─────╯").Split('\n');
}
