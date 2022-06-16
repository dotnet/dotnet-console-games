using System.Drawing;
using Checkers.Types;

namespace Checkers.Helpers;

    /// <summary>
    /// Track distance between 2 points
    /// Used in endgame for kings to hunt down closest victim
    /// </summary>
    public static class VectorHelper
    {
        public static double GetPointDistance(Point first, Point second)
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
            var sideA = (double)Math.Abs(first.Y - second.Y);
            var sideB = (double)Math.Abs(first.X - second.X);

            return Math.Sqrt(Math.Pow(sideA, 2) + Math.Pow(sideB, 2));
        }

        public static List<Point> WhereIsVillain(Piece hero, Piece villain)
        {
            var retVal = new List<Point>();

            var directions = new List<Direction>();

            if (hero.XPosition > villain.XPosition)
            {
                directions.Add(Direction.Left);
            }

            if (hero.XPosition < villain.XPosition)
            {
                directions.Add(Direction.Right);
            }

            if (hero.YPosition > villain.YPosition)
            {
                directions.Add(Direction.Up);
            }

            if (hero.YPosition < villain.YPosition)
            {
                directions.Add(Direction.Down);
            }

            if (directions.Count == 1)
            {
                switch (directions[0])
                {
                    case Direction.Up:
                        retVal.Add(new Point(hero.XPosition - 1, hero.YPosition - 1));
                        retVal.Add(new Point(hero.XPosition + 1, hero.YPosition - 1));

                        break;
                    case Direction.Down:
                        retVal.Add(new Point(hero.XPosition - 1, hero.YPosition + 1));
                        retVal.Add(new Point(hero.XPosition + 1, hero.YPosition + 1));

                        break;
                    case Direction.Left:
                        retVal.Add(new Point(hero.XPosition - 1, hero.YPosition - 1));
                        retVal.Add(new Point(hero.XPosition - 1, hero.YPosition + 1));

                        break;
                    case Direction.Right:
                        retVal.Add(new Point(hero.XPosition + 1, hero.YPosition - 1));
                        retVal.Add(new Point(hero.XPosition + 1, hero.YPosition + 1));

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                if (directions.Contains(Direction.Left) && directions.Contains(Direction.Up))
                {
                    retVal.Add(new Point(hero.XPosition - 1, hero.YPosition - 1));
                }

                if (directions.Contains(Direction.Left) && directions.Contains(Direction.Down))
                {
                    retVal.Add(new Point(hero.XPosition - 1, hero.YPosition + 1));
                }

                if (directions.Contains(Direction.Right) && directions.Contains(Direction.Up))
                {
                    retVal.Add(new Point(hero.XPosition + 1, hero.YPosition - 1));
                }

                if (directions.Contains(Direction.Right) && directions.Contains(Direction.Down))
                {
                    retVal.Add(new Point(hero.XPosition + 1, hero.YPosition + 1));
                }
            }

            return retVal;
        }
    }
