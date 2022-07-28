namespace Console_Monsters.Monsters;

public class Eggs : MonsterBase
{
	public Eggs()
	{
		Name = "Eggs";
	}

	public override string[] Sprite => (
		@"         ╭┬──╮  ╭┌┬─╮" + "\n" +
		@"  ┌˄^˄┐  │ˋ˾ˊ┤  │▾˾▾│" + "\n" +
		@"╭─┴┬╮─╯╭─┴─╮┴╯╭─┴┐╮─╯" + "\n" +
		@"├▴˾▴│  │^_^│  │▸˾◂│  " + "\n" +
		@"╰┴──╯  ╰───╯  ╰──┴╯  ").Split('\n');
}
