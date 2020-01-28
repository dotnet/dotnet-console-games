using System;

class Program
{
	enum Tetromino
	{
		I = 1,
		O = 2,
		T = 3,
		S = 4,
		Z = 5,
		J = 6,
		L = 7,
	}

	static void Main()
	{
		Console.WriteLine("Still in development...");

#if false
        int originalWidth = Console.WindowWidth;
        int originalHeight = Console.WindowHeight;
        ConsoleColor originalColor = Console.ForegroundColor;
        bool originalCursorVisible = Console.CursorVisible;

        Console.CursorVisible = false;

        var random = new Random();
        var width = 10;
        var height = 16;
        var middle = width / 2;
        var Map = new Tetromino[width, height];
        var current = (Tetromino)random.Next(1, 8);
        var next = (Tetromino)random.Next(1, 8);
        var position = (middle, 0);

        Draw();

        void Draw()
        {
            Console.Clear();

            // Map
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (Map[i, j] != 0)
                    {
                        Console.ForegroundColor = Map[i, j] switch
                        {
                            Tetromino.I => ConsoleColor.Cyan,
                            Tetromino.O => ConsoleColor.Yellow,
                            Tetromino.T => ConsoleColor.Magenta,
                            Tetromino.S => ConsoleColor.Red,
                            Tetromino.Z => ConsoleColor.DarkRed,
                            Tetromino.J => ConsoleColor.Blue,
                            Tetromino.L => ConsoleColor.DarkBlue,
                            _ => originalColor,
                        };
                        Console.SetCursorPosition(i + 2, j + 4);
                        Console.Write('█');
                    }
                }
            }

            // Outline
            for (int i = 0; i < height + 1; i++)
            {
                Console.ForegroundColor = originalColor;
                Console.SetCursorPosition(1, i + 4);
                Console.Write('█');
            }
            for (int i = 0; i < width; i++)
            {
                Console.ForegroundColor = originalColor;
                Console.SetCursorPosition(i + 2, height + 4);
                Console.Write('█');
            }
            for (int i = 0; i < height + 1; i++)
            {
                Console.ForegroundColor = originalColor;
                Console.SetCursorPosition(width + 2, i + 4);
                Console.Write('█');
            }

            Console.ForegroundColor = originalColor;
            Console.ReadLine();
        }

        Console.CursorVisible = originalCursorVisible;
        Console.ForegroundColor = originalColor;
        Console.WindowWidth = originalWidth;
        Console.WindowHeight = originalHeight;
#endif
	}
}
