namespace Checkers;

public class Player
{
	public bool IsHuman { get; private set; }
	public PieceColor Color { get; private set; }

	public Player(bool isHuman, PieceColor color)
	{
		IsHuman = isHuman;
		Color = color;
	}
}
