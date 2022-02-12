namespace Blazor;

public static class Console<TGame>
{
	const int delay = 1;

	public static Action? refresh;

	public static Queue<ConsoleKeyInfo> inputBuffer = new();

	public static string state = string.Empty;

	public static void Clear()
	{
		state = string.Empty;
		refresh?.Invoke();
	}

	public static void Write(object o)
	{
		state += o.ToString();
		refresh?.Invoke();
	}

	public static void WriteLine()
	{
		state += Environment.NewLine;
		refresh?.Invoke();
	}

	public static void WriteLine(object o)
	{
		state += o.ToString() + Environment.NewLine;
		refresh?.Invoke();
	}

	public static async Task<ConsoleKeyInfo> ReadKey(bool capture)
	{
		while (!await KeyAvailable())
		{
			await Task.Delay(delay);
			refresh?.Invoke();
		}
		var keyInfo = inputBuffer.Dequeue();
		state += keyInfo.KeyChar;
		return keyInfo;
	}

	public static string? line;

	public static async Task<string> ReadLine()
	{
		line = string.Empty;
		while (true)
		{
			while (!await KeyAvailable())
			{
				await Task.Delay(delay);
				refresh?.Invoke();
			}
			var keyInfo = inputBuffer.Dequeue();
			switch (keyInfo.Key)
			{
				case ConsoleKey.Backspace:
					if (line.Length > 0)
					{
						line = line[..^1];
						state = state[..^1];
						await Task.Delay(delay);
						refresh?.Invoke();
					}
					break;
				case ConsoleKey.Enter:
					state += '\n';
					await Task.Delay(delay);
					refresh?.Invoke();
					string l = line;
					return l;
				default:
					char c = keyInfo.KeyChar;
					line += c;
					state += c;
					await Task.Delay(delay);
					refresh?.Invoke();
					break;
			}
		}
	}

	public static bool _cursorVisible;

	public static bool CursorVisible
	{
		get => _cursorVisible;
		set => _cursorVisible = value;
	}

	public static async Task<bool> KeyAvailable()
	{
		await Task.Delay(delay);
		return inputBuffer.Count > 0;
	}
}
