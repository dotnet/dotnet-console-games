namespace Chess;

public class Queen : Piece
{
	public override string Icon { get { return "♕"; } }

	public Queen(PieceColor color) : base(color) { }

	public override void InitRules()
	{
		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX
		));

		Rules.Add(new MovementRule(
			m => m.EndY == m.StartY
		));

		Rules.Add(new MovementRule(
			m => m.StartX - m.EndX == m.StartY - m.EndY
		));

		Rules.Add(new MovementRule(
			m => m.StartX - m.EndX == -( m.StartY - m.EndY )
		));
	}
}
