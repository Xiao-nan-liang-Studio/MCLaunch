namespace MCLaunch.MyClass;
using MinecraftLaunch;

public class Init
{
    public void AInit()
    {
        InitializeHelper.Initialize(settings =>
        {
            settings.MaxThread = 256;
            settings.MaxFragment = 128;
            settings.MaxRetryCount = 4;
            settings.IsEnableMirror = true;
            settings.IsEnableFragment = true;
            settings.CurseForgeApiKey = "$2a$10$yXBSeP9DfAZGiZq9jrUDk.ZGQvcEXN3aybUVA5TbwJxz00P4WU9Sm";
            settings.UserAgent = "Mozilla/5.0";
        });
        Console.WriteLine("初始化成功喵~");

    }
}