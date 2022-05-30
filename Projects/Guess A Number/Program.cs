using Guess_A_Number;

HumanPlayer user = new();

MysteryNumber mystery = new();

var guessingValue = true;
while (guessingValue)
{
	var guess = user.GetInt($"Guess a number ({mystery.Min}-{mystery.Max})");

	string response;
	switch(MysteryNumber.Compare(guess, mystery))
	{
		case < 0:
			response = "Incorrect. Too Low.";
			break;

		case > 0:
			response = "Incorrect. Too High.";
			break;

		case 0:
			response = "You guessed it!";
			guessingValue = false;
			break;
	}

	user.Tell(response);
}

user.Tell("Press any key to exit...");
user.Wait();