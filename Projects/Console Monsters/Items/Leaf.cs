namespace Console_Monsters.Items;

public class Leaf : ItemBase
{
	public override string? Name => "Leaf";

	public override string? Description => "It is a leaf";

	public override string Sprite =>
		@" .'|'. " + "\n" +
		@"/.'|\ \" + "\n" +
		@"| /|'.|" + "\n" +
		@" \ |\/ " + "\n" +
		@"  \|/  ";

	public static readonly Leaf Instance = new();
}
