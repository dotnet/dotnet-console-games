namespace Checkers;

public class Move
{
	public Piece? PieceToMove { get; set; }

	public (int X, int Y) To { get; set; }

	public (int X, int Y)? Capturing { get; set; }

	public MoveType TypeOfMove { get; set; } = MoveType.Unknown;
}
