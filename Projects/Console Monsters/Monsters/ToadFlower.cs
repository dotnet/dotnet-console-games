namespace Console_Monsters.Monsters;

public class ToadFlower : MonsterBase
{
	public ToadFlower()
	{
		Name = "Toad Flower";
	}

	public override string[] Sprite => (
		@"    . . . .   . . . .    " + '\n' +
		@"     \~\~\~\~/~/~/~/     " + '\n' +
		@"      \ \╭─────╮/ /      " + '\n' +
		@"╔═╗ ╔══\ │ ‾o‾ │ /══╗ ╔═╗" + '\n' +
		@"  ╚═╝ ╭──╰─────╯──╮ ╚═╝  " + '\n' +
		@"      │  ╭─────╮  │      " + '\n' +
		@"      ╰──╯─╯ ╰─╰──╯      ").Split('\n');
}