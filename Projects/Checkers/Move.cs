namespace Checkers;

public class Move
{
	public Piece PieceToMove { get; set; }

	public (int X, int Y) To { get; set; }

	public (int X, int Y)? Capturing { get; set; }

	public Move(Piece piece, (int X, int Y) to, (int X, int Y)? capturing = null)
	{
		PieceToMove = piece;
		To = to;
		Capturing = capturing;
	}
}
