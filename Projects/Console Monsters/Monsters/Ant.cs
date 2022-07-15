namespace Console_Monsters.Monsters;

public class Ant : MonsterBase
{
	public Ant()
	{
		Name = "Ant";
	}

	public override string[] Sprite => (
		@"      ─┐ ┌─" + '\n' +
		@"      ╭┴─┴╮" + '\n' +
		@"╭──╭──│^_^│" + '\n' +
		@"╰┬┬╰┬┬╰┬─┬╯" + '\n' +
		@" └└ └└ └ └ ").Split('\n');
}
