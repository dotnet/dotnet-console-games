namespace Console_Monsters.Monsters;

internal class Owl : MonsterBase
{
	public Owl()
	{
		Name = "Owl";
	}

	public override string[] Sprite => (
		@" ╭\─/╮ " + '\n' +
		@"╭│⁰v⁰│╮" + '\n' +
		@"││( )││" + '\n' +
		@"╰╰┬─┬╯╯" + '\n' +
		@"  ^ ^  ").Split('\n');
}
