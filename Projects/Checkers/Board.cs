namespace Checkers;

public class Board
{
	public List<Piece> Pieces { get; set; }

	public Board()
	{
		Pieces = new List<Piece>
			{
				new() { NotationPosition ="A3", Color = PieceColor.Black},
				new() { NotationPosition ="A1", Color = PieceColor.Black},
				new() { NotationPosition ="B2", Color = PieceColor.Black},
				new() { NotationPosition ="C3", Color = PieceColor.Black},
				new() { NotationPosition ="C1", Color = PieceColor.Black},
				new() { NotationPosition ="D2", Color = PieceColor.Black},
				new() { NotationPosition ="E3", Color = PieceColor.Black},
				new() { NotationPosition ="E1", Color = PieceColor.Black},
				new() { NotationPosition ="F2", Color = PieceColor.Black},
				new() { NotationPosition ="G3", Color = PieceColor.Black},
				new() { NotationPosition ="G1", Color = PieceColor.Black},
				new() { NotationPosition ="H2", Color = PieceColor.Black},

				new() { NotationPosition ="A7", Color = PieceColor.White},
				new() { NotationPosition ="B8", Color = PieceColor.White},
				new() { NotationPosition ="B6", Color = PieceColor.White},
				new() { NotationPosition ="C7", Color = PieceColor.White},
				new() { NotationPosition ="D8", Color = PieceColor.White},
				new() { NotationPosition ="D6", Color = PieceColor.White},
				new() { NotationPosition ="E7", Color = PieceColor.White},
				new() { NotationPosition ="F8", Color = PieceColor.White},
				new() { NotationPosition ="F6", Color = PieceColor.White},
				new() { NotationPosition ="G7", Color = PieceColor.White},
				new() { NotationPosition ="H8", Color = PieceColor.White},
				new() { NotationPosition ="H6", Color = PieceColor.White}
			};
	}

	public PieceColor GetSquareOccupancy(int x, int y) =>
		GetPieceAt(x, y)?.Color ?? default;

	public Piece? GetPieceAt(int x, int y) =>
		Pieces.FirstOrDefault(piece => piece.XPosition == x && piece.YPosition == y);

	public int GetNumberOfWhitePiecesInPlay() =>
		GetNumberOfPiecesInPlay(PieceColor.White);

	public int GetNumberOfBlackPiecesInPlay() =>
		GetNumberOfPiecesInPlay(PieceColor.Black);

	private int GetNumberOfPiecesInPlay(PieceColor currentSide) =>
		Pieces.Count(piece => piece.Color == currentSide);

	public static string ToPositionNotationString(int x, int y)
	{
		if (!IsValidPosition(x, y)) throw new ArgumentException("Not a valid position!");
		return $"{(char)('A' + x)}{y + 1}";
	}

	public static (int X, int Y) ParsePositionNotation(string notation)
	{
		if (notation is null) throw new ArgumentNullException(nameof(notation));
		notation = notation.Trim().ToUpper();
		if (notation.Length is not 2 ||
			notation[0] < 'A' || 'H' < notation[0] ||
			notation[1] < '1' || '8' < notation[1])
			throw new FormatException($@"{nameof(notation)} ""{notation}"" is not valid");
		return (notation[0] - 'A', notation[1] - '1');
	}

	public static bool IsValidPosition(int x, int y) =>
		0 <= x && x < 8 &&
		0 <= y && y < 8;
}
