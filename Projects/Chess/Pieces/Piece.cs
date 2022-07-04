using System.Collections.ObjectModel;

namespace Chess;

public abstract class Piece
{
	public abstract string Icon { get; set; }
	public PieceColor Color { get; private set; }

	public Boolean IsAlive { get; set; }

	public Collection<MovementRule> Rules;

	public Piece(PieceColor color)
	{
		Rules = new Collection<MovementRule>();

		Color = color == PieceColor.White ? PieceColor.White : PieceColor.Black;
		
		Icon = color == PieceColor.White ? "" : "";

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

	public abstract void InitRules();
	
}