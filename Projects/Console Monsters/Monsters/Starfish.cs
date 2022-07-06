namespace Console_Monsters.Monsters;

internal class Starfish : MonsterBase
{
	public Starfish()
	{
		Name = "Starfish";
	}

	public override string[] Sprite => (
		@"    ╭◦╮    " + '\n' +
		@"    /◦\    " + '\n' +
		@"╭─◦╯^_^╰◦─╮" + '\n' +
		@"╰◦╮◦ ◦ ◦╭◦╯" + '\n' +
		@"  /◦/‾\◦\  " + '\n' +
		@" ╰◦╯   ╰◦╯ ").Split('\n');
}
