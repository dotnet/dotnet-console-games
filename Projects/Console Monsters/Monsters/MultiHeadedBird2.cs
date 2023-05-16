namespace Console_Monsters.Monsters;

public class MultiHeadedBird2 : MonsterBase
{
	public MultiHeadedBird2()
	{
		Name = "Three Headed Bird";
	}

	public override string[] Sprite => (
		@"     ╭─v─╮     " + "\n" +
		@"╭─v─╮│⁰v⁰│╭─v─╮" + "\n" +
		@"│⁰v⁰│╰─┬┬╯│⁰v⁰│" + "\n" +
		@"╰─┬┬╯  ││ ╰┬┬─╯" + "\n" +
		@"  ╰╰─╭─┴┴─╮╯╯  " + "\n" +
		@"     │    │    " + "\n" +
		@"     ╰╥──╥╯╮╮  " + "\n" +
		@"      ║  ║ ╰╰╰ " + "\n" +
		@"      ╝  ╚     ").Split('\n');
}
