namespace Oligopoly.Source
{
    public class Menu
    {
        // Create a class fields.
        protected int SelectedIndex;
        protected int OutputDelay;
        protected string Prompt;
        protected string[] Options;

        /// <summary>
        /// Initializes a new instance of the Menu class with given prompt, options and output delay.
        /// </summary>
        /// <param name="prompt">The prompt to display above the menu.</param>
        /// <param name="options">The options to display in the menu.</param>
        /// <param name="outputDelay">The text output delay. Have to be a positive integer or zero!</param>
        public Menu(string prompt, string[] options, int outputDelay)
        {
            Prompt = prompt;
            Options = options;
            OutputDelay = outputDelay;
            SelectedIndex = 0;
        }

        /// <summary>
        /// Displays menu to the console and redraws it when user select other option.
        /// </summary>
        protected virtual void DisplayMenu()
        {
            // Display the prompt above the menu.
            foreach (char symbol in Prompt)
            {
                Thread.Sleep(OutputDelay);
                Console.Write(symbol);
            }

            // Display all options inside the menu and redraw it when user select other option.
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "*";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"[{prefix}] {currentOption}");
            }

            // Reset console colors.
            Console.ResetColor();
        }

        /// <summary>
        /// Runs the menu.
        /// </summary>
        /// <returns>An integer that represents the selected option.</returns>
        public virtual int RunMenu()
        {
            // Set output delay to 0.
            // This is necessary so that the menu does not draw with a delay when updating the console.
            OutputDelay = 0;

            // Create variable, that contains key that was pressed.
            ConsoleKey keyPressed;

            do
            {
                // Redraw the menu.
                Console.Clear();
                DisplayMenu();

                //Read the user's input.
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                // Get the key that was pressed.
                keyPressed = keyInfo.Key;

                // Move the selection up or down, based on the pressed key.
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;

                    // Wrap around if user is out of range.
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;

                    // Wrap around if user is out of range.
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            // Return the selected option.
            return SelectedIndex;
        }
    }
}