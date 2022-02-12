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
		if (!capture && keyInfo.KeyChar >= ' ')
		{
			state += keyInfo.KeyChar;
		}
		return keyInfo;
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
