namespace Console_Monsters.Monsters;

internal class ToadBlossom : MonsterBase
{
	public ToadBlossom()
	{
		Name = "Toad Blossom";
	}

	public override string[] Sprite => (
		@"     \~\~\~|~/~/~/     " + '\n' +
		@"      \ ╭─────╮ /      " + '\n' +
		@"┌─┐ ┌──\│ o_o │/──┐ ┌─┐" + '\n' +
		@"  └─┘ ╭─╰─────╯─╮ └─┘  " + '\n' +
		@"      │ ╭─────╮ │      " + '\n' +
		@"      ╰─╯─╯ ╰─╰─╯      ").Split('\n');
}