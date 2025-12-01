using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Components.Authenticator;
using MinecraftLaunch.Components.Parser;
using MinecraftLaunch.Extensions;
using MinecraftLaunch.Launch;
using MinecraftLaunch.Utilities;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MCLaunch.MyClass;
using Panuon.WPF.UI;
using Serilog;

namespace MCLaunch
{

    public class Launch
    {

        public async Task ALaunch()
        {

            var sw = Stopwatch.StartNew();
            #region 原版安装器

            // var entry = (await VanillaInstaller.EnumerableMinecraftAsync())
            //     .First(x => x.Id == "1.20.1");
            //
            // var installer = VanillaInstaller.Create(".\\.minecraft", entry);
            // installer.ProgressChanged += (_, arg) =>
            //     Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:F2}%" : $"{arg.Progress * 100:F2}%")}");
            //
            // var minecraft = await installer.InstallAsync();
            // Console.WriteLine(minecraft.Id);

            #endregion

            #region Forge 安装器

            // var entry1 = (await ForgeInstaller.EnumerableForgeAsync("1.21.8"))
            //     .First();
            //
            // var installer1 = ForgeInstaller.Create("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft", "C:\\Program Files\\Microsoft\\jdk-21.0.7.6-hotspot\\bin\\javaw.exe", entry1);
            // installer1.ProgressChanged += (_, arg) =>
            //     Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");
            //
            // var minecraft1 = await installer1.InstallAsync();
            // Console.WriteLine(minecraft1.Id);

            #endregion

            #region Optifine 安装器

            //var entry2 = (await OptifineInstaller.EnumerableOptifineAsync("1.20.1"))
            //    .First();

            //var installer2 = OptifineInstaller.Create("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft", "C:\\Program Files\\Java\\latest\\jre-1.8\\bin\\java.exe", entry2);
            //installer2.ProgressChanged += (_, arg) =>
            //    Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed,true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");

            //var minecraft2 = await installer2.InstallAsync();
            //Console.WriteLine(minecraft2.Id);

            #endregion

            #region Fabric 安装器

            //var entry3 = (await FabricInstaller.EnumerableFabricAsync("1.20.1"))
            //    .First();

            //var installer3 = FabricInstaller.Create("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft", entry3);
            //installer3.ProgressChanged += (_, arg) =>
            //    Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");

            //var minecraft3 = await installer3.InstallAsync();
            //Console.WriteLine(minecraft3.Id);

            #endregion

            #region Quilt 安装器

            //var entry4 = (await QuiltInstaller.EnumerableQuiltAsync("1.20.1"))
            //    .First();

            //var installer4 = QuiltInstaller.Create("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft", entry4);
            //installer4.ProgressChanged += (_, arg) =>
            //    Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");

            //var minecraft4 = await installer4.InstallAsync();
            //Console.WriteLine(minecraft4.Id);

            #endregion

            #region Java 安装器
            //var javaInstaller = JavaInstaller.Create("./Java");
            //await javaInstaller.InstallAsync();
            #endregion

            #region 复合安装器

            //var mcId = "1.21.8";
            //var mc = (await VanillaInstaller.EnumerableMinecraftAsync())
            //    .First(x => x.McVersion.Equals(mcId));

            //var forgeEntry = (await ForgeInstaller.EnumerableForgeAsync(mcId))
            //    .First();

            //var installer5 = CompositeInstaller.Create([mc, forgeEntry], "C:\\Users\\wxysd\\Desktop\\temp\\.minecraft", "C:\\Program Files\\Microsoft\\jdk-21.0.7.6-hotspot\\bin\\javaw.exe", "ForgeMC");
            //installer5.ProgressChanged += (_, arg) =>
            //    Console.WriteLine($"{(arg.PrimaryStepName is InstallStep.Undefined ? "" : $"{arg.PrimaryStepName} - ")}{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");

            //installer5.Completed += (_, arg) =>
            //    Console.WriteLine(arg.IsSuccessful ? "安装成功" : $"安装失败 - {arg.Exception}");

            //var minecraft5 = await installer5.InstallAsync();
            //Console.WriteLine(minecraft5.Id);

            #endregion

            #region Curseforge 整合包安装器

            //var modpackEntry = CurseforgeModpackInstaller.ParseModpackInstallEntry(@"C:\Users\wxysd\Desktop\temp\Fabulously.Optimized-5.4.1.zip");
            //var installEntrys = CurseforgeModpackInstaller.ParseModLoaderEntryByManifestAsync(modpackEntry);

            //var cfModpackInstaller = CurseforgeModpackInstaller.Create("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft", 
            //    @"C:\Users\wxysd\Desktop\temp\Fabulously.Optimized-5.4.1.zip", 
            //    modpackEntry, new MinecraftParser("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft").GetMinecraft("Fabulously Optimized"));

            //cfModpackInstaller.ProgressChanged += (_, arg) =>
            //    Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");

            //var minecraft5 = await cfModpackInstaller.InstallAsync();
            //Console.WriteLine(minecraft5.Id);

            #endregion

            #region Modrinth 整合包安装器

            //var modpackEntry1 = ModrinthModpackInstaller.ParseModpackInstallEntry(@"C:\Users\wxysd\Desktop\temp\Zombie Invade 100 Days 2.1.mrpack");
            //var installerEntry1 = await ModrinthModpackInstaller.ParseModLoaderEntryAsync(modpackEntry1);

            //var mdModpackInstaller = ModrinthModpackInstaller.Create("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft",
            //    @"C:\Users\wxysd\Desktop\temp\Zombie Invade 100 Days 2.1.mrpack", 
            //    modpackEntry1, new MinecraftParser("C:\\Users\\wxysd\\Desktop\\temp\\.minecraft").GetMinecraft("Zombie Invade 100 Days"));

            //mdModpackInstaller.ProgressChanged += (_, arg) =>
            //    Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{DefaultDownloader.FormatSize(arg.Speed, true)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");

            //var minecraft6 = await mdModpackInstaller.InstallAsync();
            //Console.WriteLine(minecraft6.Id);

            #endregion

            #region Mcbbs 整合包安装器

            //var modpackEntry2 = McbbsModpackInstaller.ParseModpackInstallEntry(@"C:\Users\wxysd\Desktop\temp\mcbbs_test.zip");
            //var mcbbsModpackInstaller = McbbsModpackInstaller.Create(@"C:\Users\wxysd\Desktop\temp\DaiYu\.minecraft", @"C:\Users\wxysd\Desktop\temp\mcbbs_test.zip", modpackEntry2, new MinecraftParser(@"C:\Users\wxysd\Desktop\temp\DaiYu\.minecraft").GetMinecraft("1.12.2"));
            //mcbbsModpackInstaller.ProgressChanged += (_, arg) =>
            //    Console.WriteLine($"{arg.StepName} - {arg.FinishedStepTaskCount}/{arg.TotalStepTaskCount} - {(arg.IsStepSupportSpeed ? $"{FileDownloader.GetSpeedText(arg.Speed)} - {arg.Progress * 100:0.00}%" : $"{arg.Progress * 100:0.00}%")}");

            //var minecraft7 = await mcbbsModpackInstaller.InstallAsync();
            //Console.WriteLine(minecraft7.Id);

            #endregion

            #region Curseforge API

            //CurseforgeProvider curseforgeProvider = new();
            //foreach (var cfResource in await curseforgeProvider.SearchResourcesAsync("JEI")) {
            //    Console.WriteLine("Id： " + cfResource.Id);
            //    Console.WriteLine("ClassId： " + cfResource.ClassId);
            //    Console.WriteLine("Name： " + cfResource.Name);
            //    Console.WriteLine("Summary： " + cfResource.Summary);
            //    Console.WriteLine("IconUrl： " + cfResource.IconUrl);
            //    Console.WriteLine("WebsiteUrl： " + cfResource.WebsiteUrl);
            //    Console.WriteLine("DownloadCount： " + cfResource.DownloadCount);
            //    Console.WriteLine("DateModified： " + cfResource.DateModified);
            //    Console.WriteLine("MinecraftVersions： " + string.Join('，', cfResource.MinecraftVersions));
            //    Console.WriteLine("Categories： " + string.Join('，', cfResource.Categories));
            //    Console.WriteLine("Authors： " + string.Join('，', cfResource.Authors));
            //    Console.WriteLine("Screenshots： " + string.Join('，', cfResource.Screenshots));
            //    Console.WriteLine("LatestFiles - FileName： " + string.Join('，', cfResource.LatestFiles.Select(x => x.FileName)));
            //    Console.WriteLine();
            //}

            //foreach (var cfResources in await curseforgeProvider.GetResourceFilesByFingerprintsAsync([568671043])) {
            //    var cfResource = cfResources.Key;

            //    Console.WriteLine("Id： " + cfResource.Id);
            //    Console.WriteLine("ModId： " + cfResource.ModId);
            //    Console.WriteLine("FileName： " + cfResource.FileName);
            //    Console.WriteLine("Published： " + cfResource.Published);
            //    Console.WriteLine("IsAvailable： " + cfResource.IsAvailable);
            //    Console.WriteLine("ReleaseType： " + cfResource.ReleaseType);
            //    Console.WriteLine("DownloadUrl： " + cfResource.DownloadUrl);
            //    Console.WriteLine("FileFingerprint： " + cfResource.FileFingerprint);
            //    Console.WriteLine("MinecraftVersions： " + string.Join('，', cfResource.MinecraftVersions));
            //    Console.WriteLine();
            //}

            #endregion

            #region Modrinth API

            //ModrinthProvider modrinthProvider = new();
            //foreach (var mhResource in await modrinthProvider.SearchAsync("Toki")) {
            //    Console.WriteLine(mhResource.Name);
            //    Console.WriteLine(mhResource.Author);
            //    Console.WriteLine(mhResource.Updated);
            //    Console.WriteLine(mhResource.DateModified);
            //    Console.WriteLine(mhResource.Summary);
            //    Console.WriteLine(mhResource.IconUrl);
            //    Console.WriteLine($"Categories: {string.Join('，', mhResource.Categories)}");
            //    Console.WriteLine($"ScreenshotUrls: {string.Join('，', mhResource.Screenshots)}");
            //    Console.WriteLine($"MinecraftVersions: {string.Join('，', mhResource.MinecraftVersions)}");
            //    Console.WriteLine();
            //}

            #endregion

            #region 微软验证

            MicrosoftAuthenticator authenticator = new("291eedbc-7ca4-4af2-9231-9c9ff1009c10");
            var oAuth2Token = await authenticator.DeviceFlowAuthAsync(x =>
            {
                MessageBoxX.Show($"请访问以登录:{x.VerificationUrl}");
                MessageBoxX.Show($"输入一次性代码:{x.UserCode}");
            });
            var account = await authenticator.AuthenticateAsync(oAuth2Token);
            Console.WriteLine(account.Name);
            Console.WriteLine();
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
                    Console.WriteLine("Mod 加载器：" + string.Join("，", (x as ModifiedMinecraftEntry)?.ModLoaders.Select(Mod => $"{Mod.Type}_{Mod.Version}")!));
                }

