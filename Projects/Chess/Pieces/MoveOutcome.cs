namespace Chess;

public record MoveOutcome
{
	public bool Attack { get; set; }
	public Piece CapturedPiece { get; set; }
}