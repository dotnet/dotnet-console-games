namespace Guess_A_Number.Test;

using Guess_A_Number;
using FluentAssertions;
using Xunit;
using System.Diagnostics;
using System;
using ConsoleTestingHelper;

public class HumanPlayerTest
{

	[Fact]
	public void GetIntTest()
	{
		using var overrideConsole = new ConsoleOverrider();

		overrideConsole.WriteInput(i =>
		{
			i.WriteLine("foo");
			i.WriteLine("5");
		});
		var result = new HumanPlayer().GetInt("whatever");
		result.Should().Be(5);
		overrideConsole.ReadOutput(o => o.ReadLine().Should().StartWith("whatever"));
	}

	[Fact]
	public void TellTest()
	{
		using var overrideConsole = new ConsoleOverrider();
		new HumanPlayer().Tell("hello world");
		overrideConsole.ReadOutput(o => o.ReadLine().Should().Be("hello world"));
	}

	[Fact]
	public void EndToEndTest()
	{
		var app = EndToEndHelper.Run(typeof(MysteryNumber));

		var min = 1;
		var max = 100;
		var guess = min + (max - min) / 2;

		var going = true;
		var rounds = 0;
		while(going)
		{
			rounds++;
			app.StandardInput.WriteLine(guess);
			var response = app.StandardOutput.ReadLine()
				?? throw new Exception("no output?!");
			if (response.EndsWith("Too High."))
			{
				max = guess - 1;
				var delta = (guess - min) / 2;
				if (delta == 0) delta = 1;
				guess -= delta;
			}
			else if (response.EndsWith("Too Low."))
			{
				min = guess + 1;
				var delta = (max - guess) / 2;
				if (delta == 0) delta = 1;
				guess += delta;
			}
			else
				going = false;
		}

		app.StandardError.ReadToEnd().Should().BeEmpty();
		rounds.Should().BeLessThan(9);
	}
}
