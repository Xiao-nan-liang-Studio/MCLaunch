using System.IO;
using Serilog;
namespace MCLaunch.MyClass;
using MinecraftLaunch;

public class Init
{
    public void AInit()
    {
        PrepareLogFiles();
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss.fff}<主线程>{Message}{NewLine}")
            .WriteTo.File("./Logs/log1.txt",
                rollingInterval: RollingInterval.Infinite,
                shared: false,
                buffered: false,
                encoding: System.Text.Encoding.UTF8,
                outputTemplate: "[{Timestamp:HH:mm:ss.fff}<主线程>{Message}{NewLine}{Exception}")
            .CreateLogger();
        
        Log.Information("[Start]初始化日志成功");
        
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
        
        Log.Information("[Start]初始化启动相关网络设置成功");
        Console.WriteLine("初始化成功喵~");
        Log.Information("[Start]初始化全部完成");
        
    }
    static void PrepareLogFiles()
    {
        var dir = new DirectoryInfo("logs");
        if (!dir.Exists) dir.Create();

        // 找出现有 log*.txt
        var files = dir.GetFiles("log*.txt")
            .OrderBy(f => f.Name)
            .ToList();

        // 先删除超过 8 的部分（因为稍后要创建 log1）
        while (files.Count > 8)
        {
            files[^1].Delete();
            files.RemoveAt(files.Count - 1);
        }

        // log8 → log9, log7 → log8, ..., log1 → log2
        for (int i = files.Count; i >= 1; i--)
        {
            string oldName = Path.Combine(dir.FullName, $"log{i}.txt");
            if (File.Exists(oldName))
            {
                string newName = Path.Combine(dir.FullName, $"log{i + 1}.txt");
                File.Move(oldName, newName, true);
            }
        }

        // 创建新的 log1.txt
        string newLog = Path.Combine(dir.FullName, "log1.txt");
        File.WriteAllText(newLog, "");   // 清空创建

        // 最终最多 9 个，若超过则删除 log9
        string log9 = Path.Combine(dir.FullName, "log9.txt");
        if (File.Exists(log9))
        {
            var extraFiles = dir.GetFiles("log*.txt")
                .OrderBy(f => f.Name)
                .ToList();
            while (extraFiles.Count > 9)
            {
                extraFiles[^1].Delete();
                extraFiles.RemoveAt(extraFiles.Count - 1);
            }
        }
    }
}