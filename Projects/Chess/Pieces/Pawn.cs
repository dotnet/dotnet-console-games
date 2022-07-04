namespace Chess;

public class Pawn : Piece
{
	public override string Icon => "♙";

	public Pawn(PieceColor color) : base(color) { }

	 public override void InitRules()
	{
		// valid movement for a black pawn
		Rules.Add(new MovementRule(
			m => Color == PieceColor.Black,
			m => !m.UnitAttack,
			m => m.EndX == m.StartX,
			m => m.EndY == m.StartY - 1
		));

		Rules.Add(new MovementRule(
			m => Color == PieceColor.Black,
			m => !m.UnitAttack,
			m => m.StartY == 6,
			m => m.EndX == m.StartX,
			m => m.EndY == m.StartY - 2
		));

		Rules.Add(new MovementRule(
			m => Color == PieceColor.Black,
			m => m.UnitAttack,
			m => m.EndX == m.StartX + 1,
			m => m.EndY == m.StartY - 1
		));

		Rules.Add(new MovementRule(
			m => Color == PieceColor.Black,
			m => m.UnitAttack,
			m => m.EndX == m.StartX - 1,
			m => m.EndY == m.StartY - 1
		));

		// valid movement for a white pawn
		Rules.Add(new MovementRule(
			m => Color == PieceColor.White,
			m => !m.UnitAttack,
			m => m.EndX == m.StartX,
			m => m.EndY == m.StartY + 1
		));

		Rules.Add(new MovementRule(
			m => Color == PieceColor.White,
			m => !m.UnitAttack,
			m => m.StartY == 1,
			m => m.EndX == m.StartX,
			m => m.EndY == m.StartY + 2
		));

		Rules.Add(new MovementRule(
			m => Color == PieceColor.White,
			m => m.UnitAttack,
			m => m.EndX == m.StartX + 1,
			m => m.EndY == m.StartY + 1
		));

		Rules.Add(new MovementRule(
			m => Color == PieceColor.White,
			m => m.UnitAttack,
			m => m.EndX == m.StartX - 1,
			m => m.EndY == m.StartY + 1
		));
	}
}