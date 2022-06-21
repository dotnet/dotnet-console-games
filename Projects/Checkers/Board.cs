using Checkers.Data;

namespace Checkers;

public class Board
{
	public List<Piece> Pieces { get; set; }

	public Board()
	{
		Pieces = GetStartingPosition();
	}

	public Board(List<Piece> startingPosition)
	{
		Pieces = startingPosition;
	}

	public static List<Piece> GetStartingPosition()
	{
		// HACK: Can set to false and define a custom start position in GetLimitedStartingPosition
		const bool UseDefault = true;

#pragma warning disable CS0162
		return UseDefault ? GetDefaultStartingPosition() : GetLimitedStartingPosition();
#pragma warning restore CS0162
	}

	private static List<Piece> GetLimitedStartingPosition() =>
		KnowledgeBase.GetLimitedStartingPosition();

	public static List<Piece> GetDefaultStartingPosition() =>
		KnowledgeBase.GetStartingPosition();

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
