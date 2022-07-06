namespace Console_Monsters;

public class Player
{
	/// <summary>Horizontal position in pixel coordinates.</summary>
	public int I { get; set; }
	/// <summary>Vertical position in pixel coordinates.</summary>
	public int J { get; set; }
	/// <summary>Currently active animation.</summary>
	public string[] Animation { get; set; } = IdleDown;
	/// <summary>The current frame of the active animation.</summary>
	public int AnimationFrame {get; set; } = 0;
	/// <summary>The render state of the character based on the <see cref="Animation"/> and <see cref="AnimationFrame"/>.</summary>
	public string Render => Animation[AnimationFrame % Animation.Length];

	public bool IsIdle =>
		Animation == IdleDown ||
		Animation == IdleUp   ||
		Animation == IdleLeft ||
		Animation == IdleRight;


	#region Player Sprites

	private static readonly PlayerData PlayerData = new();

	// Run animation
	public static readonly string[] RunRight = PlayerData.Animations["RunRight"];
	
	public static readonly string[] RunLeft = PlayerData.Animations["RunLeft"];
	
	public static readonly string[] RunDown = PlayerData.Animations["RunDown"];
	
	public static readonly string[] RunUp = PlayerData.Animations["RunUp"];

	// Idle Animation
	public static readonly string IdleLeft1 = PlayerData.Animations["IdleLeft1"].First();
	public static readonly string IdleLeft2 = PlayerData.Animations["IdleLeft2"].First();
	
	public static readonly string[] IdleLeft =
		Enumerable.Repeat(IdleLeft1, 100).Concat(Enumerable.Repeat(IdleLeft2, 10)).ToArray();
	
	public static readonly string IdleRight1 = PlayerData.Animations["IdleRight1"].First();
	public static readonly string IdleRight2 = PlayerData.Animations["IdleRight2"].First();
	
	public static readonly string[] IdleRight =
		Enumerable.Repeat(IdleRight1, 100).Concat(Enumerable.Repeat(IdleRight2, 10)).ToArray();

	public static readonly string IdleUp1 = PlayerData.Animations["IdleUp1"].First();

	#warning TODO: need another idle sprite
	public static readonly string[] IdleUp =
		Enumerable.Repeat(IdleUp1, 100).Concat(Enumerable.Repeat(IdleUp1, 10)).ToArray();

	public static readonly string IdleDown1 = PlayerData.Animations["IdleDown1"].First();
	public static readonly string IdleDown2 = PlayerData.Animations["IdleDown2"].First();

	public static readonly string[] IdleDown =
		Enumerable.Repeat(IdleDown1, 100).Concat(Enumerable.Repeat(IdleDown2, 10)).ToArray();

	#endregion
}
