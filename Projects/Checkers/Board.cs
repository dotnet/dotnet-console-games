using Checkers.Helpers;

namespace Checkers;

public class Board
{
	public List<Piece> Pieces { get; set; }

	public Board()
	{
		Pieces = BoardHelper.GetStartingPosition();
	}

	public Board(List<Piece> startingPosition)
	{
		Pieces = startingPosition;
	}
}
