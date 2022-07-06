namespace Console_Monsters.Items;

public class ExperienceBerries : ItemBase
{
	public override string? Name => "Experience Berries";

	public override string? Description => "Used to increase a monsters experience";

	public override string Sprite =>
		@"   \   " + "\n" +
		@" ()(() " + "\n" +
		@"()()())" + "\n" +
		@" (()() " + "\n" +
		@"  ())  ";

	public static readonly ExperienceBerries Instance = new();
}
