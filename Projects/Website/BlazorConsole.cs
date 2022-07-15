﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Towel;

namespace Website;

public class BlazorConsole
{
	public struct Pixel
	{
		public char Char;
		public ConsoleColor BackgroundColor;
		public ConsoleColor ForegroundColor;

		public static bool operator ==(Pixel a, Pixel b) =>
			a.Char == b.Char &&
			a.ForegroundColor == b.ForegroundColor &&
			a.BackgroundColor == b.BackgroundColor;

		public static bool operator !=(Pixel a, Pixel b) => !(a == b);

		public override bool Equals(object? obj) => obj is Pixel pixel && this == pixel;

		public override int GetHashCode() => HashCode.Combine(Char, ForegroundColor, BackgroundColor);
	}

#pragma warning disable CA2211 // Non-constant fields should not be visible
	public static BlazorConsole? ActiveConsole;
#pragma warning restore CA2211 // Non-constant fields should not be visible

	public const int Delay = 1; // miliseconds
	public const int InactiveDelay = 1000; // milliseconds
	public readonly Queue<ConsoleKeyInfo> InputBuffer = new();
	public Action? TriggerRefresh;
	public bool RefreshOnInputOnly = true;
	public Pixel[,] View;
	public bool StateHasChanged = true;

	public string? Title;
	public ConsoleColor BackgroundColor = ConsoleColor.Black;
	public ConsoleColor ForegroundColor = ConsoleColor.White;
	public bool CursorVisible = true;
	public int LargestWindowWidth = 120;
	public int LargestWindowHeight = 51;
	public int CursorLeft = 0;
	public int CursorTop = 0;

	public int _windowHeight = 35;
	public int _windowWidth = 80;

	public Encoding? OutputEncoding;

	public int WindowHeight
	{
		get =>  _windowHeight;
		set
		{
			_windowHeight = value;
			HandleResize();
		}
	}

	public int WindowWidth
	{
		get => _windowWidth;
		set
		{
			_windowWidth = value;
			HandleResize();
		}
	}

	public int BufferWidth
	{
		get => WindowWidth;
		set => WindowWidth = value;
	}

	public int BufferHeight
	{
		get => WindowHeight;
		set => WindowHeight = value;
	}

#pragma warning disable CA1822 // Mark members as static
#pragma warning disable IDE0060 // Remove unused parameter

	public void SetWindowPosition(int left, int top)
	{
		// do nothing :)
	}

#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore CA1822 // Mark members as static

	public void SetWindowSize(int width, int height)
	{
		WindowWidth = width;
		WindowHeight = height;
	}

	public void SetBufferSize(int width, int height) => SetWindowSize(width, height);

	public void EnqueueInput(ConsoleKey key, bool shift = false, bool alt = false, bool control = false)
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

