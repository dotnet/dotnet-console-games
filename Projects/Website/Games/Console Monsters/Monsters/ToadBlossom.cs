//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class ToadBlossom : MonsterBase
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