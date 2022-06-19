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
}
