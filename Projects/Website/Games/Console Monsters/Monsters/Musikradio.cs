//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class Musikradio : MonsterBase
{
	public Musikradio()
	{
		Name = "MusikRadio";
	}

	public override string[] Sprite => (
		"╭──♫♫♫──╮" + '\n' +
		"│♪ ^_^ ♪│" + '\n' +
		"╰───◌───╯").Split('\n');
}
