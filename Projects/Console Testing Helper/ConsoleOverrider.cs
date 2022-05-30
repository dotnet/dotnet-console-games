namespace ConsoleTestingHelper;

public class ConsoleOverrider : IDisposable
{

	private readonly TextWriter oldOut;
	private readonly MemoryStream outBuffer = new();
	private readonly StreamWriter newOut;
	private readonly StreamReader queuedOutput;
	private long lastReadPosition = 0;

	public void ReadOutput(Action<StreamReader> output)
	{
		newOut.Flush();
		var writePosition = outBuffer.Position;
		outBuffer.Position = lastReadPosition;
		output(queuedOutput);
		lastReadPosition = outBuffer.Position;
		outBuffer.Position = writePosition;
	}

	private readonly TextReader oldIn;
	private readonly MemoryStream inBuffer = new();
	private readonly StreamReader newIn;
	private readonly StreamWriter queuedInput;

	public void WriteInput(Action<StreamWriter> input)
	{
		var startPosition = inBuffer.Position;
		input(queuedInput);
		queuedInput.Flush();
		inBuffer.Position = startPosition;
	}

	public ConsoleOverrider()
	{
		queuedOutput = new(outBuffer);
		queuedInput = new(inBuffer);

		var poo = new StringReader("");

		newOut = new(outBuffer);
		newIn = new(inBuffer);

		oldOut = Console.Out;
		Console.SetOut(newOut);
		oldIn = Console.In;
		Console.SetIn(newIn);

	}

	public void Dispose()
	{
		Console.SetOut(oldOut);
		queuedOutput.Dispose();

		Console.SetIn(oldIn);
		queuedInput.Dispose();
	}
}
