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
			new Player(humanPlayerCount >= 1, Black),
			new Player(humanPlayerCount >= 2, White),
		};
		Turn = Black;
		Winner = null;
	}

	public void NextTurn((int X, int Y)? from = null, (int X, int Y)? to = null)
	{
		MoveOutcome? moveOutcome = Engine.PlayNextMove(Turn, Board, from, to);
		while (from is null && to is null && moveOutcome is MoveOutcome.CaptureMoreAvailable)
		{
			moveOutcome = Engine.PlayNextMove(Turn, Board, from, to);
		}
		if (moveOutcome is MoveOutcome.BlackWin)
		{
			Winner = Black;
		}
		if (moveOutcome is MoveOutcome.WhiteWin)
		{
			Winner = White;
		}
		if (Winner is null && moveOutcome is not MoveOutcome.CaptureMoreAvailable)
		{
			CheckColorsHavePiecesLeft();
			Turn = Turn is Black ? White : Black;
		}
		if (moveOutcome is null)
		{
			Turn = Turn is Black ? White : Black;
		}
	}

	public void CheckColorsHavePiecesLeft()
	{
		if (!Board.Pieces.Any(piece => piece.Color is Black))
		{
			Winner = White;
		}
		if (!Board.Pieces.Any(piece => piece.Color is White))
		{
			Winner = Black;
		}
	}

	public int TakenCount(PieceColor colour) =>
		PiecesPerColor - Board.Pieces.Count(piece => piece.Color == colour);
}
