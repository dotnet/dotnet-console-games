namespace Chess;

public class King : Piece
{
	public override string Icon { get { return "♔"; } }

	public King(PieceColor color) : base(color) { }

	public override void InitRules()
	{
		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX + 1,
			m => m.EndY == m.StartY + 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX - 1,
			m => m.EndY == m.StartY - 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX - 1,
			m => m.EndY == m.StartY + 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX + 1,
			m => m.EndY == m.StartY - 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX + 1,
			m => m.EndY == m.StartY
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX,
			m => m.EndY == m.StartY + 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX - 1,
			m => m.EndY == m.StartY
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX,
			m => m.EndY == m.StartY - 1
		));
	}
}
