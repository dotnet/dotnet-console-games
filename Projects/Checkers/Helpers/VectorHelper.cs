namespace Checkers.Helpers;

/// <summary>
/// Track distance between 2 points
/// Used in endgame for kings to hunt down closest victim
/// </summary>
public static class VectorHelper
{
	public static double GetPointDistance((int X, int Y) first, (int X, int Y) second)
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
		double sideA = Math.Abs(first.Y - second.Y);
		double sideB = Math.Abs(first.X - second.X);
		return Math.Sqrt(Math.Pow(sideA, 2) + Math.Pow(sideB, 2));
	}

	public static List<(int X, int Y)> WhereIsVillain(Piece hero, Piece villain)
	{
		List<(int X, int Y)>? retVal = new();
		List<Direction>? directions = new();
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
					retVal.Add((hero.XPosition - 1, hero.YPosition - 1));
					retVal.Add((hero.XPosition + 1, hero.YPosition - 1));
					break;
				case Direction.Down:
					retVal.Add((hero.XPosition - 1, hero.YPosition + 1));
					retVal.Add((hero.XPosition + 1, hero.YPosition + 1));
					break;
				case Direction.Left:
					retVal.Add((hero.XPosition - 1, hero.YPosition - 1));
					retVal.Add((hero.XPosition - 1, hero.YPosition + 1));
					break;
				case Direction.Right:
					retVal.Add((hero.XPosition + 1, hero.YPosition - 1));
					retVal.Add((hero.XPosition + 1, hero.YPosition + 1));
					break;
				default:
					throw new NotImplementedException();
			}
		}
		else
		{
			if (directions.Contains(Direction.Left) && directions.Contains(Direction.Up))
			{
				retVal.Add((hero.XPosition - 1, hero.YPosition - 1));
			}
			if (directions.Contains(Direction.Left) && directions.Contains(Direction.Down))
			{
				retVal.Add((hero.XPosition - 1, hero.YPosition + 1));
			}
			if (directions.Contains(Direction.Right) && directions.Contains(Direction.Up))
			{
				retVal.Add((hero.XPosition + 1, hero.YPosition - 1));
			}
			if (directions.Contains(Direction.Right) && directions.Contains(Direction.Down))
			{
				retVal.Add((hero.XPosition + 1, hero.YPosition + 1));
			}
		}
		return retVal;
	}
}
