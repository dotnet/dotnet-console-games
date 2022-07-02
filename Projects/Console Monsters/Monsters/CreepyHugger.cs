namespace Console_Monsters.Monsters;

public class CreepyHugger : MonsterBase
{
	public CreepyHugger()
	{
		Name = "Creepy Hugger";
	}

	public override string[] Sprite => (
		"   ╭───╮   " + '\n' +
		"   │▪_▪│   " + '\n' +
		"╭──╯╭─╮╰──╮" + '\n' +
		"╰─╭ ╰─╯ ╮─╯" + '\n' +
		"  │ ╮─╭ │  " + '\n' +
		"  ╰─╯ ╰─╯  ").Split('\n');
}