namespace Checkers.Helpers;

public static class DisplayHelper
{
    public static (int X, int Y) GetScreenPositionFromBoardPosition((int X, int Y) boardPosition)
    {
        var actualX = (boardPosition.X * 2) + 7;
        var actualY = boardPosition.Y + 0 + 1;
        return (actualX, actualY);
    }
}

