/*namespace Extensions.Audio;

public partial class Sound
{
    public static byte[] LoadWav(byte[] data, out int channels, out int sampleRate, out int bitsPerSample)
    {
        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream);
        
        // Header
        if (new string(reader.ReadChars(4)) != "RIFF") // ChunkID
            throw new Exception("Given file is not a wave file.");
        
        reader.ReadInt32(); // ChunkSize
        
        if (new string(reader.ReadChars(4)) != "WAVE") // Format
            throw new Exception("Given wave file is not valid.");

        string fmtstr = new string(reader.ReadChars(4));
        string testStr = "";
        long pos = reader.BaseStream.Position;
        if (fmtstr == "JUNK")
        {
            while (testStr != "fmt ")
            {
                try
                {
                    testStr = new string(reader.ReadChars(4));
                }
                catch (Exception) { }

                reader.BaseStream.Position = ++pos;
            }

            reader.BaseStream.Position = pos - 1;
        }
        else if (fmtstr != "fmt ") // Subchunk1ID
            throw new Exception("Given wave file is not valid.");

        reader.ReadInt32(); // Subchunk1Size
        if (reader.ReadInt16() != 1) // AudioFormat
            throw new Exception("Compressed wave files cannot be loaded.");

        channels = reader.ReadInt16();
        sampleRate = reader.ReadInt32();

        reader.ReadInt32(); // ByteRate, we just calculate this when needed.
        reader.ReadInt16(); // BlockAlign

        bitsPerSample = reader.ReadInt16();
        
        // Data
        
        testStr = ""; 
        pos = reader.BaseStream.Position;
        while (testStr != "data")
        {
            try
            {
                testStr = new string(reader.ReadChars(4));
            }
            catch (Exception)
            {
	            // ignored
            }

            reader.BaseStream.Position = ++pos;
        }
        reader.BaseStream.Position = pos - 1;

        if (new string(reader.ReadChars(4)) != "data") // Subchunk2ID
            throw new Exception("Given wave file is not valid.");

        int size = reader.ReadInt32(); // Subchunk2Size
        return reader.ReadBytes(size);
    }

    private static void MonoToStereo(ref byte[] data, byte bitsPerSample)
    {
        byte[] newData = new byte[data.Length << 1];

        int newI = 0;
        byte align = (byte) (bitsPerSample / 8);
        for (int i = 0; i < data.Length; i += align)
        {
            for (int a = 0; a < align; a++)
            {
                newData[newI++] = data[i + a];
            }
        }

        data = newData;
    }
}*/