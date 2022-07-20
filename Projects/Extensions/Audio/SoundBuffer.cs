using Silk.NET.OpenAL;

namespace Extensions.Audio;

class SoundBuffer
{
	private AL al;
	internal uint buffer { get; private set; }
    
	internal SoundBuffer(AL? al)
	{
		this.al = al!;
		if (al != null) {
			buffer = al.GenBuffer();
		}
	}
    
	~SoundBuffer()
	{
		if (buffer != 0) {
			al.DeleteBuffer(buffer);
		}
	}
    
	public void UploadWaveFile(WaveFile wave)
	{
		BufferFormat format = 0;
		switch (wave.BPS)
		{
			case 8:
				format = wave.Channels == 1 ? BufferFormat.Mono8 : BufferFormat.Mono16;
				break;
			case 16:
				format = wave.Channels == 1 ? BufferFormat.Stereo8 : BufferFormat.Stereo16;
				break;
		}
		unsafe {
			fixed (byte* data = &wave.Data[0]) {
				al.BufferData(buffer, format, data, (int) wave.DataSize, (int) wave.SampleRate);
			}
		}
	}
    
}