﻿namespace Checkers;

public static class Engine
{
	public static MoveOutcome PlayNextMove(PieceColor currentSide, Board currentBoard, (int X, int Y)? playerFrom = null, (int X, int Y)? playerTo = null)
	{
		List<Move> possibleMoves;
		MoveOutcome outcome;

		if (playerFrom != null && playerTo != null)
		{
			outcome = GetAllPossiblePlayerMoves(currentSide, currentBoard, out possibleMoves);

			if (possibleMoves.Count == 0)
			{
				outcome = MoveOutcome.NoMoveAvailable;
			}
			else
			{
				if (MoveIsValid(playerFrom, playerTo.Value, possibleMoves, currentBoard, out var selectedMove))
				{
					possibleMoves.Clear();

					if (selectedMove != null)
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
			outcome = AnalysePosition(currentSide, currentBoard, out possibleMoves);
		}

		// If a side can't play then other side wins
		if (outcome == MoveOutcome.NoMoveAvailable)
		{
			outcome = currentSide == PieceColor.Black ? MoveOutcome.WhiteWin : MoveOutcome.BlackWin;
		}

		switch (outcome)
		{
			case MoveOutcome.EndGame:
			case MoveOutcome.ValidMoves:
				{
					var bestMove = possibleMoves[Random.Shared.Next(possibleMoves.Count)];

					if (bestMove == null)
					{
						throw new ArgumentNullException(nameof(bestMove));
					}

					var pieceToMove = bestMove.PieceToMove;

					if (pieceToMove == null)
					{
						throw new ArgumentNullException(nameof(pieceToMove));
					}

					int newX = bestMove.To.X;
					int newY = bestMove.To.Y;

					string from = Board.ToPositionNotationString(pieceToMove.XPosition, pieceToMove.YPosition);

					pieceToMove.XPosition = newX;
					pieceToMove.YPosition = newY;

					string to = Board.ToPositionNotationString(pieceToMove.XPosition, pieceToMove.YPosition);

					int blackPieces = currentBoard.GetNumberOfBlackPiecesInPlay();
					int whitePieces = currentBoard.GetNumberOfWhitePiecesInPlay();

					// Promotion can only happen if not already a king and you have reached the far side
					if (newY is 0 or 7 && pieceToMove.Promoted == false)
					{
						pieceToMove.Promoted = true;

						LoggingHelper.LogMove(from, to, PlayerAction.Promotion, currentSide, blackPieces, whitePieces);
					}
					else
					{
						LoggingHelper.LogMove(from, to, PlayerAction.Move, currentSide, blackPieces, whitePieces);
					}

					break;
				}
			case MoveOutcome.Capture:
				{
					var anyPromoted = PerformCapture(currentSide, possibleMoves, currentBoard);
					var moreAvailable = MoreCapturesAvailable(currentSide, currentBoard);

					if (moreAvailable && !anyPromoted)
					{
						outcome = MoveOutcome.CaptureMoreAvailable;
					}

					break;
				}
		}

		if (outcome != MoveOutcome.CaptureMoreAvailable)
		{
			ResetCapturePiece(currentBoard);
		}

		return outcome;
	}

	private static void ResetCapturePiece(Board currentBoard)
	{
		var capturePiece = GetAggressor(currentBoard);

		if (capturePiece != null)
		{
			capturePiece.Aggressor = false;
		}
	}

	private static bool MoreCapturesAvailable(PieceColor currentSide, Board currentBoard)
	{
		var aggressor = GetAggressor(currentBoard);

		if (aggressor == null)
		{
			return false;
		}

		_ = GetAllPossiblePlayerMoves(currentSide, currentBoard, out var possibleMoves);

		return possibleMoves.Any(move => move.PieceToMove == aggressor && move.Capturing != null);
	}

	private static Piece? GetAggressor(Board currentBoard)
	{
		return currentBoard.Pieces.FirstOrDefault(x => x.Aggressor);
	}

	private static bool MoveIsValid((int X, int Y)? from, (int X, int Y) to, List<Move> possibleMoves, Board currentBoard, out Move? selectedMove)
	{
		selectedMove = default;

		if (from == null)
		{
			return false;
		}

		var selectedPiece = currentBoard.GetPieceAt(from.Value.X, from.Value.Y);

		foreach (var move in possibleMoves.Where(move => move.PieceToMove == selectedPiece && move.To == to))
		{
			selectedMove = move;

			return true;
		}

		return false;
	}

	private static MoveOutcome GetAllPossiblePlayerMoves(PieceColor currentSide, Board currentBoard,
		out List<Move> possibleMoves)
	{
		var aggressor = GetAggressor(currentBoard);

		var result = MoveOutcome.Unknown;

		possibleMoves = new List<Move>();

		if (PlayingWithJustKings(currentSide, currentBoard, out var endGameMoves))
		{
			result = MoveOutcome.EndGame;
			possibleMoves.AddRange(endGameMoves);
		}

		GetPossibleMovesAndAttacks(currentSide, currentBoard, out var nonEndGameMoves);

		possibleMoves.AddRange(nonEndGameMoves);

		if (aggressor != null)
		{
			var tempMoves = possibleMoves.Where(x => x.PieceToMove == aggressor).ToList();
			possibleMoves = tempMoves;
		}

		return result;
	}

	private static bool PerformCapture(PieceColor currentSide, List<Move> possibleCaptures, Board currentBoard)
	{
		var captureMove = possibleCaptures.FirstOrDefault(x => x.Capturing != null);
		var from = string.Empty;
		var to = string.Empty;

		if (captureMove != null)
		{
			var squareToCapture = captureMove.Capturing;

			if (squareToCapture != null)
			{
				Piece? deadMan = currentBoard.GetPieceAt(squareToCapture.Value.X, squareToCapture.Value.Y);

				if (deadMan != null)
				{
					currentBoard.Pieces.Remove(deadMan);
				}

				if (captureMove.PieceToMove != null)
				{
					captureMove.PieceToMove.Aggressor = true;
					from = Board.ToPositionNotationString(captureMove.PieceToMove.XPosition, captureMove.PieceToMove.YPosition);

					captureMove.PieceToMove.XPosition = captureMove.To.X;
					captureMove.PieceToMove.YPosition = captureMove.To.Y;

					to = Board.ToPositionNotationString(captureMove.PieceToMove.XPosition, captureMove.PieceToMove.YPosition);

				}
			}
		}

		bool anyPromoted = CheckForPiecesToPromote(currentSide, currentBoard);
		int blackPieces = currentBoard.GetNumberOfBlackPiecesInPlay();
		int whitePieces = currentBoard.GetNumberOfWhitePiecesInPlay();

		var playerAction = anyPromoted ? PlayerAction.CapturePromotion : PlayerAction.Capture;

		LoggingHelper.LogMove(from, to, playerAction, currentSide, blackPieces, whitePieces);

		return anyPromoted;
	}

	private static bool CheckForPiecesToPromote(PieceColor currentSide, Board currentBoard)
	{
		bool retVal = false;
		int promotionSpot = currentSide == PieceColor.White ? 7 : 0;

		foreach (var piece in currentBoard.Pieces.Where(x => x.Color == currentSide))
		{
			if (promotionSpot == piece.YPosition && !piece.Promoted)
			{
				piece.Promoted = retVal = true;
			}
		}

		return retVal;
	}

	private static MoveOutcome AnalysePosition(PieceColor currentSide, Board currentBoard,
		out List<Move> possibleMoves)
	{
		MoveOutcome result;

		possibleMoves = new List<Move>();

		// Check for endgame first
		if (PlayingWithJustKings(currentSide, currentBoard, out var endGameMoves))
		{
			result = MoveOutcome.EndGame;
			possibleMoves.AddRange(endGameMoves);

			return result;
		}

		GetPossibleMovesAndAttacks(currentSide, currentBoard, out var nonEndGameMoves);

		if (nonEndGameMoves.Count == 0)
		{
			result = MoveOutcome.NoMoveAvailable;
		}
		else
		{
			if (nonEndGameMoves.Any(x => x.Capturing != null))
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

	private static void GetPossibleMovesAndAttacks(PieceColor currentSide, Board currentBoard,
		out List<Move> possibleMoves)
	{
		possibleMoves = new List<Move>();

		foreach (Piece piece in currentBoard.Pieces.Where(c => c.Color == currentSide))
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
						switch (currentSide)
						{
							case PieceColor.White when y == -1:
							case PieceColor.Black when y == 1:
								continue;
						}
					}

					var currentX = piece.XPosition + x;
					var currentY = piece.YPosition + y;

					if (!Board.IsValidPosition(currentX, currentY))
					{
						continue;
					}

					PieceColor targetSquare = currentBoard.GetSquareOccupancy(currentX, currentY);

					if (targetSquare == PieceColor.NotSet)
					{
						if (!Board.IsValidPosition(currentX, currentY))
						{
							continue;
						}

						Move newMove = new Move { PieceToMove = piece, TypeOfMove = MoveType.StandardMove, To = (currentX, currentY) };
						possibleMoves.Add(newMove);
					}
					else
					{
						var haveTarget = targetSquare != currentSide;

						if (!haveTarget)
						{
							continue;
						}

						var toLocation = DeriveToPosition(piece.XPosition, piece.YPosition, currentX, currentY);

						var beyondX = toLocation.X;
						var beyondY = toLocation.Y;

						if (!Board.IsValidPosition(beyondX, beyondY))
						{
							continue;
						}

						var beyondSquare = currentBoard.GetSquareOccupancy(beyondX, beyondY);

						if (beyondSquare != PieceColor.NotSet)
						{
							continue;
						}

						Move attack = new Move { PieceToMove = piece, TypeOfMove = MoveType.Capture, To = (beyondX, beyondY), Capturing = (currentX, currentY) };
						possibleMoves.Add(attack);
					}
				}
			}
		}
	}

