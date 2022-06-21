namespace Checkers;

public enum MoveOutcome
{
	ValidMoves           = 1,
	Capture              = 2,
	CaptureMoreAvailable = 3,
	EndGame              = 4, // Playing with kings with prey to hunt
	NoMoveAvailable      = 5,
	WhiteWin             = 6,
	BlackWin             = 7,
}
