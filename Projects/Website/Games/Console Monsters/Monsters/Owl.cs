//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

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
