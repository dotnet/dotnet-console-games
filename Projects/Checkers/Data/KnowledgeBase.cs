namespace Checkers.Data;

/// <summary>
/// Stores the starting position and any custom permutation for testing
/// </summary>
public static class KnowledgeBase
{
	public static List<Piece> GetStartingPosition()
	{
		var retVal = new List<Piece>
			{
				new() { NotationPosition ="A3", Side = PieceColour.Black},
				new() { NotationPosition ="A1", Side = PieceColour.Black},
				new() { NotationPosition ="B2", Side = PieceColour.Black},
				new() { NotationPosition ="C3", Side = PieceColour.Black},
				new() { NotationPosition ="C1", Side = PieceColour.Black},
				new() { NotationPosition ="D2", Side = PieceColour.Black},
				new() { NotationPosition ="E3", Side = PieceColour.Black},
				new() { NotationPosition ="E1", Side = PieceColour.Black},
				new() { NotationPosition ="F2", Side = PieceColour.Black},
				new() { NotationPosition ="G3", Side = PieceColour.Black},
				new() { NotationPosition ="G1", Side = PieceColour.Black},
				new() { NotationPosition ="H2", Side = PieceColour.Black},

				new() { NotationPosition ="A7", Side = PieceColour.White},
				new() { NotationPosition ="B8", Side = PieceColour.White},
				new() { NotationPosition ="B6", Side = PieceColour.White},
				new() { NotationPosition ="C7", Side = PieceColour.White},
				new() { NotationPosition ="D8", Side = PieceColour.White},
				new() { NotationPosition ="D6", Side = PieceColour.White},
				new() { NotationPosition ="E7", Side = PieceColour.White},
				new() { NotationPosition ="F8", Side = PieceColour.White},
				new() { NotationPosition ="F6", Side = PieceColour.White},
				new() { NotationPosition ="G7", Side = PieceColour.White},
				new() { NotationPosition ="H8", Side = PieceColour.White},
				new() { NotationPosition ="H6", Side = PieceColour.White}
			};

		return retVal;
	}

	public static List<Piece> GetLimitedStartingPosition() => new()
		{
			new() { NotationPosition = "H2", Side = PieceColour.Black},
			new() { NotationPosition = "A1", Side = PieceColour.Black},
			new() { NotationPosition = "G3", Side = PieceColour.White},
			new() { NotationPosition = "E5", Side = PieceColour.White}
		};
}
