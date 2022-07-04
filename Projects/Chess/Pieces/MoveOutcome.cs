namespace Chess;

public class MoveOutcome
{
	public MoveOutcome()
	{
		IsSuccess = true;
	}

	public bool IsSuccess { get; set; }

	public bool Attack { get; set; }

	public Piece CapturedPiece { get; set; }
}