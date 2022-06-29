using System.Collections.ObjectModel;

namespace Chess;

public abstract class Piece
{
	public abstract string Icon { get; }
	protected PieceColor Color { get; private set; }

	private Boolean IsAlive { get; set; }

	protected Collection<MovementRule> Rules;

	protected Piece(PieceColor color)
	{
		Rules = new Collection<MovementRule>();

		Color = (color == PieceColor.White ? PieceColor.White : PieceColor.Black);

		IsAlive = true;

		InitRules();
	}

	public bool CheckIfValidMove(
		bool unitAttack, 
		int startRow, 
		int startColumn, 
		int endRow, 
		int endColumn)
	{
		var movement = new Move
		{
			UnitAttack = unitAttack,
			StartX = startColumn,
			StartY = startRow,
			EndX = endColumn,
			EndY = endRow
		};

		return Rules.Any(r => r.Validate(movement));
	}

	protected abstract void InitRules();
	
}