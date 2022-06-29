namespace Chess;

public class MovementRule
{
	private int Id { get; set; }

	private readonly Predicate<Move>[] _predicates;

	internal MovementRule(params Predicate<Move>[] predicates)
	{
		_predicates = predicates;
	}

	internal MovementRule(int id, params Predicate<Move>[] predicates) : this(predicates)
	{
		Id = id;
	}

	public bool Validate(Move move)
	{
		return _predicates.All(p => p(move));
	}
}