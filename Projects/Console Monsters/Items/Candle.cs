﻿namespace Console_Monsters.Items;

internal class Candle : ItemBase
{
	public override string? Name => "Candle";

	public override string? Description => "Portable light source";

	public override string Sprite =>
		@"   *   " + "\n" +
		@"  ┌┴┐  " + "\n" +
		@"  │ │  " + "\n" +
		@"  │ │  " + "\n" +
		@" ═╧═╧═ ";

	public static readonly Candle Instance = new();
}
