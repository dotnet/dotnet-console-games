namespace Checkers;

public class Game
{
	private const int PiecesPerColor = 12;

	public PieceColor Turn { get; private set; }
	public Board Board { get; private set; }
	public PieceColor? Winner { get; private set; }
	public List<Player> Players { get; private set; }

	public Game(int humanPlayerCount)
	{
		if (humanPlayerCount < 0 || 2 < humanPlayerCount) throw new ArgumentOutOfRangeException(nameof(humanPlayerCount));
		Board = new Board();
		Players = new()
		{
			new Player(humanPlayerCount >= 1, PieceColor.Black),
			new Player(humanPlayerCount >= 2, PieceColor.White),
		};
		Turn = PieceColor.Black;
		Winner = null;
	}

	public void NextTurn((int X, int Y)? from = null, (int X, int Y)? to = null)
	{
		MoveOutcome? moveOutcome = Engine.PlayNextMove(Turn, Board, from, to);
		while (from is null && to is null && moveOutcome is MoveOutcome.CaptureMoreAvailable)
		{
			moveOutcome = Engine.PlayNextMove(Turn, Board, from, to);
		}
		if (moveOutcome == MoveOutcome.BlackWin)
		{
			Winner = PieceColor.Black;
		}
		if (moveOutcome == MoveOutcome.WhiteWin)
		{
			Winner = PieceColor.White;
		}
		if (Winner is null && moveOutcome is not MoveOutcome.CaptureMoreAvailable)
		{
			CheckSidesHavePiecesLeft();
			Turn = Turn == PieceColor.Black ? PieceColor.White : PieceColor.Black;
		}
		if (moveOutcome is null)
		{
			Turn = Turn is PieceColor.Black ? PieceColor.White : PieceColor.Black;
		}
	}

	public void CheckSidesHavePiecesLeft()
	{
		if (!Board.Pieces.Any(piece => piece.Color is PieceColor.Black))
		{
			Winner = PieceColor.White;
		}
		if (!Board.Pieces.Any(piece => piece.Color is PieceColor.White))
		{
			Winner = PieceColor.Black;
		}
	}

	public int TakenCount(PieceColor colour) =>
		PiecesPerColor - Board.Pieces.Count(piece => piece.Color == colour);
}
