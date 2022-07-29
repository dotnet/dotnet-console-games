namespace Console_Monsters.Monsters;

public class MultiHeadedBird1 : MonsterBase
{
	public MultiHeadedBird1()
	{
		Name = "Two Headed Bird";
	}

	public override string[] Sprite => (
		@" ╭───╮   ╭───╮" + "\n" +
		@" │⁰v⁰│   │⁰v⁰│" + "\n" +
		@" ╰──┬╯   ╰┬──╯" + "\n" +
		@"    ╰╭───╮╯   " + "\n" +
		@"     │   │    " + "\n" +
		@"     ╰┬─┬╯    " + "\n" +
		@"      │ │     " + "\n" +
		@"      ┘ └     ").Split('\n');
}
