//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Items;

public class MonsterBox : ItemBase
{
	public override string? Name => "Monster Box";

	public override string? Description => "Used to catch monsters";

	public override string Sprite => Sprites.MonsterBox;

	public static readonly MonsterBox Instance = new();
}
