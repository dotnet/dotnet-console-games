using Checkers.Data;
using Checkers.Types;

namespace Checkers.Helpers;

/// <summary>
/// Board related routines
/// </summary>
public static class BoardHelper
{
    public static List<Piece> GetStartingPosition()
    {
        // HACK: Can set to false and define a custom start position in GetLimitedStartingPosition
        const bool UseDefault = true;

#pragma warning disable CS0162
        return UseDefault ? GetDefaultStartingPosition() : GetLimitedStartingPosition();
#pragma warning restore CS0162
    }

    private static List<Piece> GetLimitedStartingPosition()
    {
        return KnowledgeBase.GetLimitedStartingPosition();
    }

    public static List<Piece> GetDefaultStartingPosition()
    {
        return KnowledgeBase.GetStartingPosition();
    }

    public static PieceColour GetSquareOccupancy(int xp, int yp, Board currentBoard)
    {
        var piece = currentBoard.Pieces.FirstOrDefault(x => x.XPosition == xp && x.InPlay && x.YPosition == yp);

        return piece == null ? PieceColour.NotSet : piece.Side;
    }

    public static Piece? GetPieceAt(int xp, int yp, Board currentBoard)
    {
        var retVal = currentBoard.Pieces.FirstOrDefault(x => x.XPosition == xp && x.InPlay && x.YPosition == yp);

        if (retVal == null)
        {
            return default;
        }

        return retVal;
    }

    public static int GetNumberOfWhitePiecesInPlay(Board currentBoard)
    {
        return GetNumberOfPiecesInPlay(currentBoard, PieceColour.White);
    }

    public static int GetNumberOfBlackPiecesInPlay(Board currentBoard)
    {
        return GetNumberOfPiecesInPlay(currentBoard, PieceColour.Black);
    }

    private static int GetNumberOfPiecesInPlay(Board currentBoard, PieceColour currentSide)
    {
        return currentBoard.Pieces.Count(x => x.Side == currentSide && x.InPlay);
    }
}

