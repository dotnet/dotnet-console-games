namespace Console_Monsters.Monsters;

internal class CreepyHugger : MonsterBase
{
	public CreepyHugger()
	{
		Sprite = (
			@"   ╭───╮   " + '\n' +
			@"   │▪_▪│   " + '\n' +
			@"╭──╯ ▄ ╰──╮" + '\n' +
			@"╰─╭ ▀█▀ ╮─╯" + '\n' +
			@"  │ ╮─╭ │  " + '\n' +
			@"  ╰─╯ ╰─╯  ").Split('\n');
	}
}