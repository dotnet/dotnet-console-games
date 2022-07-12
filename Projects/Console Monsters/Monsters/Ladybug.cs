namespace Console_Monsters.Monsters;

public class Ladybug : MonsterBase
{
	public Ladybug()
	{
		Name = "Ladybug";
	}

	public override string[] Sprite => (
		@" ╭──┐_┌──╮ " + '\n' +
		@"┌│●.│ │․●│┐" + '\n' +
		@"┌│●╭╨─╨╮●│┐" + '\n' +
		@"┌╰─┤^_^├─╯┐" + '\n' +
		@"   ╰───╯   ").Split('\n');
}
