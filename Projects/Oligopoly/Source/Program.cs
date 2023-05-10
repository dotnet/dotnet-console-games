using System.Xml;
using System.Xml.Serialization;

namespace Oligopoly.Source
{
    public class Program
    {
        /// <summary>
        ///  Program entry point.
        /// </summary>
        /// <param name="args">The array of strings to process.</param>
        public static void Main(string[] args)
        {
            RunMainMenu();
        }

        /// <summary>
        /// Runs the main menu of the game.
        /// </summary>
        private static void RunMainMenu()
        {
            string prompt = @"
 ██████╗ ██╗     ██╗ ██████╗  ██████╗ ██████╗  ██████╗ ██╗  ██╗   ██╗
██╔═══██╗██║     ██║██╔════╝ ██╔═══██╗██╔══██╗██╔═══██╗██║  ╚██╗ ██╔╝
██║   ██║██║     ██║██║  ███╗██║   ██║██████╔╝██║   ██║██║   ╚████╔╝ 
██║   ██║██║     ██║██║   ██║██║   ██║██╔═══╝ ██║   ██║██║    ╚██╔╝  
╚██████╔╝███████╗██║╚██████╔╝╚██████╔╝██║     ╚██████╔╝███████╗██║   
 ╚═════╝ ╚══════╝╚═╝ ╚═════╝  ╚═════╝ ╚═╝      ╚═════╝ ╚══════╝╚═╝   
        --Use up and down arrow keys to select an option--                                                                     
";
            string[] options = { "Play", "About", "Exit" };

            Menu mainMenu = new Menu(prompt, options, 0);

            int selectedOption = mainMenu.RunMenu();

            switch (selectedOption)
            {
                case 0:
                    RunSkipMenu();
                    break;
                case 1:
                    DisplayAboutInfo();
                    RunMainMenu();
                    break;
                case 2:
                    ExitGame();
                    break;
            }
        }

        /// <summary>
        /// Runs skip menu.
        /// </summary>
        private static void RunSkipMenu()
        {
            string prompt = @"Welcome to Oligopoly!
Do you want to read the introductory letter or do you want to jump right into the gameplay?

";
            string[] options = { "Read introductory letter", "Skip introductory letter" };

            Menu skipMenu = new Menu(prompt, options, 0);

            int selectedOption = skipMenu.RunMenu();

            switch (selectedOption)
            {
                case 0:
                    RunStartMenu();
                    break;
                case 1:
                    RunGameMenu();
                    break;
            }
        }

        /// <summary>
        /// Runs the start menu of the game.
        /// </summary>
        private static void RunStartMenu()
        {
            string prompt = @"Dear, new CEO
    On behalf of the board of directors of Oligopoly Investments, we would like to congratulate you on becoming our new CEO. We are confident that you will lead our company to new heights of success and innovation.
    As CEO, you now have access to our exclusive internal software called Oligopoly, where you can track the latest news from leading companies and buy and sell their shares. This software will give you an edge over the competition and help you make important decisions for our company. To access the program, simply click the button at the bottom of this email.
    We look forward to working with you and supporting you in your new role.

Sincerely,
The board of directors of Oligopoly Investments

";
            string[] options = { "Get Access" };

            Menu startMenu = new Menu(prompt, options, 15);

            int selectedOption = startMenu.RunMenu();

            switch (selectedOption)
            {
                case 0:
                    Console.Clear();
                    RunGameMenu();
                    break;
            }
        }

        /// <summary>
        /// Runs the game menu.
        /// </summary>
        private static void RunGameMenu()
        {
            // Create a Data class object, that contains game companies and events.
            Data data = new Data();

            // Read the .xml file.
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));

