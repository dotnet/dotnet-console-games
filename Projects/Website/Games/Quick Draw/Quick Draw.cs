using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Website.Games.Quick_Draw;

public class Quick_Draw
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Exception? exception = null;

		Random random = new();

		const string menu = @"
  Quick Draw

  Face your opponent and wait for the signal. Once the
  signal is given, shoot your opponent by pressing [space]
  before they shoot you. It's all about your reaction time.

  Choose Your Opponent:
  [1] Easy....1000 milliseconds
  [2] Medium...500 milliseconds
  [3] Hard.....250 milliseconds
  [4] Harder...125 milliseconds
  [escape] give up";

		const string wait = @"
  Quick Draw



              _O                          O_            
             |/|_          wait          _|\|           
             /\                            /\           
            /  |                          |  \          
  ------------------------------------------------------";

		const string fire = @"
  Quick Draw

                         ********                       
                         * FIRE *                       
              _O         ********         O_            
             |/|_                        _|\|           
             /\          spacebar          /\           
            /  |                          |  \          
  ------------------------------------------------------";

		const string loseTooSlow = @"
  Quick Draw



                                        > ╗__O          
           //            Too Slow           / \         
          O/__/\         You Lose          /\           
               \                          |  \          
  ------------------------------------------------------";

		const string loseTooFast = @"
  Quick Draw



                         Too Fast       > ╗__O          
           //           You Missed          / \         
          O/__/\         You Lose          /\           
               \                          |  \          
  ------------------------------------------------------";

		const string win = @"
  Quick Draw



            O__╔ <                                      
           / \                               \\         
             /\          You Win          /\__\O        
            /  |                          /             
  ------------------------------------------------------";


		try
		{
			while (true)
			{
				await Console.Clear();
				await Console.WriteLine(menu);
				TimeSpan? requiredReactionTime = null;
				while (requiredReactionTime is null)
				{
					Console.CursorVisible = false;
					switch ((await Console.ReadKey(true)).Key)
					{
						case ConsoleKey.D1 or ConsoleKey.NumPad1: requiredReactionTime = TimeSpan.FromMilliseconds(1000); break;
						case ConsoleKey.D2 or ConsoleKey.NumPad2: requiredReactionTime = TimeSpan.FromMilliseconds(0500); break;
						case ConsoleKey.D3 or ConsoleKey.NumPad3: requiredReactionTime = TimeSpan.FromMilliseconds(0250); break;
						case ConsoleKey.D4 or ConsoleKey.NumPad4: requiredReactionTime = TimeSpan.FromMilliseconds(0125); break;
						case ConsoleKey.Escape: return;
					}
				}
				await Console.Clear();
				TimeSpan signal = TimeSpan.FromMilliseconds(random.Next(5000, 35000));
				await Console.WriteLine(wait);
				Stopwatch stopwatch = new();
				stopwatch.Restart();
				bool tooFast = false;
				while (stopwatch.Elapsed < signal && !tooFast)
				{
					if (await Console.KeyAvailable() && (await Console.ReadKey(true)).Key is ConsoleKey.Spacebar)
					{
						tooFast = true;
					}
				}
				await Console.Clear();
				Console.CursorVisible = false;
				await Console.WriteLine(fire);
				stopwatch.Restart();
				bool tooSlow = true;
				TimeSpan reactionTime = default;
				while (!tooFast && stopwatch.Elapsed < requiredReactionTime && tooSlow)
				{
					if (await Console.KeyAvailable() && (await Console.ReadKey(true)).Key is ConsoleKey.Spacebar)
					{
						tooSlow = false;
						reactionTime = stopwatch.Elapsed;
					}
				}
				await Console.Clear();
				await Console.WriteLine(
					tooFast ? loseTooFast :
					tooSlow ? loseTooSlow :
					$"{win}{Environment.NewLine}  Reaction Time: {reactionTime.TotalMilliseconds} milliseconds");
				await Console.WriteLine("  Play Again [enter] or quit [escape]?");
				Console.CursorVisible = false;
			GetEnterOrEscape:
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter: break;
					case ConsoleKey.Escape: return;
					default: goto GetEnterOrEscape;
				}
			}
		}
		catch (Exception e)
		{
			exception = e;
			throw;
		}
		finally
		{
			await Console.Clear();
			Console.CursorVisible = true;
			await Console.WriteLine(exception?.ToString() ?? "Quick Draw was closed.");
			await Console.Refresh();
		}
	}
}
