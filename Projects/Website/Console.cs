using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Website;

public static class Console<TGame>
{
	internal const int _delay = 1;
	internal static Action? _refresh;
	internal static bool _cursorVisible;
	internal static Queue<ConsoleKeyInfo> _inputBuffer = new();
	internal static ConsoleColor _backgroundColor = ConsoleColor.Black;
	internal static ConsoleColor _foregroundColor = ConsoleColor.White;
	internal static (int Left, int Top) _cursorPosition = (0, 0);
	internal static (char Char, ConsoleColor BackgroundColor, ConsoleColor ForegroundColor)[,] _view;
	internal static int _windowHeight = 30;
	internal static int _windowWidth = 120;
	internal static bool _writeLine = false;

	static Console()
	{
		_view = new (char, ConsoleColor, ConsoleColor)[WindowHeight, WindowWidth];
		ClearNoRefresh();
	}

	private static async Task Refresh()
	{
		_refresh?.Invoke();
		await Task.Delay(_delay);
	}

	public static MarkupString State
	{
		get
		{
			StringBuilder stateBuilder = new();
			for (int row = 0; row < _view.GetLength(0); row++)
			{
				for (int column = 0; column < _view.GetLength(1); column++)
				{
					stateBuilder.Append(_view[row, column].Char);
				}
				stateBuilder.AppendLine();
			}
			string state = stateBuilder.ToString();
			return (MarkupString)state;
		}
	}

	public static void ResetColors()
	{
		_backgroundColor = ConsoleColor.Black;
		_foregroundColor = ConsoleColor.White;
	}

	public static async Task Clear()
	{
		ClearNoRefresh();
		await Refresh();
	}

	public static void ClearNoRefresh()
	{
		_writeLine = false;
		for (int row = 0; row < _view.GetLength(0); row++)
		{
			for (int column = 0; column < _view.GetLength(1); column++)
			{
				_view[row, column] = (' ', _backgroundColor, _foregroundColor);
			}
		}
	}

	public static void WriteNoRefresh(char c)
	{
		_writeLine = false;
		if (_cursorPosition.Left >= _view.GetLength(1))
		{
			_cursorPosition = (_cursorPosition.Top + 1, 0);
		}
		_view[_cursorPosition.Top, _cursorPosition.Left] = (c, _backgroundColor, _foregroundColor);
		_cursorPosition = (_cursorPosition.Left + 1, _cursorPosition.Top);
	}

	public static void WriteLineNoRefresh()
	{
		if (_writeLine && _cursorPosition.Left == _view.GetLength(1))
		{
			_cursorPosition = (0, _cursorPosition.Top + 1);
		}
		_writeLine = true;
		while (_cursorPosition.Left < _view.GetLength(1))
		{
			WriteNoRefresh(' ');
		}
		_cursorPosition = (_cursorPosition.Top + 1, 0);
	}

	public static async Task Write(object o)
	{
		_writeLine = false;
		if (o is null) return;
		string? s = o.ToString();
		if (s is null || s is "") return;
		foreach (char c in s)
		{
			WriteNoRefresh(c);
		}
		await Refresh();
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
		await Refresh();
	}

	public static async Task<ConsoleKeyInfo> ReadKey(bool capture)
	{
		_writeLine = false;
		while (!await KeyAvailable())
		{
			await Refresh();
		}
		var keyInfo = _inputBuffer.Dequeue();
		if (capture is false)
		{
			switch (keyInfo.Key)
			{
				case ConsoleKey.Enter:
					WriteLineNoRefresh();
					break;
				case ConsoleKey.Escape or ConsoleKey.Home or ConsoleKey.End:
					break;
				default:
					WriteNoRefresh(keyInfo.KeyChar);
					break;
				//case > ConsoleKey.A and < ConsoleKey.Z:
				//	break;
			}
		}
		return keyInfo;
	}

	public static async Task<string> ReadLine()
	{
		_writeLine = false;
		string line = string.Empty;
		while (true)
		{
			while (!await KeyAvailable())
			{
				await Refresh();
			}
			var keyInfo = _inputBuffer.Dequeue();
			switch (keyInfo.Key)
			{
				case ConsoleKey.Backspace:
					if (line.Length > 0)
					{
						if (_cursorPosition.Left > 0)
						{
							_cursorPosition.Left--;
							_view[_cursorPosition.Top, _cursorPosition.Left] = (' ', _view[_cursorPosition.Top, _cursorPosition.Left].BackgroundColor, _view[_cursorPosition.Top, _cursorPosition.Left].ForegroundColor);
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
					char c = keyInfo.KeyChar;
					line += c;
					WriteNoRefresh(c);
					await Refresh();
					break;
			}
		}
	}

	public static bool CursorVisible
	{
		get
		{
			return _cursorVisible;
		}
		set
		{
			_cursorVisible = value;
		}
	}

	public static async Task<bool> KeyAvailable()
	{
		await Refresh();
		return _inputBuffer.Count > 0;
	}

	public static int WindowWidth
	{
		get
		{
			return _windowWidth;
		}
		set
		{
			_windowWidth = value;
		}
	}

	public static int WindowHeight
	{
		get
		{
			return _windowHeight;
		}
		set
		{
			_windowHeight = value;
		}
	}

	public static async Task SetCursorPosition(int left, int top)
	{
		_cursorPosition = (left, top);
		await Refresh();
	}
}
