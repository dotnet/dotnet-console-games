using Checkers.Types;

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
				new() { InitialPosition ="A3", Side = PieceColour.Black},
				new() { InitialPosition ="A1", Side = PieceColour.Black},
				new() { InitialPosition ="B2", Side = PieceColour.Black},
				new() { InitialPosition ="C3", Side = PieceColour.Black},
				new() { InitialPosition ="C1", Side = PieceColour.Black},
				new() { InitialPosition ="D2", Side = PieceColour.Black},
				new() { InitialPosition ="E3", Side = PieceColour.Black},
				new() { InitialPosition ="E1", Side = PieceColour.Black},
				new() { InitialPosition ="F2", Side = PieceColour.Black},
				new() { InitialPosition ="G3", Side = PieceColour.Black},
				new() { InitialPosition ="G1", Side = PieceColour.Black},
				new() { InitialPosition ="H2", Side = PieceColour.Black},
				new() { InitialPosition ="A7",Side = PieceColour.White},
				new() { InitialPosition ="B8",Side = PieceColour.White},
				new() { InitialPosition ="B6",Side = PieceColour.White},
				new() { InitialPosition ="C7",Side = PieceColour.White},
				new() { InitialPosition ="D8",Side = PieceColour.White},
				new() { InitialPosition ="D6",Side = PieceColour.White},
				new() { InitialPosition ="E7",Side = PieceColour.White},
				new() { InitialPosition ="F8",Side = PieceColour.White},
				new() { InitialPosition ="F6",Side = PieceColour.White},
				new() { InitialPosition ="G7",Side = PieceColour.White},
				new() { InitialPosition ="H8",Side = PieceColour.White},
				new() { InitialPosition ="H6",Side = PieceColour.White}
			};

		return retVal;
	}

	public static List<Piece> GetLimitedStartingPosition()
	{
		var retVal = new List<Piece>
			{
				new () { InitialPosition ="H2", Side = PieceColour.Black},
				new () { InitialPosition ="A1", Side = PieceColour.Black},
				new () { InitialPosition ="G3", Side = PieceColour.White},
				new() { InitialPosition = "E5", Side = PieceColour.White}
			};

		return retVal;
	}


}

