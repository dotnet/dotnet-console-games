namespace Console_Monsters.Monsters;

internal class ToadBlossom : MonsterBase
{
	public ToadBlossom()
	{
		Sprite = (
			@"     \~\~\~|~/~/~/     " + '\n' +
			@"      \ ╭─────╮ /      " + '\n' +
			@"┌─┐ ┌──\│ o_o │/──┐ ┌─┐" + '\n' +
			@"  └─┘ ╭─╰─────╯─╮ └─┘  " + '\n' +
			@"      │ ╭─────╮ │      " + '\n' +
			@"      ╰─╯─╯ ╰─╰─╯      ").Split('\n');
		Name = "Toad Blossom";
	}
}