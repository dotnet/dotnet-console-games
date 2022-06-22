namespace Checkers;

public class Board
{
	public List<Piece> Pieces { get; set; }

	public Piece? Aggressor { get; set; }

	public Piece? this[int x, int y] =>
		Pieces.FirstOrDefault(piece => piece.X == x && piece.Y == y);

	public Board()
	{
		Aggressor = null;
		Pieces = new List<Piece>
			{
				new() { NotationPosition ="A3", Color = Black},
				new() { NotationPosition ="A1", Color = Black},
				new() { NotationPosition ="B2", Color = Black},
				new() { NotationPosition ="C3", Color = Black},
				new() { NotationPosition ="C1", Color = Black},
				new() { NotationPosition ="D2", Color = Black},
				new() { NotationPosition ="E3", Color = Black},
				new() { NotationPosition ="E1", Color = Black},
				new() { NotationPosition ="F2", Color = Black},
				new() { NotationPosition ="G3", Color = Black},
				new() { NotationPosition ="G1", Color = Black},
				new() { NotationPosition ="H2", Color = Black},

				new() { NotationPosition ="A7", Color = White},
				new() { NotationPosition ="B8", Color = White},
				new() { NotationPosition ="B6", Color = White},
				new() { NotationPosition ="C7", Color = White},
				new() { NotationPosition ="D8", Color = White},
				new() { NotationPosition ="D6", Color = White},
				new() { NotationPosition ="E7", Color = White},
				new() { NotationPosition ="F8", Color = White},
				new() { NotationPosition ="F6", Color = White},
				new() { NotationPosition ="G7", Color = White},
				new() { NotationPosition ="H8", Color = White},
				new() { NotationPosition ="H6", Color = White}
			};
	}

	public static string ToPositionNotationString(int x, int y)
	{
		if (!IsValidPosition(x, y)) throw new ArgumentException("Not a valid position!");
		return $"{(char)('A' + x)}{y + 1}";
	}

	public static (int X, int Y) ParsePositionNotation(string notation)
	{
		if (notation is null) throw new ArgumentNullException(nameof(notation));
		notation = notation.Trim().ToUpper();
		if (notation.Length is not 2 ||
			notation[0] < 'A' || 'H' < notation[0] ||
			notation[1] < '1' || '8' < notation[1])
			throw new FormatException($@"{nameof(notation)} ""{notation}"" is not valid");
		return (notation[0] - 'A', notation[1] - '1');
	}

	public static bool IsValidPosition(int x, int y) =>
		0 <= x && x < 8 &&
		0 <= y && y < 8;

	public (Piece A, Piece B) GetClosestRivalPieces(PieceColor priorityColor)
	{
		double minDistanceSquared = double.MaxValue;
		(Piece A, Piece B) closestRivals = (null!, null!);
		foreach (Piece a in Pieces.Where(piece => piece.Color == priorityColor))
		{
			foreach (Piece b in Pieces.Where(piece => piece.Color != priorityColor))
			{
				(int X, int Y) vector = (a.X - b.X, a.Y - b.Y);
				double distanceSquared = vector.X * vector.X + vector.Y * vector.Y;
				if (distanceSquared < minDistanceSquared)
				{
					minDistanceSquared = distanceSquared;
					closestRivals = (a, b);
				}
			}
		}
		return closestRivals;
	}

	public List<Move> GetPossibleMoves(PieceColor color)
	{
		List<Move> moves = new();
		if (Aggressor is not null)
		{
			if (Aggressor.Color != color)
			{
				throw new Exception();
			}
			moves.AddRange(GetPossibleMoves(Aggressor).Where(move => move.PieceToCapture is not null));
		}
		else
		{
			foreach (Piece piece in Pieces.Where(piece => piece.Color == color))
			{
				moves.AddRange(GetPossibleMoves(piece));
			}
		}
		return moves.Any(move => move.PieceToCapture is not null)
			? moves.Where(move => move.PieceToCapture is not null).ToList()
			: moves;
	}

	public List<Move> GetPossibleMoves(Piece piece)
	{
		List<Move> moves = new();
		ValidateMove(-1, -1);
		ValidateMove(-1, 1);
		ValidateMove(1, -1);
		ValidateMove(1, 1);
		return moves.Any(move => move.PieceToCapture is not null)
			? moves.Where(move => move.PieceToCapture is not null).ToList()
			: moves;

		void ValidateMove(int dx, int dy)
		{
			if (!piece.Promoted && piece.Color is Black && dy is -1) return;
			if (!piece.Promoted && piece.Color is White && dy is 1) return;
			(int X, int Y) target = (piece.X + dx, piece.Y + dy);
			if (!Board.IsValidPosition(target.X, target.Y)) return;
			PieceColor? targetColor = this[target.X, target.Y]?.Color;
			if (targetColor is null)
			{
				if (!Board.IsValidPosition(target.X, target.Y)) return;
				Move newMove = new(piece, target);
				moves.Add(newMove);
			}
			else if (targetColor != piece.Color)
			{
				(int X, int Y) jump = (piece.X + 2 * dx, piece.Y + 2 * dy);
				if (!Board.IsValidPosition(jump.X, jump.Y)) return;
				PieceColor? jumpColor = this[jump.X, jump.Y]?.Color;
				if (jumpColor is not null) return;
				Move attack = new(piece, jump, this[target.X, target.Y]);
				moves.Add(attack);
			}
		}
	}

	/// <summary>Returns a <see cref="Move"/> if <paramref name="from"/>-&gt;<paramref name="to"/> is valid or null if not.</summary>
	public Move? ValidateMove(PieceColor color, (int X, int Y) from, (int X, int Y) to)
	{
		Piece? piece = this[from.X, from.Y];
		if (piece is null)
		{
			return null;
		}
		foreach (Move move in GetPossibleMoves(color))
		{
			if ((move.PieceToMove.X, move.PieceToMove.Y) == from && move.To == to)
			{
				return move;
			}
		}
		return null;
	}

	public bool IsTowards(Move move, Piece piece)
	{
		(int Dx, int Dy) a = (move.PieceToMove.X - piece.X, move.PieceToMove.Y - piece.Y);
		int a_distanceSquared = a.Dx * a.Dx + a.Dy * a.Dy;
		(int Dx, int Dy) b = (move.To.X - piece.X, move.To.Y - piece.Y);
		int b_distanceSquared = b.Dx * b.Dx + b.Dy * b.Dy;
		return b_distanceSquared < a_distanceSquared;
	}
}
