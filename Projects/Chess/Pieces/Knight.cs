namespace Chess;

public class Knight : Piece
{
	public override string Icon => "♘";

	public Knight(PieceColor color) : base(color) { }

	public override void InitRules()
	{
		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX + 2,
			m => m.EndY == m.StartY + 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX + 2,
			m => m.EndY == m.StartY - 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX - 2,
			m => m.EndY == m.StartY + 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX - 2,
			m => m.EndY == m.StartY - 1
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX + 1,
			m => m.EndY == m.StartY + 2
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX - 1,
			m => m.EndY == m.StartY + 2
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX - 1,
			m => m.EndY == m.StartY - 2
		));

		Rules.Add(new MovementRule(
			m => m.EndX == m.StartX + 1,
			m => m.EndY == m.StartY - 2
		));
	}
}
