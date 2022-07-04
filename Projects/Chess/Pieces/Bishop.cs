namespace Chess;

public class Bishop : Piece
{
	public override string Icon => "♗";

	public Bishop(PieceColor color) : base(color) { }

	public override void InitRules()
	{
		Rules.Add(new MovementRule(
			m => m.StartX - m.EndX == m.StartY - m.EndY
		));

		Rules.Add(new MovementRule(
			m => m.StartX - m.EndX == -(m.StartY - m.EndY)
		));
	}
}