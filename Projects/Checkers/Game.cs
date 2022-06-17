using Checkers.Types;
using System.Drawing;

namespace Checkers;

public class Game
{
    private const int PiecesPerSide = 12;

    public PieceColour CurrentGo { get; set; }
    public Board GameBoard { get; }
    public int MovesSoFar { get; private set; }
    public PieceColour GameWinner { get; set; } = PieceColour.NotSet;

    private bool NoDisplay { get; }

    public List<Player> Players { get; set; } = new()
        {
            new Player { ControlledBy = PlayerControl.Computer, Side = PieceColour.Black },
            new Player { ControlledBy = PlayerControl.Computer, Side = PieceColour.White }
        };

    public Game(List<Piece> startingPosition, PieceColour toMove)
    {
        GameBoard = new Board(startingPosition);
        CurrentGo = toMove;
        NoDisplay = true;
    }

    public Game()
    {
        GameBoard = new Board();
        CurrentGo = PieceColour.Black;
        NoDisplay = false;
        ShowBoard();
    }

    public MoveOutcome NextRound(Point? from = null, Point? to = null)
    {
        MovesSoFar++;
        var res = Engine.PlayNextMove(CurrentGo, GameBoard, from, to);

        while (from == null & to == null && res == MoveOutcome.CaptureMoreAvailable)
        {
            res = Engine.PlayNextMove(CurrentGo, GameBoard, from, to);
        }

        if (res == MoveOutcome.BlackWin)
        {
            GameWinner = PieceColour.Black;
        }

        if (res == MoveOutcome.WhiteWin)
        {
            GameWinner = PieceColour.White;
        }

        if (GameWinner == PieceColour.NotSet && res != MoveOutcome.CaptureMoreAvailable)
        {
            CheckSidesHavePiecesLeft();
            CurrentGo = CurrentGo == PieceColour.Black ? PieceColour.White : PieceColour.Black;
        }

        if (res == MoveOutcome.Unknown)
        {
            CurrentGo = CurrentGo == PieceColour.Black
                ? PieceColour.White
                : PieceColour.Black;
        }

        ShowBoard();

        return res;
    }

    public void CheckSidesHavePiecesLeft()
    {
        var retVal = GameBoard.Pieces.Where(x => x.InPlay).Select(y => y.Side).Distinct().Count() == 2;

        if (!retVal)
        {
            GameWinner = GameBoard.Pieces.Where(x => x.InPlay).Select(y => y.Side).Distinct().FirstOrDefault();
        }
    }

    public string GetCurrentPlayer()
    {
        return CurrentGo.ToString();
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
        return PiecesPerSide - GameBoard.Pieces.Count(x => x.InPlay && x.Side == colour);
    }

    private void ShowBoard()
    {
        if (!NoDisplay)
        {
            Display.DisplayBoard(GameBoard);
            Display.DisplayStats(GetWhitePiecesTaken(), GetBlackPiecesTaken());
            Display.DisplayCurrentPlayer(CurrentGo);
        }
    }
}

