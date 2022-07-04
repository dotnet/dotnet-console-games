using System.Text;

namespace Chess;

public static class Program
{
	public static void Main()
	{
		var encoding = Console.OutputEncoding;

		try
		{
			
		}
		finally
		{
			Console.OutputEncoding = encoding;
			Console.CursorVisible = true;
			Console.Clear();
			Console.Write("Chess was closed.");
		}
	}
}

