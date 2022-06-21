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
		if (outcome == MoveOutcome.NoMoveAvailable)
		{
			outcome = side == PieceColor.Black ? MoveOutcome.WhiteWin : MoveOutcome.BlackWin;
		}
		switch (outcome)
		{
			case MoveOutcome.EndGame:
			case MoveOutcome.ValidMoves:
				Move bestMove = possibleMoves[Random.Shared.Next(possibleMoves.Count)];
				Piece pieceToMove = bestMove.PieceToMove!;
				int newX = bestMove.To.X;
				int newY = bestMove.To.Y;
				pieceToMove.XPosition = newX;
				pieceToMove.YPosition = newY;
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
					captureMove.PieceToMove.XPosition = captureMove.To.X;
					captureMove.PieceToMove.YPosition = captureMove.To.Y;
				}
			}
		}
		bool anyPromoted = CheckForPiecesToPromote(side, board);
		return anyPromoted;
	}

	private static bool CheckForPiecesToPromote(PieceColor currentSide, Board board)
	{
		bool retVal = false;
		int promotionSpot = currentSide == PieceColor.White ? 0 : 7;
		foreach (Piece piece in board.Pieces.Where(piece => piece.Color == currentSide))
		{
			if (promotionSpot == piece.YPosition && !piece.Promoted)
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
		if (nonEndGameMoves.Count == 0)
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
				if (!piece.Promoted && color is PieceColor.Black && dy is -1) return;
				if (!piece.Promoted && color is PieceColor.White && dy is  1) return;
				(int X, int Y) target = (piece.XPosition + dx, piece.YPosition + dy);
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
					(int X, int Y) jump = (piece.XPosition + 2 * dx, piece.YPosition + 2 * dy);
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
					(int X, int Y) kingPoint = (king.XPosition, king.YPosition);
					(int X, int Y) targetPoint = (target.XPosition, target.YPosition);
					double distance = VectorHelper.GetPointDistance(kingPoint, targetPoint);
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
				List<(int X, int Y)>? movementOptions = VectorHelper.WhereIsVillain(currentHero, currentVillain);
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
}
