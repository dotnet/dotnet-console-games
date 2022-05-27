using System;
using System.IO;

namespace Guess_A_Number.Test;
public class ConsoleOverrider : IDisposable
{

	private readonly TextWriter oldOut;
	private readonly MemoryStream outBuffer = new();
	private readonly StreamWriter newOut;
	private readonly StreamReader queuedOutput;

	public void ReadOutput(Action<StreamReader> output)
	{
		newOut.Flush();
		var oldOutBufferPosition = outBuffer.Position;
		outBuffer.Position = 0;
		output(queuedOutput);
		outBuffer.Position = oldOutBufferPosition;
	}

	private readonly TextReader oldIn;
	private readonly MemoryStream inBuffer = new();
	private readonly StreamReader newIn;
	private readonly StreamWriter queuedInput;

	public void AddInput(Action<StreamWriter> input)
	{
		input(queuedInput);
		queuedInput.Flush();
		inBuffer.Position = 0;
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
