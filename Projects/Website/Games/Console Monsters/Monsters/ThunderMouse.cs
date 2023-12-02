//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

internal class ThunderMouse : MonsterBase
{
	public ThunderMouse()
	{
		Name = "Thunder Mouse";
	}

	public override string[] Sprite => (
		@"◄‾_>╭─────╮<_‾►  " + '\n' +
		@"    │▪^_^▪│      " + '\n' +
		@"    │╷╭─╮╷│      " + '\n' +
		@"    ├╯╰─╯╰┤◄/\/\>" + '\n' +
		@"    ╰─╯‾╰─╯      ").Split('\n');
}
