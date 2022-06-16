using System.Drawing;

namespace Checkers.Helpers;

    /// <summary>
    /// Position routines
    /// </summary>
    public static class PositionHelper
    {
        public static string GetNotationPosition(int x, int y)
        {
            if (!PointValid(x, y))
            {
                throw new ArgumentException("Not a valid position!");
            }

            var yPortion = 9 - y;

            var xPortion = Convert.ToChar(x + 64);

            return xPortion + yPortion.ToString();
        }

        public static Point? GetPositionByNotation(string notation)
        {
            const int ExpectedPositionLength = 2;

            notation = notation.Trim();
            
            if (notation.Length != ExpectedPositionLength)
            {
                return default;
            }

            try
            {
                var letterPart = notation.Substring(0, 1);
                var numberPart = notation.Substring(1, 1);

                var x = letterPart.ToUpper().ToCharArray()[0] - 64;
                var y = 9 - Convert.ToInt32(numberPart);

                return new Point(x, y);
            }
            catch
            {
                return default;
            }
        }

        public static bool PointValid(int x, int y)
        {
            return x is > 0 and < 9 && y is > 0 and < 9;
        }
    }
