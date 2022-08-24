using System.Reflection;

namespace Console_Monsters.Audio;

public static class AudioController
{
	public static readonly string CoDA_Lullaby = "Console_Monsters.Audio.CoDA-Lullaby.wav";

	private static readonly System.Media.SoundPlayer? soundPlayer;

	private static string? recoursePlaying;

	static AudioController()
	{
		if (OperatingSystem.IsWindows())
		{
			try
			{
				soundPlayer = new System.Media.SoundPlayer();
			}
			catch
			{
				// intentionally left blank
			}
		}
	}

	public static void PlaySound(string resourceName)
	{
		if (resourceName is null) throw new ArgumentNullException(nameof(resourceName));
		if (AudioEnabled)
		{
			if (OperatingSystem.IsWindows())
			{
				if (soundPlayer is not null)
				{
					if (recoursePlaying != resourceName)
					{
						Assembly assembly = Assembly.GetExecutingAssembly();
						Stream? stream = assembly.GetManifestResourceStream(resourceName);
						if (stream is null) throw new ArgumentException($"the {nameof(resourceName)} embedded resource was not found");
						try
						{
							soundPlayer.Stream = stream;
							soundPlayer.PlayLooping();
						}
						catch
						{
							// intentionally left blank
						}
					}
				}
			}
			recoursePlaying = resourceName;
		}
	}

	public static void StopSound()
	{
		if (recoursePlaying is not null)
		{
			if (OperatingSystem.IsWindows())
			{
				if (soundPlayer is not null)
				{
					try
					{
						soundPlayer.Stop();
					}
					catch
					{
						// intentionally left blank
					}
				}
			}
			recoursePlaying = null;
		}
	}
}
