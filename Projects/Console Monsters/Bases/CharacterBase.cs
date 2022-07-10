namespace Console_Monsters.Bases;

public abstract class CharacterBase : InteractableBase
{
	public abstract string? Name { get; }

	public string[]? Dialogue { get; set; }

	public string Sprite { get; set; } = Sprites.Error;

	public string[]? SpriteAnimation { get; set; }
}
