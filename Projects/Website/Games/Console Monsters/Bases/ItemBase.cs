//using Website.Games.Console_Monsters.Screens;

namespace Website.Games.Console_Monsters.Bases;

public abstract class ItemBase
{
	public abstract string? Name { get; }

	public abstract string? Description { get; }

	public abstract string Sprite { get; }

	// we may want to change the following but doing this for now...

	public static bool operator ==(ItemBase a, ItemBase b) => a.GetType() == b.GetType();
	public static bool operator !=(ItemBase a, ItemBase b) => !(a == b);
	public override bool Equals(object? obj) => obj is ItemBase item && this == item;
	public override int GetHashCode() => this.GetType().GetHashCode();
}
