using System.Windows;
using Flurl.Http;
using MinecraftLaunch.Base.Models.Network;
using MinecraftLaunch.Components.Installer;
using Panuon.WPF.UI;
using MCLaunch.MyClass;
using MinecraftLaunch.Base.EventArgs;
using MinecraftLaunch.Components.Downloader;
using Serilog;
using static System.Environment;

namespace MCLaunch.Views.Pages;
public partial class Install
{
    private class AInstall
    {
        private readonly Install _installPage;
        private readonly string? _versionId;
        
        public AInstall(Install installPage, string? versionId)
        {
            _installPage = installPage;
            _versionId = versionId;
        }

        public async Task Vanilla()
        {
            var entry = (await VanillaInstaller.EnumerableMinecraftAsync())
                .First(x => x.Id == _versionId);
                        
            var installer = VanillaInstaller.Create(".\\.minecraft", entry);
            installer.ProgressChanged += (_, arg) =>
            {
                Log.Information(
                    $"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:F2}%" : $"{arg.Progress * 100:F2}%")}");
                _installPage.Bar.Value = arg.Progress;
            };
            installer.Completed += (_, arg) =>
                Log.Information("[安装界面]" + (arg.IsSuccessful ? "安装成功" : $"安装失败 - {arg.Exception}"));
            var minecraft = await installer.InstallAsync();
            Log.Information("\t版本:" + minecraft.Id);
        }

        public async Task Forge()
        {
            var entry1 = (await ForgeInstaller.EnumerableForgeAsync(_versionId))
                .First();
            var installer1 = ForgeInstaller.Create(".\\.minecraft", "C:\\Program Files\\Microsoft\\jdk-21.0.7.6-hotspot\\bin\\javaw.exe", entry1);
            installer1.ProgressChanged += (_, arg) =>
                Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");
            var minecraft1 = await installer1.InstallAsync();
            Console.WriteLine(minecraft1.Id);
        }
        public async Task Optifine()
        { 
            var entry = (await OptifineInstaller.EnumerableOptifineAsync(_versionId))
                .First();
                var installer = OptifineInstaller.Create(".\\.minecraft", "C:\\Program Files\\Microsoft\\jdk-21.0.7.6-hotspot\\bin\\javaw.exe", entry);
                installer.ProgressChanged += (_, arg) =>
                 Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");
                 var minecraft2 = await installer.InstallAsync();
                 Console.WriteLine(minecraft2.Id);
        }
        public async Task Fabric()
        { 
            var entry = (await FabricInstaller.EnumerableFabricAsync(_versionId))
                .First();
                var installer = FabricInstaller.Create(".\\.minecraft", entry);
                installer.ProgressChanged += (_, arg) =>
                 Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");
                var minecraft3 = await installer.InstallAsync();
                Console.WriteLine(minecraft3.Id);
        }
        public async Task Quilt()
        { 
            var entry = (await QuiltInstaller.EnumerableQuiltAsync(_versionId))
                .First();
                var installer = QuiltInstaller.Create(".\\.minecraft", entry);
                installer.ProgressChanged += (_, arg) =>
                 Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");
                 var minecraft4 = await installer.InstallAsync();
                 Console.WriteLine(minecraft4.Id);
        }
    }
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
            if (VersionList != null)
            {
                VersionList.DisplayMemberPath = null;
                VersionList.SelectedValuePath = "Id";
                VersionList.ItemsSource = Version;
                VersionList.SelectedItem = Version.First();
                MessageBoxX.Show("游戏版本获取完毕!", "提示", MessageBoxButton.OK, MessageBoxIcon.Info);
                Log.Information("[安装界面]游戏版本获取完毕!");
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
            // 定义一个 handler 变量，这样才能在内部解除绑定
            EventHandler<InstallProgressChangedEventArgs>? handler = null;
            handler = (_, ee) =>
            {
                Log.Information("[Java安装],Java安装进度: {Progress}% Java安装步骤:{Step} 下载速度:{Speed} MB/s",
                    ee.Progress * 100,
                    ee.StepName,
                    ee.Speed / 1024 / 1024
                );
                
                // 当进度达到 100% 时，自动退订
                if (ee.Progress >= 1.0)
                {
                    javaInstaller.ProgressChanged -= handler!;
                    Log.Information("[Java安装]Java 安装完成，已注销 ProgressChanged 事件订阅。");
                }
            };
            //订阅事件
            javaInstaller.ProgressChanged += handler;
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
    public Install()
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

    private async void Install_Button_Click(object sender, RoutedEventArgs e)
    {
        try{
            if (VersionList.SelectedItem is VersionInfo info)
            {
                Log.Information($"[安装界面],安装按钮按下,开始安装 版本:{info.Id}");
            
                    AInstall installer = new(this, info.Id);
                    await installer.Vanilla();
            }
            else
            {
                Log.Information("[安装界面]弹出对话框:请选择一个版本!");
                MessageBoxX.Show("请选择一个版本！", "提示", MessageBoxButton.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBoxX.Show($"发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxIcon.Error);
            Log.Error(ex, "[安装界面]安装发生错误");
        }
    }
}