namespace Chess;

public class Rook : Piece
{
	public override string Token { get { return "♖"; } }

	public Rook(PieceColor color) : base(color) { }

	public override void InitRules()
	{
		Rules.Add(new Rule(
				m => m.EndX == m.StartX
		));

		Rules.Add(new Rule(
				m => m.EndY == m.StartY
		));
	}
}
