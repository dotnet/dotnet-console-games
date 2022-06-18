﻿using System.Drawing;

namespace Checkers;

public static class Engine
{
    public static MoveOutcome PlayNextMove(PieceColour currentSide, Board currentBoard, Point? playerFrom = null, Point? playerTo = null)
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
                if (MoveIsValid(playerFrom, (Point)playerTo, possibleMoves, currentBoard, out var selectedMove))
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
                                throw new ArgumentOutOfRangeException();
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
            outcome = currentSide == PieceColour.Black ? MoveOutcome.WhiteWin : MoveOutcome.BlackWin;
        }

        switch (outcome)
        {
            case MoveOutcome.EndGame:
            case MoveOutcome.ValidMoves:
                {
                    var bestMove = possibleMoves.MinBy(x => x.Weighting);

                    if (bestMove == null)
                    {
                        throw new ArgumentNullException(nameof(bestMove));
                    }

                    var pieceToMove = bestMove.PieceToMove;

                    if (pieceToMove == null)
                    {
                        throw new ArgumentNullException(nameof(pieceToMove));
                    }

                    var newX = bestMove.To.X;
                    var newY = bestMove.To.Y;

                    var from = PositionHelper.GetNotationPosition(pieceToMove.XPosition, pieceToMove.YPosition);

                    pieceToMove.XPosition = newX;
                    pieceToMove.YPosition = newY;

                    var to = PositionHelper.GetNotationPosition(pieceToMove.XPosition, pieceToMove.YPosition);

                    var blackPieces = BoardHelper.GetNumberOfBlackPiecesInPlay(currentBoard);
                    var whitePieces = BoardHelper.GetNumberOfWhitePiecesInPlay(currentBoard);

                    // Promotion can only happen if not already a king and you have reached the far side
                    if (newY is 1 or 8 && pieceToMove.Promoted == false)
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

    private static bool MoreCapturesAvailable(PieceColour currentSide, Board currentBoard)
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

    private static bool MoveIsValid(Point? from, Point to, List<Move> possibleMoves, Board currentBoard, out Move? selectedMove)
    {
        selectedMove = default;

        if (from == null)
        {
            return false;
        }

        var selectedPiece = BoardHelper.GetPieceAt(from.Value.X, from.Value.Y, currentBoard);

        foreach (var move in possibleMoves.Where(move => move.PieceToMove == selectedPiece && move.To == to))
        {
            selectedMove = move;

            return true;
        }

        return false;
    }

    private static MoveOutcome GetAllPossiblePlayerMoves(PieceColour currentSide, Board currentBoard,
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

    private static bool PerformCapture(PieceColour currentSide, List<Move> possibleCaptures, Board currentBoard)
    {
        var captureMove = possibleCaptures.FirstOrDefault(x => x.Capturing != null);
        var from = string.Empty;
        var to = string.Empty;

        if (captureMove != null)
        {
            var squareToCapture = captureMove.Capturing;

            if (squareToCapture != null)
            {
                var deadMan =
                    BoardHelper.GetPieceAt(squareToCapture.Value.X, squareToCapture.Value.Y, currentBoard);

                if (deadMan != null)
                {
                    deadMan.InPlay = false;
                }

                if (captureMove.PieceToMove != null)
                {
                    captureMove.PieceToMove.Aggressor = true;
                    from = PositionHelper.GetNotationPosition(captureMove.PieceToMove.XPosition, captureMove.PieceToMove.YPosition);

                    captureMove.PieceToMove.XPosition = captureMove.To.X;
                    captureMove.PieceToMove.YPosition = captureMove.To.Y;

                    to = PositionHelper.GetNotationPosition(captureMove.PieceToMove.XPosition, captureMove.PieceToMove.YPosition);

                }
            }
        }

        var anyPromoted = CheckForPiecesToPromote(currentSide, currentBoard);
        var blackPieces = BoardHelper.GetNumberOfBlackPiecesInPlay(currentBoard);
        var whitePieces = BoardHelper.GetNumberOfWhitePiecesInPlay(currentBoard);

        var playerAction = anyPromoted ? PlayerAction.CapturePromotion : PlayerAction.Capture;

        LoggingHelper.LogMove(from, to, playerAction, currentSide, blackPieces, whitePieces);

        return anyPromoted;
    }

    private static bool CheckForPiecesToPromote(PieceColour currentSide, Board currentBoard)
    {
        var retVal = false;
        var promotionSpot = currentSide == PieceColour.White ? 8 : 1;

        foreach (var piece in currentBoard.Pieces.Where(x => x.Side == currentSide))
        {
            if (promotionSpot == piece.YPosition && !piece.Promoted)
            {
                piece.Promoted = retVal = true;
            }
        }

        return retVal;
    }

    private static MoveOutcome AnalysePosition(PieceColour currentSide, Board currentBoard,
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

    private static void GetPossibleMovesAndAttacks(PieceColour currentSide, Board currentBoard,
        out List<Move> possibleMoves)
    {
        possibleMoves = new List<Move>();

        foreach (var piece in currentBoard.Pieces.Where(c => c.Side == currentSide && c.InPlay))
        {
            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
                {
                    if (x == 0 || y == 0)
                    {
                        continue;
                    }

                    if (!piece.Promoted)
                    {
                        switch (currentSide)
                        {
                            case PieceColour.White when y == -1:
                            case PieceColour.Black when y == 1:
                                continue;
                        }
                    }

                    var currentX = piece.XPosition + x;
                    var currentY = piece.YPosition + y;

                    if (!PositionHelper.PointValid(currentX, currentY))
                    {
                        continue;
                    }

                    var targetSquare = BoardHelper.GetSquareOccupancy(currentX, currentY, currentBoard);

                    if (targetSquare == PieceColour.NotSet)
                    {
                        if (!PositionHelper.PointValid(currentX, currentY))
                        {
                            continue;
                        }

                        var newMove = new Move { PieceToMove = piece, TypeOfMove = MoveType.StandardMove, To = new Point { X = currentX, Y = currentY } };
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

                        if (!PositionHelper.PointValid(beyondX, beyondY))
                        {
                            continue;
                        }

                        var beyondSquare = BoardHelper.GetSquareOccupancy(beyondX, beyondY, currentBoard);

                        if (beyondSquare != PieceColour.NotSet)
                        {
                            continue;
                        }

                        var attack = new Move { PieceToMove = piece, TypeOfMove = MoveType.Capture, To = new Point { X = beyondX, Y = beyondY }, Capturing = new Point { X = currentX, Y = currentY } };
                        possibleMoves.Add(attack);
                    }
                }
            }
        }
    }

    private static Point DeriveToPosition(int pieceX, int pieceY, int captureX, int captureY)
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

        return new Point(newX, newY);
    }

    private static bool PlayingWithJustKings(PieceColour currentSide, Board currentBoard, out List<Move> possibleMoves)
    {
        possibleMoves = new List<Move>();
        var piecesInPlay = currentBoard.Pieces.Count(x => x.Side == currentSide && x.InPlay);
        var kingsInPlay = currentBoard.Pieces.Count(x => x.Side == currentSide && x.InPlay && x.Promoted);

        var playingWithJustKings = piecesInPlay == kingsInPlay;

        if (playingWithJustKings)
        {
            var shortestDistance = 12.0;

            Piece? currentHero = null;
            Piece? currentVillain = null;

            foreach (var king in currentBoard.Pieces.Where(x => x.Side == currentSide && x.InPlay))
            {
                foreach (var target in currentBoard.Pieces.Where(x => x.Side != currentSide && x.InPlay))
                {
                    var kingPoint = new Point(king.XPosition, king.YPosition);
                    var targetPoint = new Point(target.XPosition, target.YPosition);
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
                    var squareStatus = BoardHelper.GetSquareOccupancy(movementOption.X, movementOption.Y, currentBoard);

                    if (squareStatus == PieceColour.NotSet)
                    {
                        var theMove = new Move
                        {
                            PieceToMove = currentHero,
                            TypeOfMove = MoveType.EndGame,
                            To = new Point(movementOption.X, movementOption.Y)
                        };

                        if (!PositionHelper.PointValid(movementOption.X, movementOption.Y))
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

