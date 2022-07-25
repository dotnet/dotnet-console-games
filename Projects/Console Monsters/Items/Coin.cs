namespace Console_Monsters.Items;

public class Coin : ItemBase
{
	public override string? Name => "Coin";

	public override string? Description => "Used to buy Items";

	public override string Sprite =>
		@"       " + "\n" +
		@"       " + "\n" +
		@"   $   " + "\n" +
		@"       " + "\n" +
		@"       ";

	public static readonly Coin Instance = new Coin();
}
