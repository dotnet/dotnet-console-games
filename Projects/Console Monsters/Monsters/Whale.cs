namespace Console_Monsters.Monsters;

public class Whale : MonsterBase
{
	public Whale()
	{
		Name = "Whale";
	}

	public override string[] Sprite => (
		@" __    ______ " + '\n' +
		@"╵╮ ╲__╱   ^_^╲" + '\n' +
		@"╷╯ ╱──╲ ╰─╯  ╱" + '\n' +
		@" ‾‾    ‾‾‾‾‾‾ ").Split('\n');
}
