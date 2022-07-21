using static Extensions.Audio.AudioDevice;

namespace Extensions.Audio;

/// <summary>
/// Represents an audio buffer that can be used with the <see cref="AudioDevice"/>.
/// </summary>
public struct AudioBuffer : IDisposable
{
	internal uint Handle;

	/// <summary>
	/// Used to determine if this AudioBuffer exists or not.
	/// </summary>
	public readonly bool Exists;

	internal AudioBuffer(uint handle)
	{
		Handle = handle;
		Exists = true;
	}

	public void Dispose()
	{
		Al.DeleteBuffer(Handle);
	}
	
}