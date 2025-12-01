using System.IO;
using System.Windows;
using Flurl.Http;
using MinecraftLaunch.Base.Models.Network;
using MinecraftLaunch.Components.Installer;
using Panuon.WPF.UI;
using MCLaunch.MyClass;
using Serilog;
using static System.Environment;

namespace MCLaunch.Views.Pages;
public partial class install
{
    public void InitLog()
    {
        var currentDateTime = DateTime.Now;
        using var log = new LoggerConfiguration()
            .WriteTo.File("./Log/log" + currentDateTime.ToString("d"))
            .CreateLogger();
        
    }
    private string IniFileName { get; set; } = CurrentDirectory + "\\Launcher\\Steup.ini";
    public class VersionInfo(VersionManifestEntry entry)
    {
        public string Id { get; set; } = entry.Id;
        public string Type { get; set; } = entry.Type;
    }
    private List<VersionInfo> Version = [];
    private readonly List<VersionInfo> Release = [];
    private readonly List<VersionInfo> Snapshot = [];
    private readonly List<VersionInfo> Very_Very_Old = [];
    private async Task GetGameVersions()
{
    for (; ; )
    {
        try
        {
            IEnumerable<VersionManifestEntry> entries = await VanillaInstaller.EnumerableMinecraftAsync();
            Console.WriteLine("正在获取游戏版本");
            Version.Clear();
            Release.Clear();
            Snapshot.Clear();
            Very_Very_Old.Clear();

            Version = entries.Select(entry => new VersionInfo(entry)).ToList();
            foreach (var entry in Version)
            {
                switch (entry.Type)
                {
                    case "release":
                        Release.Add(entry);
                        Console.WriteLine($"正式版本: {entry.Id}");
                        break;
                    case "snapshot":
                        Snapshot.Add(entry);
                        Console.WriteLine($"快照版本: {entry.Id}");
                        break;
                    case "old_beta":
                    case "old_alpha":
                        Very_Very_Old.Add(entry);
                        Console.WriteLine($"远古版本: {entry.Id}");
                        break;
                }
            }
            Console.WriteLine("游戏版本获取完毕");
            if (VersionList != null)
            {
                VersionList.DisplayMemberPath = null;
                VersionList.SelectedValuePath = "Id";
                VersionList.ItemsSource = Version;
                MessageBoxX.Show("游戏版本获取完毕!", "提示", MessageBoxButton.OK, MessageBoxIcon.Info);
            }
            break;
        }
        catch (FlurlHttpException ex)
        {
            if (ex.Call?.Response?.StatusCode == 429)
            {
                MessageBoxX.Show($"请求数过多，10秒后重试: {ex.Message}", "请求过多", MessageBoxButton.OK, MessageBoxIcon.Warning);
                await Task.Delay(10000);
                continue;
            }
            Console.WriteLine($"请求失败，2秒后重试: {ex.Message}");
            
            // 确保UI控件存在后再操作
            if (VersionList != null)
            {
                VersionList.ItemsSource = new[] { new { Id = $"请求失败，2秒后重试: {ex.Message}" } };
            }
            MessageBoxX.Show($"请求失败，2秒后重试: {ex.Message}", "网络错误", MessageBoxButton.OK, MessageBoxIcon.Warning);
            await Task.Delay(2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"未知错误: {ex.Message}");
            MessageBoxX.Show($"发生未知错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxIcon.Error);
            break;
        }
    }
}



    private async void JavaInstallCilck(object sender, RoutedEventArgs e)
    {
        try{
            //下载 java
            var javaInstaller = JavaInstaller.Create(CurrentDirectory + "\\Launcher\\Java");
            Log.Information("[安装界面]安装java按钮按下,开始安装Java!");
            Log.Information("[安装界面],安装路径:" + CurrentDirectory + @"\Launcher\Java");
            Console.WriteLine("安装路径:" + CurrentDirectory + @"\Launcher\Java");
            javaInstaller.ProgressChanged += (_,ee) =>
            {
                Log.Information("[安装界面],Java安装进度: " + ee.Progress * 100 + "%" + " Java安装步骤:" + ee.StepName + " Java下载速度:" + ee.Speed / 1024*1024 + "MB/s");
            };
            await javaInstaller.InstallAsync();

        }
        catch (Exception ex)
        {
            MessageBoxX.Show($"发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxIcon.Error);
            Log.Error(ex, "[安装界面]Java安装发生错误");
        }
    }

    private void KuaizhaoButton_CilckClick(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Snapshot;
        Log.Information("[安装界面],过滤快照版本");
    }

    private void ZhengshiButton_CilckClickClick(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Release;
        Log.Information("[安装界面],过滤正式版");

    }

    private void ALLButton_CilckClick(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Version;
        Log.Information("[安装界面],显示所有版本");

    }
    private void OldVerButton_Click(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Very_Very_Old;
        Log.Information("[安装界面],过滤旧版本");
        
    }
    public install()
    {
        
        InitializeComponent();
    }

    private async void Install_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (VersionList != null)
            {
                await GetGameVersions();
            }

            Log.Information("[安装界面],初始化完成");
            Download download = new();
            Log.Information("准备调用CurseForge搜索Mod");
            Log.Information("开始搜索,搜索关键词 JEI");
            await download.CurseForge("JEI");
            
        }
        catch (Exception ex)
        {
            MessageBoxX.Show($"发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxIcon.Error);
            Log.Error(ex, "[安装界面]加载发生错误");
        }
    }


    private void TestButton_CilckClick(object sender, RoutedEventArgs e)
    {
        try
        {
            Log.Debug("[安装界面]调试按钮按下");
            // 调试信息
            Console.WriteLine($"当前目录: {CurrentDirectory}");
            Console.WriteLine($"INI文件路径: {IniFileName}");
            Console.WriteLine($"目录是否存在: {Directory.Exists(Path.GetDirectoryName(IniFileName))}");
            Log.Debug($"当前目录: {CurrentDirectory}");
            Log.Debug($"INI文件路径: {IniFileName}");
            Console.WriteLine($"INI文件是否存在: {File.Exists(IniFileName)}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"写入失败: {ex.Message}");
            Console.WriteLine($"错误,\nerror:{ex}");
        }
    }

    
}