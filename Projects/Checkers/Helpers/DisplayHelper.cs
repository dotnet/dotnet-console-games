using System.Drawing;

namespace Checkers.Helpers;

public static class DisplayHelper
{
    public static Point GetScreenPositionFromBoardPosition(Point boardPosition)
    {
        var actualX = (boardPosition.X * 2) + 2;
        var actualY = boardPosition.Y + 0;

        return new Point(actualX, actualY);
    }
}

