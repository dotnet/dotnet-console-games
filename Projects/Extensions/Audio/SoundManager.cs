namespace Extensions.Audio;

using Silk.NET.OpenAL;

unsafe static class SoundManager
{
    
    public static ALContext? ALC { get; private set; }
    public static AL? OpenAL { get; private set; }
    private static Device* DeviceContext;
    private static Context* OpenALContext;
    
    public static bool IsInitialized { get; private set; }
    
    public static void Initialize()
    {
        ALC = ALContext.GetApi();
        OpenAL = AL.GetApi();
        
        // todo: probe devices, select default, if default doesn't work go down the list
        Device* dc = ALC.OpenDevice("");
        if (dc == null) {
            OpenAL = null;
            ALC = null;
            Console.Error.WriteLine("Failed to open device.");
            return;
        }
        DeviceContext = dc;
        
        Context* context = ALC.CreateContext(dc, null);
        if (context == null) {
            ALC.CloseDevice(dc);
            OpenAL = null;
            ALC = null;
            DeviceContext = null;
            Console.Error.WriteLine("Failed to create context");
            return;
        }
        OpenALContext = context;
        ALC.MakeContextCurrent(context);
        
        ContextError error = ALC.GetError(dc);
        if (error != ContextError.NoError) {
            ALC.DestroyContext(context);
            ALC.CloseDevice(dc);
            OpenAL = null;
            ALC = null;
            DeviceContext = null;
            OpenALContext = null;
            Console.Error.WriteLine("Failed to make context current");
            return;
        }
        
        IsInitialized = true;
    }
    
    public static void Clean()
    {
        if (IsInitialized == false)
            return;
        if (ALC == null)
            return;
        if (OpenAL == null)
            return;
        
        if (OpenALContext != null) {
            ALC.DestroyContext(OpenALContext);
            OpenALContext = null;
        }
        if (DeviceContext != null) {
            ALC.CloseDevice(DeviceContext);
            DeviceContext = null;
        }
        OpenAL.Dispose();
        OpenAL = null;
        ALC.Dispose();
        ALC = null;
    }
    
    public static SoundSource? CreateSource()
    {
        if (IsInitialized == false)
            return null;
        return new SoundSource(OpenAL);
    }
    
    public static SoundBuffer? CreateBuffer()
    {
        if (IsInitialized == false)
            return null;
        return new SoundBuffer(OpenAL);
    }
    
}