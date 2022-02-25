﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Towel;

namespace Website;

public static class Console<TGame>
{
	internal const int Delay = 1;
	internal static readonly Queue<ConsoleKeyInfo> InputBuffer = new();
	internal static Action? StateHasChanged;
	internal static bool RefreshOnInputOnly = true;
	internal static bool Initialized = false;
	internal static (char Char, ConsoleColor BackgroundColor, ConsoleColor ForegroundColor)[,] _view;
	
	internal static ConsoleColor BackgroundColor = ConsoleColor.Black;
	internal static ConsoleColor ForegroundColor = ConsoleColor.White;
	internal static bool CursorVisible = true;
	internal static int WindowHeight = 35;
	internal static int WindowWidth = 80;
	public static int LargestWindowWidth = 120;
	public static int LargestWindowHeight = 40;
	public static int CursorLeft = 0;
	public static int CursorTop = 0;

	public static int BufferWidth
	{
		get => WindowWidth;
		set => WindowWidth = value;
	}

	public static int BufferHeight
	{
		get => WindowHeight;
		set => WindowHeight = value;
	}

	public static void EnqueueInput(ConsoleKey key, bool shift = false, bool alt = false, bool control = false)
	{
		char c = key switch
		{
			>= ConsoleKey.A and <= ConsoleKey.Z => (char)(key - ConsoleKey.A + 'a'),
			>= ConsoleKey.D0 and <= ConsoleKey.D9 => (char)(key - ConsoleKey.D0 + '0'),
			ConsoleKey.Enter => '\n',
			ConsoleKey.Backspace => '\b',
			ConsoleKey.OemPeriod => '.',
			_ => '\0',
		};
		InputBuffer.Enqueue(new(shift ? char.ToUpper(c) : c, key, shift, alt, control));
	}

	public static void OnKeyDown(KeyboardEventArgs e)
	{
		switch (e.Key)
		{
			case "End":        EnqueueInput(ConsoleKey.End);        break;
			case "Backspace":  EnqueueInput(ConsoleKey.Backspace);  break;
			case " ":          EnqueueInput(ConsoleKey.Spacebar);   break;
			case "Delete":     EnqueueInput(ConsoleKey.Delete);     break;
			case "Enter":      EnqueueInput(ConsoleKey.Enter);      break;
			case "Escape":     EnqueueInput(ConsoleKey.Escape);     break;
			case "ArrowLeft":  EnqueueInput(ConsoleKey.LeftArrow);  break;
			case "ArrowRight": EnqueueInput(ConsoleKey.RightArrow); break;
			case "ArrowUp":    EnqueueInput(ConsoleKey.UpArrow);    break;
			case "ArrowDown":  EnqueueInput(ConsoleKey.DownArrow);  break;
			case ".":          EnqueueInput(ConsoleKey.OemPeriod);  break;
			default:
				if (e.Key.Length is 1)
				{
					char c = e.Key[0];
					switch (c)
					{
						case >= '0' and <= '9': EnqueueInput(ConsoleKey.D0 + (c - '0'));              break;
						case >= 'a' and <= 'z': EnqueueInput(ConsoleKey.A  + (c - 'a'));              break;
						case >= 'A' and <= 'Z': EnqueueInput(ConsoleKey.A  + (c - 'A'), shift: true); break;
					}
				}
				break;
		}
	}

	public static string HtmlEncode(ConsoleColor color)
	{
		return color switch
		{
			ConsoleColor.Black =>       "#000000",
			ConsoleColor.White =>       "#ffffff",
			ConsoleColor.Blue =>        "#0000ff",
			ConsoleColor.Red =>         "#ff0000",
			ConsoleColor.Green =>       "#00ff00",
			ConsoleColor.Yellow =>      "#ffff00",
			ConsoleColor.Cyan =>        "#00ffff",
			ConsoleColor.Magenta =>     "#ff00ff",
			ConsoleColor.Gray =>        "#808080",
			ConsoleColor.DarkBlue =>    "#00008b",
			ConsoleColor.DarkRed =>     "#8b0000",
			ConsoleColor.DarkGreen =>   "#006400",
			ConsoleColor.DarkYellow =>  "#8b8000",
			ConsoleColor.DarkCyan =>    "#008b8b",
			ConsoleColor.DarkMagenta => "#8b008b",
			ConsoleColor.DarkGray =>    "#a9a9a9",
			_ => throw new NotImplementedException(),
		};
	}

