using System.Drawing;

namespace Checkers;

public static class Display
{
    private const string BlackPiece = "○";
    private const string BlackKing = "☺";
    private const string WhitePiece = "◙";
    private const string WhiteKing = "☻";

    public static void DisplayBoard(Board currentBoard)
    {
        PrintBoard();
        PrintPieces(currentBoard);
        Console.CursorVisible = false;
    }

    public static void DisplayStats(int whitesTaken, int blacksTaken)
    {
        Console.SetCursorPosition(22, 6);
        Console.WriteLine(" Taken:");
        Console.SetCursorPosition(22, 7);
        Console.WriteLine($"{whitesTaken.ToString(),2} x {WhitePiece}");
        Console.SetCursorPosition(22, 8);
        Console.WriteLine($"{blacksTaken.ToString(),2} x {BlackPiece}");
    }

    public static void DisplayCurrentPlayer(PieceColour currentSide)
    {
        Console.SetCursorPosition(0, 11);
        Console.WriteLine($"{currentSide} to play");
    }

    public static void DisplayWinner(PieceColour winningSide)
    {
        Console.SetCursorPosition(0, 11);
        Console.WriteLine($"*** {winningSide} wins ***");
    }

    private static void PrintPieces(Board currentBoard)
    {
        foreach (var piece in currentBoard.Pieces.Where(x => x.InPlay))
        {
            var screenPosition =
                DisplayHelper.GetScreenPositionFromBoardPosition(new Point(piece.XPosition, piece.YPosition));
            var displayPiece = GetDisplayPiece(piece);
            Console.SetCursorPosition(screenPosition.X, screenPosition.Y);
            Console.Write(displayPiece);
        }
    }

    private static string GetDisplayPiece(Piece currentPiece)
    {
        string retVal;

        if (currentPiece.Side == PieceColour.Black)
        {
            retVal = currentPiece.Promoted ? BlackKing : BlackPiece;
        }
        else
        {
            retVal = currentPiece.Promoted ? WhiteKing : WhitePiece;
        }

        return retVal;
    }

    private static void PrintBoard()
    {
        Console.Clear();
        var emptyBoard = GetBlankBoard();

        foreach (var rank in emptyBoard)
        {
            Console.WriteLine(rank);
        }
    }

    private static List<string> GetBlankBoard()
    {
        var retVal = new List<string>
            {
                $" ╔═══════════════════╗",
                $"8║  . . . . . . . .  ║ {BlackPiece} = Black",
                $"7║  . . . . . . . .  ║ {BlackKing} = Black King",
                $"6║  . . . . . . . .  ║ {WhitePiece} = White",
                $"5║  . . . . . . . .  ║ {WhiteKing} = White King",
                $"4║  . . . . . . . .  ║",
                $"3║  . . . . . . . .  ║",
                $"2║  . . . . . . . .  ║",
                $"1║  . . . . . . . .  ║",
                $" ╚═══════════════════╝",
                $"    A B C D E F G H"
            };

        return retVal;
    }
}

