using System.IO;
using System.Windows;
using System.Windows.Controls;
using Flurl.Http;
using MinecraftLaunch.Base.Models.Network;
using MinecraftLaunch.Components.Installer;
using Panuon.WPF.UI;
using CmlLib.Core;
using CmlLib.Core.FileExtractors;
using MCLaunch.MyClass;

namespace MCLaunch.Views.Pages;
public partial class install
{
    
    private string IniFileName { get; set; } = Environment.CurrentDirectory + "\\Launcher\\Steup.ini";
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

    private void MyWritelog(string logMessage)
    {
        IniFile.WriteLog(logMessage);
    }


    private async void JavaInstallCilck(object sender, RoutedEventArgs e)
    {
        //下载 java
        var javaInstaller = JavaInstaller.Create(Environment.CurrentDirectory + "\\Launcher\\Java");
        Console.WriteLine(Environment.CurrentDirectory + "\\Launcher\\Java");
        await javaInstaller.InstallAsync();
        
    }

    private void KuaizhaoButton_CilckClick(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Snapshot;
        MyWritelog("KuaizhaoButton_Cilck");
    }

    private void ZhengshiButton_CilckClickClick(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Release;
        MyWritelog("ZhengshiButton_Cilck");

    }

    private void ALLButton_CilckClick(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Version;
        MyWritelog("ALLButton_Cilck");

    }
    private void OldVerButton_Click(object sender, RoutedEventArgs e)
    {
        VersionList.ItemsSource = Very_Very_Old;
        MyWritelog("OldVerButton_Click");
        
    }
    public install()
    {
        
        InitializeComponent();
    }

    private async void Install_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (VersionList != null)
        {
            await GetGameVersions();
        }
        MyWritelog("Install_OnLoaded");
        Download download = new();
        await download.CurseForge("JEI");
    }


    private void TestButton_CilckClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // 调试信息
            Console.WriteLine($"当前目录: {Environment.CurrentDirectory}");
            Console.WriteLine($"INI文件路径: {IniFileName}");
            Console.WriteLine($"目录是否存在: {Directory.Exists(Path.GetDirectoryName(IniFileName))}");
        
            // 确保目录存在
            var directory = Path.GetDirectoryName(IniFileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine("已创建目录");
            }
        
            IniFile.WriteIniFile(null, "1", "1", IniFileName);
            MessageBoxX.Show("写入成功");
            MyWritelog("TestButton_Cilck");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"写入失败: {ex.Message}");
            Console.WriteLine($"写入失败: {ex.Message}");
        }
    }

    
}