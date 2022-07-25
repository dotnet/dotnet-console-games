namespace Console_Monsters.Items;

public class HealthPotionLarge : ItemBase
{
	public override string? Name => "Health Potion Large";

	public override string? Description => "Used to restore hp to monsters";

	public override ItemCategory Category => ItemCategory.Potions;

	public override string Sprite =>
		@" [╤═╤] " + "\n" +
		@" ╭╯ ╰╮ " + "\n" +
		@" │▄█▄│ " + "\n" +
		@" │ ▀ │ " + "\n" +
		@" ╰───╯ ";

	public static readonly HealthPotionLarge Instance = new();
}
