namespace Console_Monsters.Monsters;

internal class ToadFlower : MonsterBase
{
	public ToadFlower()
	{
		Sprite = (
			@"       . . . .   . . . .        " + '\n' +
			@"        \~\~\~\~/~/~/~/         " + '\n' +
			@"         \ \╭─────╮/ /          " + '\n' +
			@"   ╔═╗ ╔══\ │ ‾o‾ │ /══╗ ╔═╗    " + '\n' +
			@"     ╚═╝ ╭──╰─────╯──╮ ╚═╝      " + '\n' +
			@"         │  ╭─────╮  │          " + '\n' +
			@"         ╰──╯─╯ ╰─╰──╯          ").Split('\n');
	}
}