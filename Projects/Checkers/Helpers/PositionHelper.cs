namespace Checkers.Helpers;

/// <summary>Position routines</summary>
public static class PositionHelper
{
    public static string ToPositionNotationString(int x, int y)
    {
        if (!PointValid(x, y)) throw new ArgumentException("Not a valid position!");
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
        return (notation[0] - 'A', '8' - notation[1]);
    }

    public static bool PointValid(int x, int y) =>
        0 <= x && x < 8 &&
        0 <= y && y < 8;
}

