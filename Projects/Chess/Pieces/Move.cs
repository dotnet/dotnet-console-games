namespace Chess;

public record Move
{
	public bool UnitAttack;
	public int StartX, StartY;
	public int EndX, EndY;    
}