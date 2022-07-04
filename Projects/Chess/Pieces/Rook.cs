namespace Chess;

public class Rook : Piece
{
	public override string Icon { get { return "♖"; } }

	public Rook(PieceColor color) : base(color) { }

	public override void InitRules()
	{
		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX
		));

		Rules.Add(new MovementRule(
			m => m.EndY == m.StartY
		));
	}
}
