using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Components.Parser;
using MinecraftLaunch.Utilities;
using Panuon.WPF.UI;
using System.Diagnostics;
using System.Windows;
using MinecraftLaunch.Components.Authenticator;
using MinecraftLaunch.Extensions;
using MinecraftLaunch.Launch;
using Serilog;

namespace MCLaunch.Views.Pages
{
    /// <summary>
    /// Go.xaml 的交互逻辑
    /// </summary>
    public partial class Go
    {
        public class Launch(string v)
        {

            public async Task ALaunch()
            {

                var sw = Stopwatch.StartNew();

                #region 微软验证

                Console.WriteLine("登录...");
                MicrosoftAuthenticator authenticator = new("5b8c4951-abef-4351-a3fe-65096bb8e76b");
                Console.WriteLine("登录...");
                var oAuth2Token = await authenticator.DeviceFlowAuthAsync(deviceCode =>
                {
                    Console.WriteLine($"请访问以登录: {deviceCode.VerificationUrl}");
                    Console.WriteLine($"输入一次性代码: {deviceCode.UserCode}");
                    if (MessageBoxX.Show($"请访问以登录: {deviceCode.VerificationUrl}", "警告", MessageBoxButton.OKCancel,
                            MessageBoxIcon.Info) == MessageBoxResult.OK)
                    {
                        Process.Start("explorer.exe", deviceCode.VerificationUrl);
                        Clipboard.SetDataObject(deviceCode.UserCode);
                        Console.WriteLine("已复制一次性代码到剪贴板");
                    }
                    else
                    {
                        Console.WriteLine("测试");

                    }
                });
                var account = await authenticator.AuthenticateAsync(oAuth2Token);
                MessageBoxX.Show($"登录成功: {account.Name}", "提示", MessageBoxButton.OK, MessageBoxIcon.Info);
                Console.WriteLine(account.Name);

                #endregion

                #region 第三方验证

                //YggdrasilAuthenticator authenticator = new("https://littleskin.cn/api/yggdrasil", "Your email", "Your password");
                //var result = await authenticator.AuthenticateAsync();
                //foreach (var item in result)
                //    Console.WriteLine(item.Name);

                //var newResult = await authenticator.RefreshAsync(result.First());
                //Console.WriteLine(newResult.Name);

                #endregion

                #region 本地游戏读取

                MinecraftParser minecraftParser = ".\\.minecraft";
                minecraftParser.GetMinecrafts().ForEach(x =>
                {
                    Console.WriteLine(x.Id);
                    Console.WriteLine($"是否为原版：{x.IsVanilla}");

                    if (!x.IsVanilla)
                    {
                        Console.WriteLine("Mod 加载器：" + string.Join("，",
                            (x as ModifiedMinecraftEntry)?.ModLoaders.Select(Mod => $"{Mod.Type}_{Mod.Version}")!));
                    }

                    Console.WriteLine();
                });

                foreach (var processor in MinecraftParser.DataProcessors)
                {
                    foreach (var item in processor.Datas)
                    {
                        Console.WriteLine($"Id:{(item.Value as GameProfileEntry)!.Name}");
                        Console.WriteLine($"Type:{(item.Value as GameProfileEntry)!.Type}");
                        Console.WriteLine(
                            $"Resolution - Width:{(item.Value as GameProfileEntry)!.Resolution?.Width} - Height:{(item.Value as GameProfileEntry)!.Resolution?.Height}");
                        Console.WriteLine();
                    }
                }

                #endregion

                #region 本地 Java 读取

                var asyncJavas = JavaUtil.EnumerableJavaAsync();
                List<JavaEntry> javaList = [];
                await foreach (var java in asyncJavas)
                {
                    Console.WriteLine(java);
                    javaList.Add(java);
                }

                #endregion

                #region NBT 文件操作

                //var minecraft = minecraftParser.GetMinecraft("1.12.2");
                //var save = await minecraft.GetNBTParser().ParseSaveAsync("New World");
                //Console.WriteLine($"存档名：{save.LevelName}");
                //Console.WriteLine($"种子：{save.Seed}");
                //Console.WriteLine($"游戏模式：{save.GameType}");
                //Console.WriteLine($"版本：{save.Version}");

                //var rootTag = @"C:\Users\wxysd\AppData\Roaming\ModrinthApp\profiles\Fabulously Optimized\servers.dat".GetNBTParser()
                //    .GetReader()
                //    .ReadRootTag();

                //var entries = rootTag["servers"].AsTagList<TagCompound>().FirstOrDefault();

                //Console.WriteLine(entries["ip"].AsString());
                //Console.WriteLine(entries["name"].AsString());

                #endregion

                #region 启动

                var newAccount = await authenticator.RefreshAsync(account);
                Console.WriteLine(newAccount.Name); //刷新访问令牌
                var minecraft = minecraftParser.GetMinecraft(v);

                MinecraftRunner runner = new(new LaunchConfig
                {
                    Account = newAccount,
                    MaxMemorySize = 2048,
                    MinMemorySize = 512,
                    LauncherName = "MinecraftLaunch",
                    JavaPath = minecraft.GetAppropriateJava(javaList),
                }, minecraftParser);

                var process = await runner.RunAsync(minecraft);
                process.Started += (_, _) =>
                {
                    Console.WriteLine("Done Launcher Minecraft Java successful!成功了!!!");
                    MessageBoxX.Show("游戏启动成功!");
                };
                process.OutputLogReceived += (_, arg) => Console.WriteLine(arg.Data);
                process.Exited += (_, _) =>
                {
                    Console.WriteLine();
                    Console.WriteLine(string.Join(Environment.NewLine, process.ArgumentList));
                };

                #endregion

                #region 错误分析

                //
                // LogAnalyzer analyzer = new(minecraft);
                // var result = analyzer.Analyze();
                // foreach (var item in result.CrashReasons)
                // {
                //     Console.WriteLine(item);
                // }

                #endregion

                Console.WriteLine("Done!");
                Console.WriteLine($"总耗时：{sw.Elapsed:hh\\:mm\\:ss}");
            }
        }

