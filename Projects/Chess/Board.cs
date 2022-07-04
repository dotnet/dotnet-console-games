namespace Chess;

public class Board
{
	public static char[] Letters { get; } = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};

	public static Dictionary<char, int> BoardColumns { get; } = new()
	{
		{'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8}
	};
	
	// position status for a piece on the board currently
	public object[,] Cases { get; set; }
	public IList<Piece> Pieces { get; set; }

	private Board()
	{
		Cases = new object[8, 8];
		Pieces = new List<Piece>();
	}
	
	public static Board NewGame()
	{
		var board = new Board();

		board.InitGame();

		return board;
	}
	
	
    public static Board NewEmpty() => new Board();
    public Piece GetPiece(char column, int row) => (Cases[row - 1, BoardColumns[column] - 1 ] as Piece)!;

    public T SetWhite<T>( char column, int row ) where T : Piece
    {
        var piece = Activator.CreateInstance(typeof(T), PieceColor.White) as T;
        Pieces.Add(piece);

        SetPiece(piece, column, row);

        return piece;
    }

    public T SetBlack<T>(char column, int row) where T : Piece
    {
        var piece = Activator.CreateInstance(typeof(T), PieceColor.Black) as T;
        Pieces.Add(piece);

        SetPiece(piece, column, row);

        return piece;
    }

    public void InitGame()
    {
        foreach( var column in BoardColumns.Keys )
        {

            SetWhite<Pawn>(column, 2);
            SetBlack<Pawn>(column, 7);
        }

        // Set our pieces for the start of the game
        SetWhite<Rook>('A', 1);
        SetWhite<Rook>('H', 1);
        SetBlack<Rook>('A', 8);
        SetBlack<Rook>('H', 8);

        SetWhite<Knight>('B', 1);
        SetWhite<Knight>('G', 1);
        SetBlack<Knight>('B', 8);
        SetBlack<Knight>('G', 8);

        SetWhite<Bishop>('C', 1);
        SetWhite<Bishop>('F', 1);
        SetBlack<Bishop>('C', 8);
        SetBlack<Bishop>('F', 8);

        SetWhite<Queen>('D', 1);
        SetBlack<Queen>('D', 8);

        SetWhite<King>('E', 1);
        SetBlack<King>('E', 8);
    }

    // try to do a movement of piece
    public MoveOutcome? MovePiece(
	    char column, 
	    int row, 
	    char targetColumn, 
	    int targetRow, 
	    PieceColor? color = null )
    {
        var result = new MoveOutcome();
    
        if(BoardColumns[targetColumn] is < 1 or > 8 || targetRow is < 1 or > 8)
        {
	        return null;
        }

        if(column == targetColumn && row == targetRow)
        {
	        return null;
        }

        var selectPiece = GetPiece(column, row);

        // check if there is a piece at start position
        if(selectPiece is null)
        {
	        return null;
        }

        // check color of piece
        if(color.HasValue) {
            if((color == PieceColor.White && selectPiece.Color == PieceColor.Black) ||
                (color == PieceColor.Black && selectPiece.Color == PieceColor.White)) {
                return null;
            }
        }

        var target = GetPiece(targetColumn, targetRow);

        // check it is a valid movement for piece (rules piece validator)
        if(!selectPiece.CheckIfValidMove(
            !selectPiece.Color.Equals(target.Color),
            row - 1, BoardColumns[column] - 1 , targetRow - 1, BoardColumns[targetColumn] - 1))
        {
            return null;
        }

        // check that the path is free so long as the piece is not a knight
        if(selectPiece is not Knight && !CheckIfPathIsOpen( column, row, targetColumn, targetRow))
        {
	        return null;
        }

        // check if target position there is already present a piece with same color
        if(target is not null && selectPiece.Color.Equals(target.Color))
        {
	        return null;
        }

        // set result information after capture
        result.Attack = target is not null && !selectPiece.Color.Equals(target.Color);
        if(result.Attack)
        {
            result.CapturedPiece = target;
            target.IsAlive = false;
        }

        // change the position of the piece
        SetPiece(selectPiece, targetColumn, targetRow);
        ClearBoardPosition(column, row);

        return result;
    }

    // set the piece at a specified position
    private void SetPiece(Piece piece, char column, int row)
    {
        Cases[row - 1, BoardColumns[column] - 1] = piece;
    }

    // check if the path for the selected piece is open
    private bool CheckIfPathIsOpen(char column, int row, char targetColumn, int targetRow)
    {
        var result = true;

        var stepX = (BoardColumns[targetColumn] - 1).CompareTo(BoardColumns[column] - 1);
        var stepY = targetRow.CompareTo(row);

        // start position
        var c = BoardColumns[column] - 1;
        var r = row - 1;

        // next position
        c = c + stepX;
        r = r + stepY;

        while(c != BoardColumns[targetColumn] - 1 && r == (targetRow -1))
        {
            var p = Cases[r, c];

            if(p is not null)
            {
                result = false;
                break;
            }

            // next position
            c = c + stepX;
            r = r + stepY;
        }

        return result;
    }
    
    // clear any pieces at a specified position
    private void ClearBoardPosition(char column, int row)
    {
	    Cases[row - 1, BoardColumns[column] - 1] = null;
    }
}
