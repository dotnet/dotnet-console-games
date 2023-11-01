//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Items;

public class HealthPotionLarge : ItemBase
{
	public override string? Name => "Health Potion Large";

	public override string? Description => "Used to restore hp to monsters";

	public override string Sprite =>
		@" [╤═╤] " + "\n" +
		@" ╭╯ ╰╮ " + "\n" +
		@" │▄█▄│ " + "\n" +
		@" │ ▀ │ " + "\n" +
		@" ╰───╯ ";

	public static readonly HealthPotionLarge Instance = new();
}
