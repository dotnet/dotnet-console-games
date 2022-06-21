namespace Checkers;

public class Player
{
	public PlayerControl ControlledBy { get; private set; }
	public PieceColor Color { get; private set; }

	public Player(PlayerControl controlledBy, PieceColor color)
	{
		ControlledBy = controlledBy;
		Color = color;
	}
}
