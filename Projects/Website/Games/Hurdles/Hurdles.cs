using System;
using System.Threading.Tasks;

namespace Website.Games.Hurdles;

public class Hurdles
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		string[] runningAnimation =
		[
			#region Frames
			// 0
			@"       " + '\n' +
			@"       " + '\n' +
			@"  __O  " + '\n' +
			@" / /\_," + '\n' +
			@"__/\   " + '\n' +
			@"    \  ",
			// 1
			@"       " + '\n' +
			@"       " + '\n' +
			@"   _O  " + '\n' +
			@"  |/|_ " + '\n' +
			@"  /\   " + '\n' +
			@" /  |  ",
			// 2
			@"       " + '\n' +
			@"       " + '\n' +
			@"    O  " + '\n' +
			@"  </L  " + '\n' +
			@"   \   " + '\n' +
			@"   /|  ",
			// 3
			@"       " + '\n' +
			@"       " + '\n' +
			@"   O   " + '\n' +
			@"   |_  " + '\n' +
			@"   |>  " + '\n' +
			@"  /|   ",
			// 4
			@"       " + '\n' +
			@"       " + '\n' +
			@"   O   " + '\n' +
			@"  <|L  " + '\n' +
			@"   |_  " + '\n' +
			@"   |/  ",
			// 5
			@"       " + '\n' +
			@"       " + '\n' +
			@"   O   " + '\n' +
			@"  L|L  " + '\n' +
			@"   |_  " + '\n' +
			@"  /  | ",
			// 6
			@"       " + '\n' +
			@"       " + '\n' +
			@"  _O   " + '\n' +
			@" | |L  " + '\n' +
			@"   /-- " + '\n' +
			@"  /   |",
			#endregion
		];

		string[] jumpingAnimation =
		[
			#region Frames
			// 0
			@"       " + '\n' +
			@"       " + '\n' +
			@"   _O  " + '\n' +
			@"  |/|_ " + '\n' +
			@"  /\   " + '\n' +
			@" /  |  ",
			// 1
			@"       " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"    O  " + '\n' +
			@"  </L  " + '\n' +
			@"   /|  ",
			// 2
			@"       " + '\n' +
			@"    /O/" + '\n' +
			@"    /  " + '\n' +
			@"   //  " + '\n' +
			@"  //   " + '\n' +
			@"       ",
			// 3
			@"  __O__" + '\n' +
			@" /     " + '\n' +
			@"//     " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       ",
			// 4
			@"  __   " + '\n' +
			@" // \O " + '\n' +
			@"     \\" + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       ",
			// 5
			@"  __   " + '\n' +
			@" //_O\ " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       ",
			// 6
			@"  __\  " + '\n' +
			@" _O/   " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       ",
			// 7
			@" \O\__ " + '\n' +
			@"     \\" + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       ",
			// 8
			@"       " + '\n' +
			@"       " + '\n' +
			@"   O   " + '\n' +
			@"  L|L  " + '\n' +
			@"   |_  " + '\n' +
			@"  /  | ",
			#endregion
		];

		string hurdleFrame =
			#region Frame
			@"  ___  " + '\n' +
			@" |   | " + '\n' +
			@" | . | ";
			#endregion

		int position = 0;
		int? runningFrame = 0;
		int? jumpingFrame = null;

		Console.CursorVisible = false;
		if (OperatingSystem.IsWindows())
		{
			Console.WindowWidth = 120;
			Console.WindowHeight = 20;
		}
		await Console.Clear();
		while (position < int.MaxValue)
		{
			if (await Console.KeyAvailable())
			{
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Escape:
						await Console.Clear();
						await Console.Write("Hurdles was closed.");
						await Console.Refresh();
						return;
					case ConsoleKey.UpArrow:
						if (!jumpingFrame.HasValue)
						{
							jumpingFrame = 0;
							runningFrame = null;
						}
						break;
				}
			}
			if (position >= 100 &&
				position % 50 is 0 &&
				(!jumpingFrame.HasValue ||
				!(2 <= jumpingFrame && jumpingFrame <= 7)))
			{
				await Console.Clear();
				await Console.Write("Game Over. Score " + position + ".");
				await Console.Refresh();
				return;
			}
			string playerFrame =
				jumpingFrame.HasValue ? jumpingAnimation[jumpingFrame.Value] :
				runningAnimation[runningFrame!.Value];
			await Console.SetCursorPosition(4, 10);
			await Render(playerFrame, true);
			await RenderHurdles(true);
			if (position % 50 is 5)
			{
				await Console.SetCursorPosition(0, 13);
				await Render(
					@"       " + '\n' +
					@"       " + '\n' +
					@"       ", true);
			}
			if (position % 50 < 3)
			{
				await Console.SetCursorPosition(4, 10);
				await Render(playerFrame, false);
				await RenderHurdles(false);
			}
			else
			{
				await RenderHurdles(false);
				await Console.SetCursorPosition(4, 10);
				await Render(playerFrame, false);
			}
			runningFrame = runningFrame.HasValue
				? (runningFrame + 1) % runningAnimation.Length
				: runningFrame;
			jumpingFrame = jumpingFrame.HasValue
				? jumpingFrame + 1
				: jumpingFrame;
			if (jumpingFrame >= jumpingAnimation.Length)
			{
				jumpingFrame = null;
				runningFrame = 2;
			}
			position++;
			await Console.RefreshAndDelay(TimeSpan.FromMilliseconds(80));
		}
		await Console.Clear();
		await Console.Write("You Win.");
		await Console.Refresh();

		async Task Render(string @string, bool renderSpace)
		{
			int x = Console.CursorLeft;
			int y = Console.CursorTop;
			foreach (char c in @string)
				if (c is '\n') await Console.SetCursorPosition(x, ++y);
				else if (c is not ' ' || renderSpace) await Console.Write(c);
				else await Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
		}

		async Task RenderHurdles(bool renderSpace)
		{
			for (int i = 5; i < Console.WindowWidth - 5; i++)
			{
				if (position + i >= 100 && (position + i - 7) % 50 is 0)
				{
					await Console.SetCursorPosition(i - 3, 13);
					await Render(hurdleFrame, renderSpace);
				}
			}
		}
	}
}
