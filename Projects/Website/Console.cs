﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Website;

public static class Console<TGame>
{
	internal const int _delay = 1;
	internal static Action? _refresh;
	internal static bool _cursorVisible = true;
	internal static Queue<ConsoleKeyInfo> _inputBuffer = new();
	internal static ConsoleColor _backgroundColor = ConsoleColor.Black;
	internal static ConsoleColor _foregroundColor = ConsoleColor.White;
	internal static (int Left, int Top) _cursorPosition = (0, 0);
	internal static (char Char, ConsoleColor BackgroundColor, ConsoleColor ForegroundColor)[,] _view;
	internal static int _windowHeight = 30;
	internal static int _windowWidth = 120;
	internal static bool _refreshOnInputOnly = true;

	public static void OnKeyDown(KeyboardEventArgs e)
	{
		switch (e.Key)
		{
			case "Backspace":  _inputBuffer.Enqueue(new('\b', ConsoleKey.Backspace,  false, false, false)); break;
			case " ":          _inputBuffer.Enqueue(new(' ',  ConsoleKey.Spacebar,   false, false, false)); break;
			case "Delete":     _inputBuffer.Enqueue(new('\0', ConsoleKey.Delete,     false, false, false)); break;
			case "Enter":      _inputBuffer.Enqueue(new('\n', ConsoleKey.Enter,      false, false, false)); break;
			case "Escape":     _inputBuffer.Enqueue(new('\0', ConsoleKey.Escape,     false, false, false)); break;
			case "ArrowLeft":  _inputBuffer.Enqueue(new('\0', ConsoleKey.LeftArrow,  false, false, false)); break;
			case "ArrowRight": _inputBuffer.Enqueue(new('\0', ConsoleKey.RightArrow, false, false, false)); break;
			case "ArrowUp":    _inputBuffer.Enqueue(new('\0', ConsoleKey.UpArrow,    false, false, false)); break;
			case "ArrowDown":  _inputBuffer.Enqueue(new('\0', ConsoleKey.DownArrow,  false, false, false)); break;
			default:
				if (e.Key.Length is 1 && e.Key[0] >= '0' && e.Key[0] <= '9')
				{
					_inputBuffer.Enqueue(new(e.Key[0], ConsoleKey.D0 + (e.Key[0] - '0'), false, false, false));
				}
				if (e.Key.Length is 1 && e.Key[0] >= 'a' && e.Key[0] <= 'z')
				{
					_inputBuffer.Enqueue(new(e.Key[0], ConsoleKey.A + (e.Key[0] - 'a'), false, false, false));
				}
				else if (e.Key.Length is 1 && e.Key[0] >= 'A' && e.Key[0] <= 'Z')
				{
					_inputBuffer.Enqueue(new(e.Key[0], ConsoleKey.A + (e.Key[0] - 'A'), true, false, false));
				}
				return;
		}
	}

	static Console()
	{
		_view = new (char, ConsoleColor, ConsoleColor)[WindowHeight, WindowWidth];
		ClearNoRefresh();
	}

	public static async Task RefreshAndDelay(TimeSpan timeSpan)
	{
		_refresh?.Invoke();
		await Task.Delay(timeSpan);
	}

	public static async Task Refresh()
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
					if (_cursorVisible && _cursorPosition == (column, row))
					{
						stateBuilder.Append(@"<span class=""cursor"">");
					}
					stateBuilder.Append(HttpUtility.HtmlEncode(_view[row, column].Char));
					if (_cursorVisible && _cursorPosition == (column, row))
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
		_backgroundColor = ConsoleColor.Black;
		_foregroundColor = ConsoleColor.White;
	}

	public static async Task Clear()
	{
		ClearNoRefresh();
		if (!_refreshOnInputOnly)
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
				_view[row, column] = (' ', _backgroundColor, _foregroundColor);
			}
		}
		_cursorPosition = (0, 0);
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
		if (_cursorPosition.Left >= _view.GetLength(1))
		{
			_cursorPosition = (0, _cursorPosition.Top + 1);
		}
		_view[_cursorPosition.Top, _cursorPosition.Left] = (c, _backgroundColor, _foregroundColor);
		_cursorPosition = (_cursorPosition.Left + 1, _cursorPosition.Top);
	}

	public static void WriteLineNoRefresh()
	{
		while (_cursorPosition.Left < _view.GetLength(1))
		{
			WriteNoRefresh(' ');
		}
		_cursorPosition = (0, _cursorPosition.Top + 1);
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
		if (!_refreshOnInputOnly)
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
		if (!_refreshOnInputOnly)
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

	public static int CursorLeft
	{
		get
		{
			return _cursorPosition.Left;
		}
		set
		{
			_cursorPosition.Left = value;
		}
	}

	public static int CursorTop
	{
		get
		{
			return _cursorPosition.Top;
		}
		set
		{
			_cursorPosition.Top = value;
		}
	}

	public static async Task SetCursorPosition(int left, int top)
	{
		_cursorPosition = (left, top);
		if (!_refreshOnInputOnly)
		{
			await Refresh();
		}
	}
}