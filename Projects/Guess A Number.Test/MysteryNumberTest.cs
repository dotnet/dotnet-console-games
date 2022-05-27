namespace Guess_A_Number.Test;
using FluentAssertions;
using Xunit;

public class MysteryNumberTest
{

	[Fact]
	public void DefaultRangeTest()
	{
		var oneToOneHundred = new MysteryNumber();
		oneToOneHundred.Min.Should().Be(1);
		oneToOneHundred.Max.Should().Be(100);
	}

	[Fact]
	public void CompareTest()
	{
		var one = new MysteryNumber(1, 1);

		MysteryNumber.Compare(1, one).Should().Be(0);
		MysteryNumber.Compare(0, one).Should().BeNegative();
		MysteryNumber.Compare(2, one).Should().BePositive();
	}

}