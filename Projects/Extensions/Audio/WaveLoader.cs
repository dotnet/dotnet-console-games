using System.Text;

namespace Extensions.Audio;

static class WaveLoader
{
    
	public static WaveFile LoadWaveFile(string path)
	{
		// throws uncaught exceptions intentionally
		WaveFile file = new WaveFile();
        
		using (FileStream fs = File.OpenRead(path)) {
			byte[] header = new byte[8];
			fs.Read(header, 0, 8);
            
			string chunkid = Encoding.ASCII.GetString(header, 0, 4);
			if (chunkid != "RIFF") {
				Console.Error.WriteLine("Not a RIFF");
				return file;
			}
            
			uint file_size = (uint) ((header[4]) | (header[5] << 8) | (header[6] << 16) | (header[7] << 24));
            
			byte[] header2 = new byte[file_size];
			fs.Read(header2, 0, (int) file_size);
            
			string wavetype = Encoding.ASCII.GetString(header2, 0, 4);
			if (wavetype != "WAVE") {
				Console.Error.WriteLine("Not a WAVE");
				return file;
			}
            
			uint index = 0;
			while (index < file_size)
			{
				index += 4;
				string section = Encoding.ASCII.GetString(header2, (int) index, 4);
				uint section_length = (uint) ((header2[index+4]) | (header2[index+5] << 8) | (header2[index+6] << 16) | (header2[index+7] << 24));
				if (section.Equals("fmt ")) {
					ushort channels = (ushort) ((header2[index+10]) | (header2[index+11] << 8));
					uint sample_rate = (uint) ((header2[index+12]) | (header2[index+13] << 8) | (header2[index+14] << 16) | (header2[index+15] << 24));
					ushort bps = (ushort) ((header2[index+22]) | (header2[index+23] << 8));
					file.Channels = channels;
					file.SampleRate = sample_rate;
					file.BPS = bps;
                    
					index += section_length + 4;
					continue;
				}
				if (section == "data") {
					byte[] data = new byte[section_length];
					Array.Copy(header2, data, section_length);
					file.DataSize = section_length;
					file.Data = data;
					break;
				}
                
				index += section_length + 4;
				continue;
			}
            
		}
        
		return file;
	}
    
}

struct WaveFile
{
	public ushort Channels { get; set; }
	public uint SampleRate { get; set; }
	public ushort BPS { get; set; }
	public uint DataSize { get; set; }
	public byte[] Data { get; set; }
}