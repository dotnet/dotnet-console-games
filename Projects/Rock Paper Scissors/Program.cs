using Rock_Paper_Scissors;

//var game = new PaperScissorsRocks(new RandomPlayer(), new RandomPlayer());
var game = new PaperScissorsRocks(new HumanPlayer(), new RandomPlayer());

var stillPlaying = true;
while (stillPlaying)
{
	stillPlaying = game.PlayRound();
}
