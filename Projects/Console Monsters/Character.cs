namespace Console_Monsters;

public class Character
{
	/// <summary>Horizontal position in pixel coordinates.</summary>
	public int I { get; set; }
	/// <summary>Vertical position in pixel coordinates.</summary>
	public int J { get; set; }
	/// <summary>Currently active animation.</summary>
	public string[] Animation { get; set; } = Sprites.IdlePlayer;
	/// <summary>The current frame of the active animation.</summary>
	public int AnimationFrame {get; set; } = 0;
	/// <summary>The render state of the character based on the <see cref="Animation"/> and <see cref="AnimationFrame"/>.</summary>
	public string Render => Animation[AnimationFrame % Animation.Length];
}
