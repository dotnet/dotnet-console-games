using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Website;

public static class Resources
{
	public static readonly string[]? Words;
	public static readonly string[]? FiveLetterWords;

	static Resources()
	{
		{
			const string wordsResource = "Website.Words.txt";
			Assembly assembly = Assembly.GetExecutingAssembly();
			using Stream stream = assembly.GetManifestResourceStream(wordsResource)!;
			if (stream is not null)
			{
				List<string> words = new();
				using StreamReader streamReader = new(stream);
				while (!streamReader.EndOfStream)
				{
					string word = streamReader.ReadLine()!;
					words.Add(word);
				}
				Words = words.ToArray();
			}
		}
		{
			const string wordsResource = "Website.FiveLetterWords.txt";
			Assembly assembly = Assembly.GetExecutingAssembly();
			using Stream stream = assembly.GetManifestResourceStream(wordsResource)!;
			if (stream is not null)
			{
				List<string> words = new();
				using StreamReader streamReader = new(stream);
				while (!streamReader.EndOfStream)
				{
					string word = streamReader.ReadLine()!;
					words.Add(word);
				}
				FiveLetterWords = words.ToArray();
			}
		}
	}
}