                Console.WriteLine();
            });

            foreach (var processor in MinecraftParser.DataProcessors)
            {
                foreach (var item in processor.Datas)
                {
                    Console.WriteLine($"Id:{(item.Value as GameProfileEntry)!.Name}");
                    Console.WriteLine($"Type:{(item.Value as GameProfileEntry)!.Type}");
                    Console.WriteLine($"Resolution - Width:{(item.Value as GameProfileEntry)!.Resolution?.Width} - Height:{(item.Value as GameProfileEntry)!.Resolution?.Height}");
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
                Console.WriteLine(newAccount.Name);//刷新访问令牌
                var minecraft = minecraftParser.GetMinecraft("1.20.1");

                MinecraftRunner runner = new(new LaunchConfig
                {
                    Account = newAccount,
                    MaxMemorySize = 2048,
                    MinMemorySize = 512,
                    LauncherName = "MinecraftLaunch",
                    JavaPath = minecraft.GetAppropriateJava(javaList),
                }, minecraftParser);

                var process = await runner.RunAsync(minecraft);
                process.Started += (_, _) => { Console.WriteLine("Done Launcher Minecraft Java successful!成功了!!!");
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

        public async Task login()
        {
            #region 微软验证
            Console.WriteLine("登录...");
            MicrosoftAuthenticator authenticator = new("5b8c4951-abef-4351-a3fe-65096bb8e76b");
            Console.WriteLine("登录...");
            var oAuth2Token = await authenticator.DeviceFlowAuthAsync(deviceCode => {
                Console.WriteLine($"请访问以登录: {deviceCode.VerificationUrl}");
                Console.WriteLine($"输入一次性代码: {deviceCode.UserCode}");
                if (MessageBoxX.Show($"请访问以登录: {deviceCode.VerificationUrl}", "警告", MessageBoxButton.OKCancel, MessageBoxIcon.Info) == MessageBoxResult.OK)
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
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //创建ViewModel

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private static readonly Dictionary<Type, Page> bufferedPages =
       new Dictionary<Type, Page>();
        
        /// <summary>
        /// 页面跳转逻辑
        /// </summary>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 如果选择项不是 ListBoxItem, 则返回
            if (navMenu.SelectedItem is not ListBoxItem item)
                return;

            // 如果 Tag 不是一个类型, 则返回
            if (item.Tag is not Type type)
                return;

            // 如果页面缓存中找不到页面, 则创建一个新的页面并存入
            if (!bufferedPages.TryGetValue(type, out Page? page))
                page = bufferedPages[type] =
                    Activator.CreateInstance(type) as Page ?? throw new Exception("this would never happen");

            // 使用 Frame 进行导航.
            appFrame.Navigate(page);

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                navMenu.SelectedIndex = 0;
                var init = new Init();
                init.AInit();
                var asyncJavas = JavaUtil.EnumerableJavaAsync();
                await foreach (var java in asyncJavas)
                    Console.WriteLine(java);
            }
            catch(Exception ex)
            {
                MessageBoxX.Show(ex.Message, "哎呀~出了一点问题~~~主人不要介意嘛~~~~~~~", MessageBoxButton.OK, MessageBoxIcon.Error);
                Log.Error(ex,"[窗体]java搜索时错误");
            }
        }
    }
}