	private static (int X, int Y) DeriveToPosition(int pieceX, int pieceY, int captureX, int captureY)
	{
		int newX;
		if (captureX > pieceX)
		{
			newX = captureX + 1;
		}
		else
		{
			newX = captureX - 1;
		}

		int newY;
		if (captureY > pieceY)
		{
			newY = captureY + 1;
		}
		else
		{
			newY = captureY - 1;
		}

		return (newX, newY);
	}

	private static bool PlayingWithJustKings(PieceColor currentSide, Board currentBoard, out List<Move> possibleMoves)
	{
		possibleMoves = new List<Move>();
		var piecesInPlay = currentBoard.Pieces.Count(x => x.Color == currentSide);
		var kingsInPlay = currentBoard.Pieces.Count(x => x.Color == currentSide && x.Promoted);

		var playingWithJustKings = piecesInPlay == kingsInPlay;

		if (playingWithJustKings)
		{
			var shortestDistance = 12.0;

			Piece? currentHero = null;
			Piece? currentVillain = null;

			foreach (var king in currentBoard.Pieces.Where(x => x.Color == currentSide))
			{
				foreach (var target in currentBoard.Pieces.Where(x => x.Color != currentSide))
				{
					var kingPoint = (king.XPosition, king.YPosition);
					var targetPoint = (target.XPosition, target.YPosition);
					var distance = VectorHelper.GetPointDistance(kingPoint, targetPoint);

					if (distance < shortestDistance)
					{
						shortestDistance = distance;
						currentHero = king;
						currentVillain = target;
					}
				}
			}

			if (currentHero != null && currentVillain != null)
			{
				var movementOptions = VectorHelper.WhereIsVillain(currentHero, currentVillain);

				foreach (var movementOption in movementOptions)
				{
					var squareStatus = currentBoard.GetSquareOccupancy(movementOption.X, movementOption.Y);

					if (squareStatus == PieceColor.NotSet)
					{
						var theMove = new Move
						{
							PieceToMove = currentHero,
							TypeOfMove = MoveType.EndGame,
							To = (movementOption.X, movementOption.Y)
						};

						if (!Board.IsValidPosition(movementOption.X, movementOption.Y))
						{
							continue;
						}

						possibleMoves.Add(theMove);

						break;
					}
				}
			}
		}

		return possibleMoves.Count > 0;
	}
}
