namespace Checkers;

public static class Display
{
	private const char BlackPiece = '○';
	private const char BlackKing = '☺';
	private const char WhitePiece = '◙';
	private const char WhiteKing = '☻';

	public static void DisplayBoard(Board currentBoard, int whitesTaken, int blacksTaken)
	{
		Console.Clear();
		Dictionary<(int X, int Y), char> tiles = new();
		foreach (Piece piece in currentBoard.Pieces)
		{
			if (piece.InPlay)
			{
				tiles[(piece.XPosition, piece.YPosition)] = ToChar(piece);
			}
		}
		char C(int x, int y) => tiles.GetValueOrDefault((x, y), '.');
		Console.Write(
			$"    ╔═══════════════════╗\n" +
			$"  8 ║  {C(0, 0)} {C(1, 0)} {C(2, 0)} {C(3, 0)} {C(4, 0)} {C(5, 0)} {C(6, 0)} {C(7, 0)}  ║ {BlackPiece} = Black\n" +
			$"  7 ║  {C(0, 1)} {C(1, 1)} {C(2, 1)} {C(3, 1)} {C(4, 1)} {C(5, 1)} {C(6, 1)} {C(7, 1)}  ║ {BlackKing} = Black King\n" +
			$"  6 ║  {C(0, 2)} {C(1, 2)} {C(2, 2)} {C(3, 2)} {C(4, 2)} {C(5, 2)} {C(6, 2)} {C(7, 2)}  ║ {WhitePiece} = White\n" +
			$"  5 ║  {C(0, 3)} {C(1, 3)} {C(2, 3)} {C(3, 3)} {C(4, 3)} {C(5, 3)} {C(6, 3)} {C(7, 3)}  ║ {WhiteKing} = White King\n" +
			$"  4 ║  {C(0, 4)} {C(1, 4)} {C(2, 4)} {C(3, 4)} {C(4, 4)} {C(5, 4)} {C(6, 4)} {C(7, 4)}  ║\n" +
			$"  3 ║  {C(0, 5)} {C(1, 5)} {C(2, 5)} {C(3, 5)} {C(4, 5)} {C(5, 5)} {C(6, 5)} {C(7, 5)}  ║ Taken:\n" +
			$"  2 ║  {C(0, 6)} {C(1, 6)} {C(2, 6)} {C(3, 6)} {C(4, 6)} {C(5, 6)} {C(6, 6)} {C(7, 6)}  ║ {whitesTaken,2} x {WhitePiece}\n" +
			$"  1 ║  {C(0, 7)} {C(1, 7)} {C(2, 7)} {C(3, 7)} {C(4, 7)} {C(5, 7)} {C(6, 7)} {C(7, 7)}  ║ {blacksTaken,2} x {BlackPiece}\n" +
			$"    ╚═══════════════════╝\n" +
			$"       A B C D E F G H");

		Console.CursorVisible = false;
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

	private static char ToChar(Piece piece) =>
		(piece.Side, piece.Promoted) switch
		{
			(PieceColour.Black, false) => BlackPiece,
			(PieceColour.Black, true) => BlackKing,
			(PieceColour.White, false) => WhitePiece,
			(PieceColour.White, true) => WhiteKing,
			_ => throw new NotImplementedException(),
		};

	public static (int X, int Y) GetScreenPositionFromBoardPosition((int X, int Y) boardPosition) =>
		((boardPosition.X * 2) + 7, boardPosition.Y + 1);
}