	public static void ResetColor()
	{
		BackgroundColor = ConsoleColor.Black;
		ForegroundColor = ConsoleColor.White;
	}

	static Console()
	{
		_view = new (char, ConsoleColor, ConsoleColor)[WindowHeight, WindowWidth];
		ClearNoRefresh();
	}

	public static async Task RefreshAndDelay(TimeSpan timeSpan)
	{
		StateHasChanged?.Invoke();
		await Task.Delay(timeSpan);
	}

	public static async Task Refresh()
	{
		StateHasChanged?.Invoke();
		await Task.Delay(Delay);
	}

	public static MarkupString State
	{
		get
		{
			if (_view.GetLength(0) != WindowHeight || _view.GetLength(1) != WindowWidth)
			{
				(char, ConsoleColor, ConsoleColor)[,] old_view = _view;
				_view = new (char, ConsoleColor, ConsoleColor)[WindowHeight, WindowWidth];
				for (int row = 0; row < _view.GetLength(0); row++)
				{
					for (int column = 0; column < _view.GetLength(1); column++)
					{
						_view[row, column] = old_view[row, column];
					}
				}
			}
			StringBuilder stateBuilder = new();
			for (int row = 0; row < _view.GetLength(0); row++)
			{
				for (int column = 0; column < _view.GetLength(1); column++)
				{
					if (CursorVisible && (CursorLeft, CursorTop) == (column, row))
					{
						bool isDark =
							(_view[row, column].Char is '█' && _view[row, column].ForegroundColor is ConsoleColor.White) ||
							(_view[row, column].Char is ' ' && _view[row, column].BackgroundColor is ConsoleColor.White);
						stateBuilder.Append($@"<span class=""cursor {(isDark ? "cursor-dark" : "cursor-light")}"">");
					}
					if (_view[row, column].BackgroundColor is not ConsoleColor.Black)
					{
						stateBuilder.Append($@"<span style=""background-color:{HtmlEncode(_view[row, column].BackgroundColor)}"">");
					}
					if (_view[row, column].ForegroundColor is not ConsoleColor.White)
					{
						stateBuilder.Append($@"<span style=""color:{HtmlEncode(_view[row, column].ForegroundColor)}"">");
					}
					stateBuilder.Append(HttpUtility.HtmlEncode(_view[row, column].Char));
					if (_view[row, column].ForegroundColor is not ConsoleColor.White)
					{
						stateBuilder.Append(@"</span>");
					}
					if (_view[row, column].BackgroundColor is not ConsoleColor.Black)
					{
						stateBuilder.Append(@"</span>");
					}
					if (CursorVisible && (CursorLeft, CursorTop) == (column, row))
					{
						stateBuilder.Append(@"</span>");
					}
				}
				stateBuilder.Append("<br />");
			}
			string state = stateBuilder.ToString();
			return (MarkupString)state;
		}
	}

	public static void ResetColors()
	{
		BackgroundColor = ConsoleColor.Black;
		ForegroundColor = ConsoleColor.White;
	}

	public static async Task Clear()
	{
		ClearNoRefresh();
		if (!RefreshOnInputOnly)
		{
			await Refresh();
		}
	}

	public static void ClearNoRefresh()
	{
		for (int row = 0; row < _view.GetLength(0); row++)
		{
			for (int column = 0; column < _view.GetLength(1); column++)
			{
				_view[row, column] = (' ', BackgroundColor, ForegroundColor);
			}
		}
		(CursorLeft, CursorTop) = (0, 0);
	}

