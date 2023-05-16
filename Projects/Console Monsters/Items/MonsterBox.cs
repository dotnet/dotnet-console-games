namespace Console_Monsters.Items;

public class MonsterBox : ItemBase
{
	public override string? Name => "Monster Box";

	public override string? Description => "Used to catch monsters";

	public override ItemCategory Category => ItemCategory.MonsterBoxes;

	public override string Sprite => Sprites.MonsterBox;

	public static readonly MonsterBox Instance = new();
}
