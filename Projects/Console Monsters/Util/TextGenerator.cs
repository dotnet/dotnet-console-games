namespace Console_Monsters.Util;

public static class TextGenerator
{
	/// <summary>
	/// Generate medium sized ascii text art from a string.
	/// All outputs will be uppercase.
	/// </summary>
	/// <param name="text"></param>
	public static string[] GenerateAsciiText(string text)
	{
		text = text.ToLower().Trim();
		var alphabet = Alphabet.AsciiLetters;

		try
		{
			// We will always have 3 rows in our letter library so 3 loops will suffice
			// Each loop is a row we are iterating over.
			var top = "";
			foreach (var c in text)
			{
				for (int i = 0; i < alphabet[c][0].Length; i++)
				{
					if (alphabet.ContainsKey(c)) top = $"{top}{alphabet[c][0][i]}";
				}
			}

			var middle = "";
			foreach (var c in text)
			{
				for (int i = 0; i < alphabet[c][1].Length; i++)
				{
					if (alphabet.ContainsKey(c))
						middle = $"{middle}{alphabet[c][1][i]}";
				}
			}

			var bottom = "";
			foreach (var c in text)
			{
				for (int i = 0; i < alphabet[c][2].Length; i++)
				{
					if (alphabet.ContainsKey(c)) bottom = $"{bottom}{alphabet[c][2][i]}";
				}
			}
			
			return new[] {top, middle, bottom};
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}