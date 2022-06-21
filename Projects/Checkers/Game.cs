namespace Checkers;

public class Game
{
	private const int PiecesPerSide = 12;

	public PieceColor Turn { get; set; }
	public Board Board { get; }
	public int MoveCount { get; private set; }
	public PieceColor Winner { get; set; } = PieceColor.NotSet;

	public List<Player> Players { get; set; } = new()
		{
			new Player { ControlledBy = PlayerControl.Computer, Color = PieceColor.Black },
			new Player { ControlledBy = PlayerControl.Computer, Color = PieceColor.White }
		};

	public Game()
	{
		Board = new Board();
		Turn = PieceColor.Black;
	}

	public MoveOutcome NextRound((int X, int Y)? from = null, (int X, int Y)? to = null)
	{
		MoveCount++;
		var res = Engine.PlayNextMove(Turn, Board, from, to);

		while (from == null & to == null && res == MoveOutcome.CaptureMoreAvailable)
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

		if (Winner == PieceColor.NotSet && res != MoveOutcome.CaptureMoreAvailable)
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
		var retVal = Board.Pieces.Select(y => y.Color).Distinct().Count() == 2;

		if (!retVal)
		{
			Winner = Board.Pieces.Select(y => y.Color).Distinct().FirstOrDefault();
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
		return PiecesPerSide - Board.Pieces.Count(x => x.Color == colour);
	}
}
