namespace Checkers;

public static class Engine
{
	public static MoveOutcome? PlayNextMove(PieceColor side, Board board, (int X, int Y)? from = null, (int X, int Y)? to = null)
	{
		List<Move> possibleMoves;
		MoveOutcome? outcome;
		if (from is not null && to is not null)
		{
			outcome = GetAllPossiblePlayerMoves(side, board, out possibleMoves);
			if (possibleMoves.Count is 0)
			{
				outcome = MoveOutcome.NoMoveAvailable;
			}
			else
			{
				if (MoveIsValid(from, to.Value, possibleMoves, board, out Move? selectedMove))
				{
					possibleMoves.Clear();
					if (selectedMove is not null)
					{
						possibleMoves.Add(selectedMove);
						outcome = selectedMove.Capturing is not null ? MoveOutcome.Capture : MoveOutcome.ValidMoves;
					}
				}
			}
		}
		else
		{
			outcome = AnalysePosition(side, board, out possibleMoves);
		}
		// If a side can't play then other side wins
		if (outcome is MoveOutcome.NoMoveAvailable)
		{
			outcome = side is Black ? MoveOutcome.WhiteWin : MoveOutcome.BlackWin;
		}
		switch (outcome)
		{
			case MoveOutcome.EndGame:
			case MoveOutcome.ValidMoves:
				Move bestMove = possibleMoves[Random.Shared.Next(possibleMoves.Count)];
				Piece pieceToMove = bestMove.PieceToMove!;
				int newX = bestMove.To.X;
				int newY = bestMove.To.Y;
				pieceToMove.X = newX;
				pieceToMove.Y = newY;
				// Promotion can only happen if not already a king and you have reached the far side
				if (newY is 0 or 7 && !pieceToMove.Promoted)
				{
					pieceToMove.Promoted = true;
				}
				break;
			case MoveOutcome.Capture:
				bool anyPromoted = PerformCapture(side, possibleMoves, board);
				bool moreAvailable = MoreCapturesAvailable(side, board);
				if (moreAvailable && !anyPromoted)
				{
					outcome = MoveOutcome.CaptureMoreAvailable;
				}
				break;
		}
		if (outcome is not null && outcome is not MoveOutcome.CaptureMoreAvailable)
		{
			board.Aggressor = null;
		}
		return outcome;
	}

	private static bool MoreCapturesAvailable(PieceColor side, Board board)
	{
		Piece? aggressor = board.Aggressor;
		if (aggressor is null)
		{
			return false;
		}
		_ = GetAllPossiblePlayerMoves(side, board, out List<Move>? possibleMoves);
		return possibleMoves.Any(move => move.PieceToMove == aggressor && move.Capturing is not null);
	}

	private static bool MoveIsValid((int X, int Y)? from, (int X, int Y) to, List<Move> possibleMoves, Board board, out Move? selectedMove)
	{
		selectedMove = default;
		if (from is null)
		{
			return false;
		}
		Piece? selectedPiece = board[from.Value.X, from.Value.Y];
		foreach (Move move in possibleMoves.Where(move => move.PieceToMove == selectedPiece && move.To == to))
		{
			selectedMove = move;
			return true;
		}
		return false;
	}

	private static MoveOutcome? GetAllPossiblePlayerMoves(PieceColor side, Board board, out List<Move> possibleMoves)
	{
		Piece? aggressor = board.Aggressor;
		MoveOutcome? result = null;
		possibleMoves = new List<Move>();
		if (PlayingWithJustKings(side, board, out List<Move>? endGameMoves))
		{
			result = MoveOutcome.EndGame;
			possibleMoves.AddRange(endGameMoves);
		}
		GetPossibleMovesAndAttacks(side, board, out List<Move>? nonEndGameMoves);
		possibleMoves.AddRange(nonEndGameMoves);
		if (aggressor is not null)
		{
			List<Move>? tempMoves = possibleMoves.Where(move => move.PieceToMove == aggressor).ToList();
			possibleMoves = tempMoves;
		}
		return result;
	}

