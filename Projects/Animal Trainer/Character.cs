namespace Animal_Trainer;

public class Character
{
	// I & J represent the character's position in pixel coordinates
	// relative to the current map
	public int I { get; set; }
	public int J { get; set; }
	// TileI and TileJ represent the character's position in tile coordinates
	// relative to the current map
	public int TileI => I < 0 ? (I - 6) / 7 : I / 7;
	public int TileJ => J < 0 ? (J - 4) / 5 : J / 5;
	private string[]? _mapAnaimation;
	public string[]? MapAnimation
	{
		get => _mapAnaimation;
		set
		{
			_mapAnaimation = value;
			_mapAnimationFrame = 0;
		}
	}
	private int _mapAnimationFrame;
	public int MapAnimationFrame
	{
		get => _mapAnimationFrame;
		set
		{
			_mapAnimationFrame = value;
			Moved = false;
			if ((MapAnimation == Sprites.RunUp    && _mapAnimationFrame is 5) ||
				(MapAnimation == Sprites.RunDown  && _mapAnimationFrame is 5) ||
				(MapAnimation == Sprites.RunLeft  && _mapAnimationFrame is 7) ||
				(MapAnimation == Sprites.RunRight && _mapAnimationFrame is 7))
			{
				Moved = true;
				MapAnimation = Sprites.IdlePlayer;
				_mapAnimationFrame = 0;
			}
		}
	}

	public bool IsIdle => _mapAnaimation == Sprites.IdlePlayer;

	public string Render => _mapAnaimation[_mapAnimationFrame % _mapAnaimation.Length];

	public bool Moved { get; set; } = false;
}
