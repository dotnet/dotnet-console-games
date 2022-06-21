namespace Checkers;

public class Board
{
	public List<Piece> Pieces { get; set; }

	public Board()
	{
		Pieces = new List<Piece>
			{
				new() { NotationPosition ="A3", Side = PieceColour.Black},
				new() { NotationPosition ="A1", Side = PieceColour.Black},
				new() { NotationPosition ="B2", Side = PieceColour.Black},
				new() { NotationPosition ="C3", Side = PieceColour.Black},
				new() { NotationPosition ="C1", Side = PieceColour.Black},
				new() { NotationPosition ="D2", Side = PieceColour.Black},
				new() { NotationPosition ="E3", Side = PieceColour.Black},
				new() { NotationPosition ="E1", Side = PieceColour.Black},
				new() { NotationPosition ="F2", Side = PieceColour.Black},
				new() { NotationPosition ="G3", Side = PieceColour.Black},
				new() { NotationPosition ="G1", Side = PieceColour.Black},
				new() { NotationPosition ="H2", Side = PieceColour.Black},

				new() { NotationPosition ="A7", Side = PieceColour.White},
				new() { NotationPosition ="B8", Side = PieceColour.White},
				new() { NotationPosition ="B6", Side = PieceColour.White},
				new() { NotationPosition ="C7", Side = PieceColour.White},
				new() { NotationPosition ="D8", Side = PieceColour.White},
				new() { NotationPosition ="D6", Side = PieceColour.White},
				new() { NotationPosition ="E7", Side = PieceColour.White},
				new() { NotationPosition ="F8", Side = PieceColour.White},
				new() { NotationPosition ="F6", Side = PieceColour.White},
				new() { NotationPosition ="G7", Side = PieceColour.White},
				new() { NotationPosition ="H8", Side = PieceColour.White},
				new() { NotationPosition ="H6", Side = PieceColour.White}
			};
	}

	public PieceColour GetSquareOccupancy(int x, int y) =>
		GetPieceAt(x, y)?.Side ?? default;

	public Piece? GetPieceAt(int x, int y) =>
		Pieces.FirstOrDefault(p => p.XPosition == x && p.InPlay && p.YPosition == y);

	public int GetNumberOfWhitePiecesInPlay() =>
		GetNumberOfPiecesInPlay(PieceColour.White);

	public int GetNumberOfBlackPiecesInPlay() =>
		GetNumberOfPiecesInPlay(PieceColour.Black);

	private int GetNumberOfPiecesInPlay(PieceColour currentSide) =>
		Pieces.Count(x => x.Side == currentSide && x.InPlay);

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
		return (notation[0] - 'A', '8' - notation[1]);
	}

	public static bool IsValidPosition(int x, int y) =>
		0 <= x && x < 8 &&
		0 <= y && y < 8;
}
