namespace Chess;

// TODO: Delete this entire file once it is no longer useful
public struct PieceTypeToken
{
	// White tokens
	public const string PawnToken = "\u2659";
	public const string KnightToken = "\u2658";
	public const string BishopToken = "\u2657";
	public const string RookToken = "\u2656";
	public const string QueenToken = "\u2655";
	public const string KingToken = "\u2654";

}

public enum PieceType
{
	Pawn = 1,
	Knight = 2,
	Bishop = 4,
	Rook = 8,
	Queen = 16,
	King = 32
}