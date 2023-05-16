namespace Console_Monsters.Monsters;

internal class PsychicElephant : MonsterBase
{
	public PsychicElephant()
	{
		Name = "Psychic Elephant";
	}

	public override string[] Sprite => (
		@"   ╭────╮   " + "\n" +
		@"  <│^ ╮^│>  " + "\n" +
		@"╭╮╭╰─││─╯╮╭╮" + "\n" +
		@"╰─├ᴖᴗᴖᴗᴖᴗ┤─╯" + "\n" +
		@"  ╰┐ ┌┐ ┌╯  " + "\n" +
		@"   ╰─╯╰─╯   ").Split('\n');
}
