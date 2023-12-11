//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Items;

public class HealthPotionMedium : ItemBase
{
	public override string? Name => "Health Potion Medium";

	public override string? Description => "Used to restore hp to monsters";

	public override string Sprite =>
		@" [╤═╤] " + "\n" +
		@" ╭╯ ╰╮ " + "\n" +
		@" │╺╋╸│ " + "\n" +
		@" ╰───╯ " + "\n" +
		@"       ";

	public static readonly HealthPotionMedium Instance = new();
}
