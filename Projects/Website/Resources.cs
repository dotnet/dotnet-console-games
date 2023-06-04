using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Website;

public static class Resources
{
	public static readonly string[]? Words;
	public static readonly string[]? FiveLetterWords;
	public static readonly string? Company_json;
	public static readonly string? Event_json;
	public static readonly string? GlobalEvent_json;

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
		{
			const string name = "Website.Company.json";
			Assembly assembly = Assembly.GetExecutingAssembly();
			using Stream stream = assembly.GetManifestResourceStream(name)!;
			if (stream is not null)
			{
				using StreamReader streamReader = new(stream);
				Company_json = streamReader.ReadToEnd();
			}
		}
		{
			const string name = "Website.Event.json";
			Assembly assembly = Assembly.GetExecutingAssembly();
			using Stream stream = assembly.GetManifestResourceStream(name)!;
			if (stream is not null)
			{
				using StreamReader streamReader = new(stream);
				Event_json = streamReader.ReadToEnd();
			}
		}
		{
			const string name = "Website.GlobalEvent.json";
			Assembly assembly = Assembly.GetExecutingAssembly();
			using Stream stream = assembly.GetManifestResourceStream(name)!;
			if (stream is not null)
			{
				using StreamReader streamReader = new(stream);
				GlobalEvent_json = streamReader.ReadToEnd();
			}
		}
	}
}
