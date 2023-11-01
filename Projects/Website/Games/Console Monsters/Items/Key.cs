//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Items;

public class Key : ItemBase
{
	public override string? Name => "Key";

	public override string? Description => "It opens something";

	public override string Sprite =>
		@"  ═╗   " + "\n" +
		@"  ═╣   " + "\n" +
		@"   ║   " + "\n" +
		@"  ╔╩╗  " + "\n" +
		@"  ╚═╝  ";

	public static readonly Key Instance = new();
}
