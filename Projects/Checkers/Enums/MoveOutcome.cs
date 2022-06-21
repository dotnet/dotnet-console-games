namespace Checkers.Enums;

public enum MoveOutcome
{
	Unknown,
	ValidMoves,
	Capture,
	CaptureMoreAvailable,
	EndGame,    // Playing with kings with prey to hunt
	NoMoveAvailable,
	WhiteWin,
	BlackWin
}
