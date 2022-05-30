namespace Console_Testing_Helper.Test;

using ConsoleTestingHelper;
using FluentAssertions;
using Xunit;

public class ConsoleOverriderTest
{
	/// <summary>
	/// I really wish this wasn't true, there doesn't seem to be a way to fix it though.
	/// </summary>
	[Fact(Skip = "can't do it")]
	public void HandleClearTest()
	{
		using var c = new ConsoleOverrider();
		Console.Clear(); //per spec throws an IOException due to SetOut
		//need to find a way around this
	}

	[Fact(Skip ="can't do it")]
	public void ReadKeyTest()
	{
		using var c = new ConsoleOverrider();
		//there is nothing you can do to feed a key in
		Console.ReadKey(); //hangs forever
	}

	[Fact]
	public void OutputMultiCallTest()
	{
		using var c = new ConsoleOverrider();
		Console.WriteLine("hello");
		c.ReadOutput(o => {
			o.ReadLine().Should().Be("hello");
		});

		Console.WriteLine("world");
		c.ReadOutput(o => {
			o.ReadLine().Should().Be("world");
		});

		c.ReadOutput(o => {
			o.ReadLine().Should().BeNull();
		});
	}

	[Fact]
	public void OutputTest()
	{
		using var c = new ConsoleOverrider();
		Console.WriteLine("hello");
		Console.WriteLine("world");
		c.ReadOutput(o => {
			o.ReadLine().Should().Be("hello");
			o.ReadLine().Should().Be("world");
			o.ReadLine().Should().BeNull();
		});
	}
}