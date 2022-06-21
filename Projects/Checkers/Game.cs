namespace Checkers;

public class Game
{
	private const int PiecesPerSide = 12;

	public PieceColor Turn { get; private set; }
	public Board Board { get; private set; }
	public int MoveCount { get; private set; }
	public PieceColor? Winner { get; private set; }
	public List<Player> Players { get; private set; }

	public Game(int humanPlayerCount)
	{
		if (humanPlayerCount < 0 || 2 < humanPlayerCount) throw new ArgumentOutOfRangeException(nameof(humanPlayerCount));
		Board = new Board();
		Players = new()
		{
			new Player { ControlledBy = humanPlayerCount < 1 ? PlayerControl.Computer : PlayerControl.Human, Color = PieceColor.Black },
			new Player { ControlledBy = humanPlayerCount < 2 ? PlayerControl.Computer : PlayerControl.Human, Color = PieceColor.White },
		};
		Turn = PieceColor.Black;
		MoveCount = 0;
		Winner = null;
	}

	public MoveOutcome NextRound((int X, int Y)? from = null, (int X, int Y)? to = null)
	{
		MoveCount++;
		MoveOutcome res = Engine.PlayNextMove(Turn, Board, from, to);
		while (from is null && to is null && res is MoveOutcome.CaptureMoreAvailable)
		{
			res = Engine.PlayNextMove(Turn, Board, from, to);
		}
		if (res == MoveOutcome.BlackWin)
		{
			Winner = PieceColor.Black;
		}
		if (res == MoveOutcome.WhiteWin)
		{
			Winner = PieceColor.White;
		}
		if (Winner is null && res is not MoveOutcome.CaptureMoreAvailable)
		{
			CheckSidesHavePiecesLeft();
			Turn = Turn == PieceColor.Black ? PieceColor.White : PieceColor.Black;
		}
		if (res == MoveOutcome.Unknown)
		{
			Turn = Turn == PieceColor.Black
				? PieceColor.White
				: PieceColor.Black;
		}
		return res;
	}

	public void CheckSidesHavePiecesLeft()
	{
		bool retVal = Board.Pieces.Select(piece => piece.Color).Distinct().Count() == 2;
		if (!retVal)
		{
			Winner = Board.Pieces.Select(piece => piece.Color).Distinct().FirstOrDefault();
		}
	}

	public string GetCurrentPlayer()
	{
		return Turn.ToString();
	}

	public int GetWhitePiecesTaken()
	{
		return GetPiecesTakenForSide(PieceColor.White);
	}

	public int GetBlackPiecesTaken()
	{
		return GetPiecesTakenForSide(PieceColor.Black);
	}

	public int GetPiecesTakenForSide(PieceColor colour)
	{
		return PiecesPerSide - Board.Pieces.Count(piece => piece.Color == colour);
	}
}
