namespace Checkers;

public static class Engine
{
	public static MoveOutcome PlayNextMove(PieceColor side, Board board, (int X, int Y)? playerFrom = null, (int X, int Y)? playerTo = null)
	{
		List<Move> possibleMoves;
		MoveOutcome outcome;
		if (playerFrom is not null && playerTo is not null)
		{
			outcome = GetAllPossiblePlayerMoves(side, board, out possibleMoves);
			if (possibleMoves.Count == 0)
			{
				outcome = MoveOutcome.NoMoveAvailable;
			}
			else
			{
				if (MoveIsValid(playerFrom, playerTo.Value, possibleMoves, board, out Move? selectedMove))
				{
					possibleMoves.Clear();
					if (selectedMove is not null)
					{
						possibleMoves.Add(selectedMove);

						switch (selectedMove.TypeOfMove)
						{
							case MoveType.Unknown:
								break;
							case MoveType.StandardMove:
								outcome = MoveOutcome.ValidMoves;
								break;
							case MoveType.Capture:
								outcome = MoveOutcome.Capture;
								break;
							case MoveType.EndGame:
								outcome = MoveOutcome.EndGame;
								break;
							default:
								throw new NotImplementedException();
						}
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

		if (outcome is not MoveOutcome.CaptureMoreAvailable)
		{
			ResetCapturePiece(board);
		}

		return outcome;
	}

	private static void ResetCapturePiece(Board board)
	{
		Piece? capturePiece = GetAggressor(board);
		if (capturePiece is not null)
		{
			capturePiece.Aggressor = false;
		}
	}

	private static bool MoreCapturesAvailable(PieceColor side, Board board)
	{
		Piece? aggressor = GetAggressor(board);
		if (aggressor is null)
		{
			return false;
		}
		_ = GetAllPossiblePlayerMoves(side, board, out List<Move>? possibleMoves);
		return possibleMoves.Any(move => move.PieceToMove == aggressor && move.Capturing is not null);
	}

	private static Piece? GetAggressor(Board board)
	{
		return board.Pieces.FirstOrDefault(piece => piece.Aggressor);
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

	private static MoveOutcome GetAllPossiblePlayerMoves(PieceColor side, Board board, out List<Move> possibleMoves)
	{
		Piece? aggressor = GetAggressor(board);
		MoveOutcome result = MoveOutcome.Unknown;
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
		string from = string.Empty;
		string to = string.Empty;

		if (captureMove is not null)
		{
			(int X, int Y)? squareToCapture = captureMove.Capturing;

			if (squareToCapture is not null)
			{
				Piece? deadMan = board[squareToCapture.Value.X, squareToCapture.Value.Y];
				if (deadMan is not null)
				{
					board.Pieces.Remove(deadMan);
				}
				if (captureMove.PieceToMove is not null)
				{
					captureMove.PieceToMove.Aggressor = true;
					from = Board.ToPositionNotationString(captureMove.PieceToMove.XPosition, captureMove.PieceToMove.YPosition);
					captureMove.PieceToMove.XPosition = captureMove.To.X;
					captureMove.PieceToMove.YPosition = captureMove.To.Y;
					to = Board.ToPositionNotationString(captureMove.PieceToMove.XPosition, captureMove.PieceToMove.YPosition);
				}
			}
		}
		bool anyPromoted = CheckForPiecesToPromote(side, board);
		PlayerAction playerAction = anyPromoted ? PlayerAction.CapturePromotion : PlayerAction.Capture;
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

	private static void GetPossibleMovesAndAttacks(PieceColor side, Board board, out List<Move> possibleMoves)
	{
		possibleMoves = new List<Move>();

		foreach (Piece piece in board.Pieces.Where(piece => piece.Color == side))
		{
			for (int x = -1; x < 2; x++)
			{
				for (int y = -1; y < 2; y++)
				{
					if (x == 0 || y == 0)
					{
						continue;
					}
					if (!piece.Promoted)
					{
						switch (side)
						{
							case PieceColor.White when y == 1:
							case PieceColor.Black when y == -1:
								continue;
						}
					}
					int currentX = piece.XPosition + x;
					int currentY = piece.YPosition + y;
					if (!Board.IsValidPosition(currentX, currentY))
					{
						continue;
					}
					PieceColor? targetSquare = board[currentX, currentY]?.Color;
					if (targetSquare is null)
					{
						if (!Board.IsValidPosition(currentX, currentY))
						{
							continue;
						}
						Move newMove = new() { PieceToMove = piece, TypeOfMove = MoveType.StandardMove, To = (currentX, currentY) };
						possibleMoves.Add(newMove);
					}
					else if (targetSquare != side)
					{
						(int X, int Y) toLocation = (piece.XPosition + 2 * x, piece.YPosition + 2 * y);
						if (!Board.IsValidPosition(toLocation.X, toLocation.Y))
						{
							continue;
						}
						PieceColor? beyondSquare = board[toLocation.X, toLocation.Y]?.Color;
						if (beyondSquare is not null)
						{
							continue;
						}
						Move attack = new() { PieceToMove = piece, TypeOfMove = MoveType.Capture, To = (toLocation.X, toLocation.Y), Capturing = (currentX, currentY) };
						possibleMoves.Add(attack);
					}
				}
			}
		}
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
					PieceColor? squareStatus = board[movementOption.X, movementOption.Y]?.Color;
					if (squareStatus is null)
					{
						Move move = new()
						{
							PieceToMove = currentHero,
							TypeOfMove = MoveType.EndGame,
							To = (movementOption.X, movementOption.Y)
						};
						if (!Board.IsValidPosition(movementOption.X, movementOption.Y))
						{
							continue;
						}
						possibleMoves.Add(move);
						break;
					}
				}
			}
		}
		return possibleMoves.Count > 0;
	}
}