	public void OnKeyDown(KeyboardEventArgs e)
	{
		switch (e.Key)
		{
			case "Home":       EnqueueInput(ConsoleKey.Home); break;
			case "End":        EnqueueInput(ConsoleKey.End); break;
			case "Backspace":  EnqueueInput(ConsoleKey.Backspace); break;
			case " ":          EnqueueInput(ConsoleKey.Spacebar); break;
			case "Delete":     EnqueueInput(ConsoleKey.Delete); break;
			case "Enter":      EnqueueInput(ConsoleKey.Enter); break;
			case "Escape":     EnqueueInput(ConsoleKey.Escape); break;
			case "ArrowLeft":  EnqueueInput(ConsoleKey.LeftArrow); break;
			case "ArrowRight": EnqueueInput(ConsoleKey.RightArrow); break;
			case "ArrowUp":    EnqueueInput(ConsoleKey.UpArrow); break;
			case "ArrowDown":  EnqueueInput(ConsoleKey.DownArrow); break;
			case ".":          EnqueueInput(ConsoleKey.OemPeriod); break;
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

	public void ResetColor()
	{
		BackgroundColor = ConsoleColor.Black;
		ForegroundColor = ConsoleColor.White;
	}

	public BlazorConsole()
	{
		ActiveConsole = this;
		View = new Pixel[WindowHeight, WindowWidth];
		ClearNoRefresh();
	}

	public void DieIfNotActiveGame()
	{
		if (this != ActiveConsole)
		{
			throw new Exception("die :P pew pew");
		}
	}

	public async Task RefreshAndDelay(TimeSpan timeSpan)
	{
		DieIfNotActiveGame();
		if (StateHasChanged)
		{
			TriggerRefresh?.Invoke();
		}
		await Task.Delay(timeSpan);
	}

	public void HandleResize()
	{
		if (View.GetLength(0) != WindowHeight || View.GetLength(1) != WindowWidth)
		{
			Pixel[,] old_view = View;
			View = new Pixel[WindowHeight, WindowWidth];
			for (int row = 0; row < View.GetLength(0) && row < old_view.GetLength(0); row++)
			{
				for (int column = 0; column < View.GetLength(1) && column < old_view.GetLength(1); column++)
				{
					View[row, column] = old_view[row, column];
				}
			}
			StateHasChanged = true;
		}
	}

	public async Task Refresh()
	{
		DieIfNotActiveGame();
		if (StateHasChanged)
		{
			TriggerRefresh?.Invoke();
		}
		await Task.Delay(Delay);
	}

	public MarkupString State
	{
		get
		{
			StringBuilder stateBuilder = new();
			for (int row = 0; row < View.GetLength(0); row++)
			{
				for (int column = 0; column < View.GetLength(1); column++)
				{
					if (CursorVisible && (CursorLeft, CursorTop) == (column, row))
					{
						bool isDark =
							(View[row, column].Char is '█' && View[row, column].ForegroundColor is ConsoleColor.White) ||
							(View[row, column].Char is ' ' && View[row, column].BackgroundColor is ConsoleColor.White);
						stateBuilder.Append($@"<span class=""cursor {(isDark ? "cursor-dark" : "cursor-light")}"">");
					}
					if (View[row, column].BackgroundColor is not ConsoleColor.Black)
					{
						stateBuilder.Append($@"<span style=""background-color:{HtmlEncode(View[row, column].BackgroundColor)}"">");
					}
					if (View[row, column].ForegroundColor is not ConsoleColor.White)
					{
						stateBuilder.Append($@"<span style=""color:{HtmlEncode(View[row, column].ForegroundColor)}"">");
					}
					stateBuilder.Append(HttpUtility.HtmlEncode(View[row, column].Char));
					if (View[row, column].ForegroundColor is not ConsoleColor.White)
					{
						stateBuilder.Append("</span>");
					}
					if (View[row, column].BackgroundColor is not ConsoleColor.Black)
					{
						stateBuilder.Append("</span>");
					}
					if (CursorVisible && (CursorLeft, CursorTop) == (column, row))
					{
						stateBuilder.Append("</span>");
					}
				}
				stateBuilder.Append("<br />");
			}
			string state = stateBuilder.ToString();
			StateHasChanged = false;
			return (MarkupString)state;
		}
	}

	public void ResetColors()
	{
		DieIfNotActiveGame();
		BackgroundColor = ConsoleColor.Black;
		ForegroundColor = ConsoleColor.White;
	}

	public async Task Clear()
	{
		DieIfNotActiveGame();
		ClearNoRefresh();
		if (!RefreshOnInputOnly)
		{
			await Refresh();
		}
	}

	public void ClearNoRefresh()
	{
		DieIfNotActiveGame();
		for (int row = 0; row < View.GetLength(0); row++)
		{
			for (int column = 0; column < View.GetLength(1); column++)
			{
				Pixel pixel = new()
				{
					Char = ' ',
					BackgroundColor = BackgroundColor,
					ForegroundColor = ForegroundColor,
				};
				StateHasChanged = StateHasChanged || pixel != View[row, column];
				View[row, column] = pixel;
			}
		}
		(CursorLeft, CursorTop) = (0, 0);
	}

	public void WriteNoRefresh(char c)
	{
		DieIfNotActiveGame();
		if (c is '\r')
		{
			return;
		}
		if (c is '\n')
		{
			WriteLineNoRefresh();
			return;
		}
		if (CursorLeft >= View.GetLength(1))
		{
			(CursorLeft, CursorTop) = (0, CursorTop + 1);
		}
		if (CursorTop >= View.GetLength(0))
		{
			for (int row = 0; row < View.GetLength(0) - 1; row++)
			{
				for (int column = 0; column < View.GetLength(1); column++)
{
					StateHasChanged = StateHasChanged || View[row, column] != View[row + 1, column];
					View[row, column] = View[row + 1, column];
				}
			}
			for (int column = 0; column < View.GetLength(1); column++)
			{
				Pixel pixel = new()
				{
					Char = ' ',
					BackgroundColor = BackgroundColor,
					ForegroundColor = ForegroundColor
				};
				StateHasChanged = StateHasChanged || View[View.GetLength(0) - 1, column] != pixel;
				View[View.GetLength(0) - 1, column] = pixel;
			}
			CursorTop--;
		}
		{
			Pixel pixel = new()
			{
				Char = c,
				BackgroundColor = BackgroundColor,
				ForegroundColor = ForegroundColor
			};
			StateHasChanged = StateHasChanged || View[CursorTop, CursorLeft] != pixel;
			View[CursorTop, CursorLeft] = pixel;
		}
		CursorLeft++;
	}

	public void WriteLineNoRefresh()
	{
		DieIfNotActiveGame();
		while (CursorLeft < View.GetLength(1))
		{
			WriteNoRefresh(' ');
		}
		(CursorLeft, CursorTop) = (0, CursorTop + 1);
	}

	public async Task Write(object o)
	{
		DieIfNotActiveGame();
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

	public async Task WriteLine()
	{
		WriteLineNoRefresh();
		await Refresh();
	}

	public async Task WriteLine(object o)
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

	public ConsoleKeyInfo ReadKeyNoRefresh(bool capture)
	{
		if (!KeyAvailableNoRefresh())
		{
			throw new InvalidOperationException("attempting a no refresh ReadKey with an empty input buffer");
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

	public async Task<ConsoleKeyInfo> ReadKey(bool capture)
	{
		while (!KeyAvailableNoRefresh())
		{
			await Refresh();
		}
		return ReadKeyNoRefresh(capture);
	}

	public async Task<string> ReadLine()
	{
		string line = string.Empty;
		while (true)
		{
			while (!KeyAvailableNoRefresh())
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
							StateHasChanged = true;
							View[CursorTop, CursorLeft].Char = ' ';
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

	public bool KeyAvailableNoRefresh()
	{
		return InputBuffer.Count > 0;
	}

	public async Task<bool> KeyAvailable()
	{
		await Refresh();
		return KeyAvailableNoRefresh();
	}

	public async Task SetCursorPosition(int left, int top)
	{
		(CursorLeft, CursorTop) = (left, top);
		if (!RefreshOnInputOnly)
		{
			await Refresh();
		}
	}

	public async Task PromptPressToContinue(string? prompt = null, ConsoleKey key = ConsoleKey.Enter)
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

	public async Task PressToContinue(ConsoleKey key = ConsoleKey.Enter)
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

#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// Returns true. Some members of <see cref="Console"/> only work
	/// on Windows such as <see cref="Console.WindowWidth"/>, but even though this
	/// is blazor and not necessarily on Windows, this wrapper contains implementations
	/// for those Windows-only members.
	/// </summary>
	/// <returns>true</returns>
	public bool IsWindows() => true;
#pragma warning restore CA1822 // Mark members as static
}