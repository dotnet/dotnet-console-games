using System;

namespace Guess_A_Number;

public struct MysteryNumber
{
	public int Min { get; init; }
	public int Max { get; init; }
	private int current;
	public MysteryNumber(int minInclusive, int maxInclusive)
	{
		Min = minInclusive;
		Max = maxInclusive;

		current = Random.Shared.Next(Min, Max + 1);
	}
	public MysteryNumber()
		: this(1, 100)
	{

	}

	public static int Compare(int number, MysteryNumber mystery) => number.CompareTo(mystery.current);

}