                using (Stream stream = File.Open("Data\\Data.xml", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = (Data)serializer.Deserialize(reader);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error! In file Data.xml specified invalid value.");

                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! \nDetails: {ex.Message}");
            }

            // Create variables.
            int currentEvent;
            double money = 10000;
            bool isGameEnded = false;

            // Create a Random class object to generate event.
            Random random = new Random();

            // Start of the game cycle.
            while (!isGameEnded)
            {
                // Generate event for current turn.
                currentEvent = random.Next(0, data.gameEvents.Count);

                // Determine current event's type.
                if (data.gameEvents[currentEvent].Type == "Positive")  // If current event is positive.
                {
                    foreach (var currentCompany in data.gameCompanies)
                    {
                        if (currentCompany.Ticker == data.gameEvents[currentEvent].Target)
                        {
                            currentCompany.SharePrice = Math.Round(currentCompany.SharePrice + currentCompany.SharePrice * data.gameEvents[currentEvent].Effect / 100, 2);
                        }
                    }
                }
                else if (data.gameEvents[currentEvent].Type == "Negative")  // If current event is negative.
                {
                    foreach (var currentCompany in data.gameCompanies)
                    {
                        if (currentCompany.Ticker == data.gameEvents[currentEvent].Target)
                        {
                            currentCompany.SharePrice = Math.Round(currentCompany.SharePrice - currentCompany.SharePrice * data.gameEvents[currentEvent].Effect / 100, 2);
                        }
                    }
                }

                string prompt = "\nUse up and down arrow keys to select an option: \n";
                string[] options = { "Skip", "Buy", "Sell", "More About Companies" };
                GameMenu gameMenu = new GameMenu(prompt, options, 0, currentEvent, money, data);

                int selectedOption = gameMenu.RunMenu();

                switch (selectedOption)
                {
                    case 0:
                        break;
                    case 1:
                        RunActionMenu(ref money, currentEvent, data, true);
                        break;
                    case 2:
                        RunActionMenu(ref money, currentEvent, data, false);
                        break;
                    case 3:
                        DisplayMoreAboutCompaniesMenu(data);
                        gameMenu.RunMenu();
                        break;
                }

                // Check for win or loss.
                if (money <= 0)
                {
                    isGameEnded = true;
                    RunEndMenu(false);
                }
                else if (money >= 50000)
                {
                    isGameEnded = true;
                    RunEndMenu(true);
                }
                else
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Runs action menu, that is buying or selling menu.
        /// </summary>
        /// <param name="money">The amount of money that user currently has.</param>
        /// <param name="currentEvent">The index of current generated event.</param>
        /// <param name="data">An Data class object, that contain information about companies and events.</param>
        /// <param name="isBuying">Flag that determines the mode of the method. True - buying, false - selling.</param>
        private static void RunActionMenu(ref double money, int currentEvent, Data data, bool isBuying)
        {
            string actionPrompt = "Select a company: \n";
            string amountPrompt = "Select an amount: \n";
            string[] actionOptions = new string[data.gameCompanies.Count];
            string[] amountOptions = { "Increase (+)", "Decrease (-)", "Enter" };

            for (int i = 0; i < actionOptions.Length; i++)
            {
                actionOptions[i] = data.gameCompanies[i].Name;
            }

            GameMenu actionMenu = new GameMenu(actionPrompt, actionOptions, 0, currentEvent, money, data);
            GameMenu amountMenu = new GameMenu(amountPrompt, amountOptions, 0, currentEvent, money, data);

            int selectedCompany = actionMenu.RunMenu();
            int amountOfShares = amountMenu.RunAmountMenu();

            if (isBuying)
            {
                data.gameCompanies[selectedCompany].ShareAmount += amountOfShares;
                money -= amountOfShares * data.gameCompanies[selectedCompany].SharePrice;

                Console.WriteLine($"You have bought {amountOfShares} shares of {data.gameCompanies[selectedCompany].Name} company.");
                Thread.Sleep(2500);
            }
            else
            {
                if (amountOfShares <= data.gameCompanies[selectedCompany].ShareAmount)
                {
                    data.gameCompanies[selectedCompany].ShareAmount -= amountOfShares;
                    money += amountOfShares * data.gameCompanies[selectedCompany].SharePrice;

                    Console.WriteLine($"You have sold {amountOfShares} shares of {data.gameCompanies[selectedCompany].Name} company.");
                    Thread.Sleep(2500);
                }
                else
                {
                    Console.WriteLine("Entered not a valid value");
                    Thread.Sleep(2500);
                }
            }
        }

        /// <summary>
        /// Displays companies descriptions to the console.
        /// </summary>
        /// <param name="data">An Data class object, that contain information about companies and events.</param>
        private static void DisplayMoreAboutCompaniesMenu(Data data)
        {
            Console.Clear();

            foreach (var company in data.gameCompanies)
            {
                Console.WriteLine($"{company.Name} - {company.Description}\n");
            }

            Console.WriteLine("Press any key to exit the menu...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Runs end menu.
        /// </summary>
        /// <param name="isWinner">Flag that determines the mode of the method. True - for a winner, false - for a loser.</param>
        private static void RunEndMenu(bool isWinner)
        {
            Console.Clear();

            if (isWinner)
            {
                string prompt = @"Dear CEO,
    On behalf of the board of directors of Oligopoly Investments, we would like to express our gratitude and understanding for your decision to leave your post. You have been a remarkable leader and a visionary strategist, who played the stock market skillfully and increased our budget by five times. We are proud of your achievements and we wish you all the best in your future endeavors.
    As a token of our appreciation, we are pleased to inform you that the company will pay you a bonus of $1 million. You deserve this reward for your hard work and dedication. We hope you will enjoy it and remember us fondly.
    Thank you for your service and your contribution to Oligopoly Investments. You will be missed.

Sincerely,
The board of directors of Oligopoly Investments

";
                string[] options = { "Play Again", "Return to Main Menu" };

                Menu EndMenu = new Menu(prompt, options, 15);

                int selectedOption = EndMenu.RunMenu();

                switch (selectedOption)
                {
                    case 0:
                        RunGameMenu();
                        break;
                    case 1:
                        RunMainMenu();
                        break;
                }
            }
            else
            {
                string prompt = @"Dear, former CEO
    We regret to inform you that you are being removed from the position of CEO and fired from the company, effective immediately.
    The board of directors of Oligopoly Investments has decided to take this action because you have spent the budget allocated to you, and your investment turned out to be unprofitable for the company.
    We appreciate your service and wish you all the best in your future endeavors.

Sincerely,
The board of directors of Oligopoly Investments

";
                string[] options = { "Play Again", "Return to Main Menu" };

                Menu EndMenu = new Menu(prompt, options, 15);

                int selectedOption = EndMenu.RunMenu();

                switch (selectedOption)
                {
                    case 0:
                        RunGameMenu();
                        break;
                    case 1:
                        RunMainMenu();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays information about the game.
        /// </summary>
        private static void DisplayAboutInfo()
        {
            Console.Clear();
            Console.WriteLine(@"THANKS!
No really, thank you for taking time to play this simple console game. It means a lot.

This game was created by Semion Medvedev (Fuinny)
My GitHub profile: https://github.com/Fuinny

Press any key to exit the menu...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        private static void ExitGame()
        {
            Console.Clear();
            Console.WriteLine("Press any key to exit the game...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}