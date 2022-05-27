namespace Guess_A_Number.Test;

using Guess_A_Number;
using FluentAssertions;
using Xunit;
using System.Diagnostics;
using System;

public class ConsoleCommunicatorTest
{

	[Fact]
	public void GetIntTest()
	{
		using var overrideConsole = new ConsoleOverrider();

		overrideConsole.AddInput(i =>
		{
			i.WriteLine("foo");
			i.WriteLine("5");
		});
		var result = new ConsoleCommunicator().GetInt("whatever");
		result.Should().Be(5);
		overrideConsole.ReadOutput(o => o.ReadLine().Should().StartWith("whatever"));
	}

	[Fact]
	public void TellTest()
	{
		using var overrideConsole = new ConsoleOverrider();
		new ConsoleCommunicator().Tell("hello world");
		overrideConsole.ReadOutput(o => o.ReadLine().Should().Be("hello world"));
	}

	[Fact(Skip = "can't test readkey")]
	public void WaitTest()
	{
		using var overrideConsole = new ConsoleOverrider();
		new ConsoleCommunicator().Wait();
	}

	[Fact]
	public void EndToEndTest()
	{
		var bin = Environment.CurrentDirectory.Replace(".Test","") + "\\Guess A Number.exe";

		var processInfo = new ProcessStartInfo {
			FileName = bin,
			WindowStyle = ProcessWindowStyle.Hidden,
			CreateNoWindow = true,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			RedirectStandardInput = true,
		};
		
		var min = 1;
		var max = 100;
		var guess = min + (max - min) / 2;

		var process = Process.Start(processInfo)
			?? throw new Exception("create process failed");

		var going = true;
		var rounds = 0;
		while(going)
		{
			rounds++;
			process.StandardInput.WriteLine(guess);
			var response = process.StandardOutput.ReadLine()
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
		rounds.Should().BeLessThan(9);
	}
}