        private void CheckMemoryStatus()
        {
            var process = Process.GetCurrentProcess();
            // 当前进程内存使用
            var processMemoryMb = process.WorkingSet64 / 1024 / 1024;
            var privateMemoryMb = process.PrivateMemorySize64 / 1024 / 1024;
            Log.Information("[主界面]内存测试程序启动");
            Console.WriteLine($"😸 进程工作集内存: {processMemoryMb} MB");
            Console.WriteLine($"😸 进程私有内存: {privateMemoryMb} MB");
            Console.WriteLine($"😸 GC总内存: {GC.GetTotalMemory(false) / 1024 / 1024} MB");
            Log.Information("😸 进程工作集内存: {ProcessMemoryMb} MB", processMemoryMb);
            Log.Information("😸 进程私有内存: {PrivateMemoryMb} MB", privateMemoryMb);
            Log.Information("😸 GC总内存: {GetTotalMemory} MB", GC.GetTotalMemory(false) / 1024 / 1024);
        }

        private async Task GetJavaVersions()
        {
            var asyncJavas = JavaUtil.EnumerableJavaAsync();
            await foreach (var java in asyncJavas)
            {
                Console.WriteLine(java);
                JavaCombo.Items.Add(java);
            }

            Console.WriteLine("好了");

        }

        public Go()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.Information("[主界面]按下启动按钮,开始启动");
                MessageBoxX.Show("别急,快速打开是不可能的,要想快就捐100000万!!!!!!!(bushi),没给白子充钱导致的(√)");
                Log.Information("弹出提示框,内容: 别急,快速打开是不可能的,要想快就捐100000万!!!!!!!(bushi),没给白子充钱导致的(√)");
                if (VerCombo.SelectedItem is MinecraftEntry info)
                {
                    Log.Information($"[安装界面],启动按钮按下,开始启动 版本:{info.Id}");
                    Launch launcher = new(info.Id);
                    await launcher.ALaunch();
                }
            }
            catch (Exception ex)
            {
                MessageBoxX.Show("启动失败");
                Log.Error(ex, "[主界面]错误");
            }
        }

        private async void JavaButton_CilckClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxX.Show("别急,等一下");
                await GetJavaVersions();
            }
            catch (Exception ex)
            {
                MessageBoxX.Show("获取Java版本失败");
                Log.Error(ex, "[主界面]获取Java版本失败");
            }
        }

        private void VerButton_CilckClick(object sender, RoutedEventArgs e)
        {
            MinecraftParser minecraftParser = ".\\.minecraft";
            Log.Information("[主界面]获取版本列表");
            List<MinecraftEntry> VerList = minecraftParser.GetMinecrafts();
            VerCombo.DisplayMemberPath = "Id";
            VerCombo.SelectedValuePath = "Id";
            VerCombo.ItemsSource = VerList;
            Console.WriteLine("好了");
            Log.Information("[主界面]获取版本列表成功");
            MessageBoxX.Show("已获取版本列表");
        }

        private void RamTestButton_CilckClick(object sender, RoutedEventArgs e)
        {
            CheckMemoryStatus();
        }
    }
}
