using Guess_A_Number;

ConsoleCommunicator user = new();

MysteryNumber mystery = new();

var guessingValue = true;
while (guessingValue)
{
	var input = user.GetInt($"Guess a number ({mystery.Min}-{mystery.Max})");

	string response;
	switch(MysteryNumber.Compare(input, mystery))
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