﻿namespace Console_Monsters.Items;

public class HealthPotionSmall : ItemBase
{
	public override string? Name => "Health Potion Small";

	public override string? Description => "Used to restore hp to monsters";

	public override string Sprite =>
		@" [╤╤]  " + "\n" +
		@" ╭╯╰╮  " + "\n" +
		@" │+ │  " + "\n" +
		@" ╰──╯  " + "\n" +
		@"       ";

	public static readonly HealthPotionSmall Instance = new();
}
