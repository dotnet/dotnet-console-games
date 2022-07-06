namespace Console_Monsters.Monsters;

internal class Bat : MonsterBase
{
	public Bat()
	{
		Name = "Bat";
	}

	public override string[] Sprite => (
		@"       ◦╮ ╭◦       " + '\n' +
		@" /‾‾‾\ ╭┴─┴╮ /‾‾‾\ " + '\n' +
		@"/ ( ( >│^_^│< ) ) \" + '\n' +
		@"\/\/\/ ╰┬─┬╯ \/\/\/" + '\n' +
		@"        │ │        " + '\n' +
		@"        ╹ ╹        ").Split('\n');
}