	public static void WriteNoRefresh(char c)
	{
		if (c is '\r')
		{
			return;
		}
		if (c is '\n')
		{
			WriteLineNoRefresh();
			return;
		}
		if (CursorLeft >= _view.GetLength(1))
		{
			(CursorLeft, CursorTop) = (0, CursorTop + 1);
		}
		_view[CursorTop, CursorLeft] = (c, BackgroundColor, ForegroundColor);
		CursorLeft++;
	}

	public static void WriteLineNoRefresh()
	{
		while (CursorLeft < _view.GetLength(1))
		{
			WriteNoRefresh(' ');
		}
		(CursorLeft, CursorTop) = (0, CursorTop + 1);
	}

	public static async Task Write(object o)
	{
		if (o is null) return;
		string? s = o.ToString();
		if (s is null || s is "") return;
		foreach (char c in s)
		{
			WriteNoRefresh(c);
		}
		if (!RefreshOnInputOnly)
		{
			await Refresh();
		}
	}

	public static async Task WriteLine()
	{
		WriteLineNoRefresh();
		await Refresh();
	}

	public static async Task WriteLine(object o)
	{
		if (o is not null)
		{
			string? s = o.ToString();
			if (s is not null)
			{
				foreach (char c in s)
				{
					WriteNoRefresh(c);
				}
			}
		}
		WriteLineNoRefresh();
		if (!RefreshOnInputOnly)
		{
			await Refresh();
		}
	}

	public static async Task<ConsoleKeyInfo> ReadKey(bool capture)
	{
		while (!await KeyAvailable())
		{
			await Refresh();
		}
		var keyInfo = InputBuffer.Dequeue();
		if (capture is false)
		{
			switch (keyInfo.KeyChar)
			{
				case '\n': WriteLineNoRefresh(); break;
				case '\0': break;
				case '\b': throw new NotImplementedException("ReadKey backspace not implemented");
				default: WriteNoRefresh(keyInfo.KeyChar); break;
			}
		}
		return keyInfo;
	}

	public static async Task<string> ReadLine()
	{
		string line = string.Empty;
		while (true)
		{
			while (!await KeyAvailable())
			{
				await Refresh();
			}
			var keyInfo = InputBuffer.Dequeue();
			switch (keyInfo.Key)
			{
				case ConsoleKey.Backspace:
					if (line.Length > 0)
					{
						if (CursorLeft > 0)
						{
							CursorLeft--;
							_view[CursorTop, CursorLeft].Char = ' ';
						}
						line = line[..^1];
						await Refresh();
					}
					break;
				case ConsoleKey.Enter:
					WriteLineNoRefresh();
					await Refresh();
					return line;
				default:
					if (keyInfo.KeyChar is not '\0')
					{
						line += keyInfo.KeyChar;
						WriteNoRefresh(keyInfo.KeyChar);
						await Refresh();
					}
					break;
			}
		}
	}

	public static async Task<bool> KeyAvailable()
	{
		await Refresh();
		return InputBuffer.Count > 0;
	}

	public static async Task SetCursorPosition(int left, int top)
	{
		(CursorLeft, CursorTop) = (left, top);
		if (!RefreshOnInputOnly)
		{
			await Refresh();
		}
	}

	public static async Task PromptPressToContinue(string? prompt = null, ConsoleKey key = ConsoleKey.Enter)
	{
		if (!key.IsDefined())
		{
			throw new ArgumentOutOfRangeException(nameof(key), key, $"{nameof(key)} is not a defined value in the {nameof(ConsoleKey)} enum");
		}
		prompt ??= $"Press [{key}] to continue...";
		foreach (char c in prompt)
		{
			WriteNoRefresh(c);
		}
		await PressToContinue(key);
	}

	public static async Task PressToContinue(ConsoleKey key = ConsoleKey.Enter)
	{
		if (!key.IsDefined())
		{
			throw new ArgumentOutOfRangeException(nameof(key), key, $"{nameof(key)} is not a defined value in the {nameof(ConsoleKey)} enum");
		}
		while ((await ReadKey(true)).Key != key)
		{
			continue;
		}
	}

	public static bool IsWindows()
	{
		return true;
	}
}