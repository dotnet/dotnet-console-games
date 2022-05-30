using FluentAssertions;
using Xunit;
using ConsoleTestingHelper;

namespace Rock_Paper_Scissors.Test;

public class HumanPlayerTest
{
	[Fact]
	public void GetMove()
	{
		using var console = new ConsoleOverrider();
		console.WriteInput(i => i.WriteLine("s"));

		new HumanPlayer().GetMove()
			.Should().Be(Move.Scissors);

		console.WriteInput(i => i.WriteLine("e"));
		new HumanPlayer().GetMove()
			.Should().Be(Move.GiveUp);

		console.WriteInput(i => i.WriteLine("r"));
		new HumanPlayer().GetMove()
			.Should().Be(Move.Rock);

		console.WriteInput(i => i.WriteLine("p"));
		new HumanPlayer().GetMove()
			.Should().Be(Move.Paper);
	}

	[Fact]
	public void EndToEndTest()
	{
		var app = EndToEndHelper.Run(typeof(PaperScissorsRocks));

		app.StandardInput.WriteLine("rock");
		app.StandardInput.WriteLine("paper");
		app.StandardInput.WriteLine("scissors");
		app.StandardInput.WriteLine("exit");
		var log = app.StandardOutput.ReadToEnd();

		log.Should().NotBeEmpty();
		app.StandardError.ReadToEnd().Should().BeEmpty();
	}
}