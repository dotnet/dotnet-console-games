namespace Console_Monsters.Monsters;

public class Goat : MonsterBase
{
	public Goat()
	{
		Name = "Goat";
	}

	public override string[] Sprite => (
		@"     ╭╭╮_   " + '\n' +
		@"     ╰╰╷^╰─╮" + '\n' +
		@"*~~~~~~╭───╯" + '\n' +
		@" ~~~~~~~    " + '\n' +
		@" ╰╯╯ ╰╰╯    ").Split('\n');
}
