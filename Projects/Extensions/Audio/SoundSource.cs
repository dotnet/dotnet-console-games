using Silk.NET.OpenAL;

namespace Extensions.Audio;

class SoundSource
{
    
	private AL al;
    
	private uint source;
	public SoundBuffer? Buffer { get; private set; }
    
	internal SoundSource(AL? al) {
		this.al = al!;
		if (al != null) {
			source = al.GenSource();
		}
	}
    
	~SoundSource() {
		if (source != 0) {
			al.DeleteSource(source);
		}
	}
    
	public void SetBuffer(SoundBuffer? buffer)
	{
		uint bufferid = buffer == null ? 0 : buffer.buffer;
		Buffer = buffer;
		if (source != 0) {
			al.SetSourceProperty(source, SourceInteger.Buffer, bufferid);
		}
	}
    
	public void Play()
	{
		if (source != 0) {
			al.SourcePlay(source);
		}
	}
    
	public void Stop()
	{
		if (source != 0)
			al.SourceStop(source);
	}
    
	public void Pause()
	{
		if (source != 0)
			al.SourcePause(source);
	}
    
	public void Restart()
	{
		if (source != 0)
			al.SourceRewind(source);
	}
    
}