	private static bool PerformCapture(PieceColor side, List<Move> possibleCaptures, Board board)
	{
		Move? captureMove = possibleCaptures.FirstOrDefault(move => move.Capturing is not null);
		if (captureMove is not null)
		{
			(int X, int Y)? positionToCapture = captureMove.Capturing;
			if (positionToCapture is not null)
			{
				Piece? deadMan = board[positionToCapture.Value.X, positionToCapture.Value.Y];
				if (deadMan is not null)
				{
					board.Pieces.Remove(deadMan);
				}
				if (captureMove.PieceToMove is not null)
				{
					board.Aggressor = captureMove.PieceToMove;
					captureMove.PieceToMove.X = captureMove.To.X;
					captureMove.PieceToMove.Y = captureMove.To.Y;
				}
			}
		}
		bool anyPromoted = CheckForPiecesToPromote(side, board);
		return anyPromoted;
	}

	private static bool CheckForPiecesToPromote(PieceColor currentSide, Board board)
	{
		bool retVal = false;
		int promotionSpot = currentSide is White ? 0 : 7;
		foreach (Piece piece in board.Pieces.Where(piece => piece.Color == currentSide))
		{
			if (promotionSpot == piece.Y && !piece.Promoted)
			{
				piece.Promoted = retVal = true;
			}
		}
		return retVal;
	}

	private static MoveOutcome AnalysePosition(PieceColor side, Board board, out List<Move> possibleMoves)
	{
		MoveOutcome result;
		possibleMoves = new List<Move>();
		// Check for endgame first
		if (PlayingWithJustKings(side, board, out List<Move>? endGameMoves))
		{
			result = MoveOutcome.EndGame;
			possibleMoves.AddRange(endGameMoves);
			return result;
		}
		GetPossibleMovesAndAttacks(side, board, out List<Move>? nonEndGameMoves);
		if (nonEndGameMoves.Count is 0)
		{
			result = MoveOutcome.NoMoveAvailable;
		}
		else
		{
			if (nonEndGameMoves.Any(move => move.Capturing is not null))
			{
				result = MoveOutcome.Capture;
			}
			else
			{
				result = nonEndGameMoves.Count > 0 ? MoveOutcome.ValidMoves : MoveOutcome.NoMoveAvailable;
			}
		}
		if (nonEndGameMoves.Count > 0)
		{
			possibleMoves.AddRange(nonEndGameMoves);
		}
		return result;
	}

	private static void GetPossibleMovesAndAttacks(PieceColor color, Board board, out List<Move> possibleMoves)
	{
		List<Move> moves = new();

		foreach (Piece piece in board.Pieces.Where(piece => piece.Color == color))
		{
			ValidateMove(-1, -1);
			ValidateMove(-1,  1);
			ValidateMove( 1, -1);
			ValidateMove( 1,  1);

			void ValidateMove(int dx, int dy)
			{
				if (!piece.Promoted && color is Black && dy is -1) return;
				if (!piece.Promoted && color is White && dy is  1) return;
				(int X, int Y) target = (piece.X + dx, piece.Y + dy);
				if (!Board.IsValidPosition(target.X, target.Y)) return;
				PieceColor? targetColor = board[target.X, target.Y]?.Color;
				if (targetColor is null)
				{
					if (!Board.IsValidPosition(target.X, target.Y)) return;
					Move newMove = new(piece, target);
					moves.Add(newMove);
				}
				else if (targetColor != color)
				{
					(int X, int Y) jump = (piece.X + 2 * dx, piece.Y + 2 * dy);
					if (!Board.IsValidPosition(jump.X, jump.Y)) return;
					PieceColor? jumpColor = board[jump.X, jump.Y]?.Color;
					if (jumpColor is not null) return;
					Move attack = new(piece, jump, target);
					moves.Add(attack);
				}
			}
		}

		possibleMoves = moves;
	}

