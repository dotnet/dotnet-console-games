using System.Drawing;

namespace Checkers;

public class Game
{
	private const int PiecesPerSide = 12;

	public PieceColour Turn { get; set; }
	public Board Board { get; }
	public int MoveCount { get; private set; }
	public PieceColour Winner { get; set; } = PieceColour.NotSet;

	public List<Player> Players { get; set; } = new()
		{
			new Player { ControlledBy = PlayerControl.Computer, Side = PieceColour.Black },
			new Player { ControlledBy = PlayerControl.Computer, Side = PieceColour.White }
		};

	public Game()
	{
		Board = new Board();
		Turn = PieceColour.Black;
		//ShowBoard();
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
			Winner = PieceColour.Black;
		}

		if (res == MoveOutcome.WhiteWin)
		{
			Winner = PieceColour.White;
		}

		if (Winner == PieceColour.NotSet && res != MoveOutcome.CaptureMoreAvailable)
		{
			CheckSidesHavePiecesLeft();
			Turn = Turn == PieceColour.Black ? PieceColour.White : PieceColour.Black;
		}

		if (res == MoveOutcome.Unknown)
		{
			Turn = Turn == PieceColour.Black
				? PieceColour.White
				: PieceColour.Black;
		}

		//ShowBoard();

		return res;
	}

	public void CheckSidesHavePiecesLeft()
	{
		var retVal = Board.Pieces.Where(x => x.InPlay).Select(y => y.Side).Distinct().Count() == 2;

		if (!retVal)
		{
			Winner = Board.Pieces.Where(x => x.InPlay).Select(y => y.Side).Distinct().FirstOrDefault();
		}
	}

	public string GetCurrentPlayer()
	{
		return Turn.ToString();
	}

	public int GetWhitePiecesTaken()
	{
		return GetPiecesTakenForSide(PieceColour.White);
	}

	public int GetBlackPiecesTaken()
	{
		return GetPiecesTakenForSide(PieceColour.Black);
	}

	public int GetPiecesTakenForSide(PieceColour colour)
	{
		return PiecesPerSide - Board.Pieces.Count(x => x.InPlay && x.Side == colour);
	}

	//private void ShowBoard()
	//{
	//	Display.DisplayBoard(Board, GetWhitePiecesTaken(), GetBlackPiecesTaken());
	//	Display.DisplayCurrentPlayer(Turn);
	//}
}
