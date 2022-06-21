namespace Checkers;

public class Piece
{
	public int XPosition { get; set; }

	public int YPosition { get; set; }

	public string NotationPosition
	{
		get => Board.ToPositionNotationString(XPosition, YPosition);
		set => (XPosition, YPosition) = Board.ParsePositionNotation(value);
	}

	public PieceColor Color { get; set; }

	public bool Promoted { get; set; }
}