	private static bool PlayingWithJustKings(PieceColor side, Board board, out List<Move> possibleMoves)
	{
		possibleMoves = new List<Move>();
		int piecesInPlay = board.Pieces.Count(piece => piece.Color == side);
		int kingsInPlay = board.Pieces.Count(piece => piece.Color == side && piece.Promoted);
		bool playingWithJustKings = piecesInPlay == kingsInPlay;
		if (playingWithJustKings)
		{
			double shortestDistance = 12.0;
			Piece? currentHero = null;
			Piece? currentVillain = null;
			foreach (Piece king in board.Pieces.Where(piece => piece.Color == side))
			{
				foreach (Piece target in board.Pieces.Where(piece => piece.Color != side))
				{
					(int X, int Y) kingPoint = (king.X, king.Y);
					(int X, int Y) targetPoint = (target.X, target.Y);
					double distance = GetPointDistance(kingPoint, targetPoint);
					if (distance < shortestDistance)
					{
						shortestDistance = distance;
						currentHero = king;
						currentVillain = target;
					}
				}
			}
			if (currentHero is not null && currentVillain is not null)
			{
				List<(int X, int Y)>? movementOptions = WhereIsVillain(currentHero, currentVillain);
				foreach ((int X, int Y) movementOption in movementOptions)
				{
					PieceColor? targetColor = board[movementOption.X, movementOption.Y]?.Color;
					if (targetColor is null)
					{
						if (!Board.IsValidPosition(movementOption.X, movementOption.Y))
						{
							possibleMoves.Add(new Move(currentHero, movementOption));
						}
						break;
					}
				}
			}
		}
		return possibleMoves.Count > 0;
	}

	public static double GetPointDistance((int X, int Y) first, (int X, int Y) second)
	{
		// Easiest cases are points on the same vertical or horizontal axis
		if (first.X == second.X)
		{
			return Math.Abs(first.Y - second.Y);
		}
		if (first.Y == second.Y)
		{
			return Math.Abs(first.X - second.X);
		}
		// Pythagoras baby
		double sideA = Math.Abs(first.Y - second.Y);
		double sideB = Math.Abs(first.X - second.X);
		return Math.Sqrt(Math.Pow(sideA, 2) + Math.Pow(sideB, 2));
	}

	public static List<(int X, int Y)> WhereIsVillain(Piece hero, Piece villain)
	{
		const int Up = 1;
		const int Down = 2;
		const int Left = 3;
		const int Right = 4;

		List<(int X, int Y)>? retVal = new();
		List<int> directions = new();
		if (hero.X > villain.X)
		{
			directions.Add(Left);
		}
		if (hero.X < villain.X)
		{
			directions.Add(Right);
		}
		if (hero.Y > villain.Y)
		{
			directions.Add(Up);
		}
		if (hero.Y < villain.Y)
		{
			directions.Add(Down);
		}
		if (directions.Count is 1)
		{
			switch (directions[0])
			{
				case Up:
					retVal.Add((hero.X - 1, hero.Y - 1));
					retVal.Add((hero.X + 1, hero.Y - 1));
					break;
				case Down:
					retVal.Add((hero.X - 1, hero.Y + 1));
					retVal.Add((hero.X + 1, hero.Y + 1));
					break;
				case Left:
					retVal.Add((hero.X - 1, hero.Y - 1));
					retVal.Add((hero.X - 1, hero.Y + 1));
					break;
				case Right:
					retVal.Add((hero.X + 1, hero.Y - 1));
					retVal.Add((hero.X + 1, hero.Y + 1));
					break;
				default:
					throw new NotImplementedException();
			}
		}
		else
		{
			if (directions.Contains(Left) && directions.Contains(Up))
			{
				retVal.Add((hero.X - 1, hero.Y - 1));
			}
			if (directions.Contains(Left) && directions.Contains(Down))
			{
				retVal.Add((hero.X - 1, hero.Y + 1));
			}
			if (directions.Contains(Right) && directions.Contains(Up))
			{
				retVal.Add((hero.X + 1, hero.Y - 1));
			}
			if (directions.Contains(Right) && directions.Contains(Down))
			{
				retVal.Add((hero.X + 1, hero.Y + 1));
			}
		}
		return retVal;
	}
}
