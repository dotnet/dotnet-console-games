namespace Website.Games.Checkers;

public class Piece
{
	public int X { get; set; }

	public int Y { get; set; }

	public string NotationPosition
	{
		get => Board.ToPositionNotationString(X, Y);
		set => (X, Y) = Board.ParsePositionNotation(value);
	}

	public PieceColor Color { get; set; }

	public bool Promoted { get; set; }
}
