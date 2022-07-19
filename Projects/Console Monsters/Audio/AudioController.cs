namespace Console_Monsters.Audio;

public static class AudioController
{
	public static readonly string CoDA_Lullaby = Path.Combine("Audio", "CoDA-Lullaby.wav");

	private readonly static System.Media.SoundPlayer? soundPlayer;

	private static bool isPlaying;

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

	public static void PlaySound(string fileName)
	{
		if (AudioEnabled)
		{
			if (OperatingSystem.IsWindows())
			{
				if (!File.Exists(fileName)) throw new FileNotFoundException("attempted to play a non-existant audio file", fileName);
				if (soundPlayer is not null)
				{
					if (!(isPlaying && soundPlayer.SoundLocation == fileName))
					{
						try
						{
							soundPlayer.SoundLocation = fileName;
							soundPlayer.PlayLooping();
						}
						catch
						{
							// intentionally left blank
						}
					}
				}
			}
			isPlaying = true;
		}
	}

	public static void StopSound()
	{
		if (isPlaying)
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
			isPlaying = false;
		}
	}
}
