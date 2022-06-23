namespace Checkers;

public class Move
{
	public Piece PieceToMove { get; set; }

	public (int X, int Y) To { get; set; }

	public Piece? PieceToCapture { get; set; }

	public Move(Piece pieceToMove, (int X, int Y) to, Piece? pieceToCapture = null)
	{
		PieceToMove = pieceToMove;
		To = to;
		PieceToCapture = pieceToCapture;
	}
}
