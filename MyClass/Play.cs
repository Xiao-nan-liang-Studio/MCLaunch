namespace MCLaunch.MyClass;
using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Components.Authenticator;
using MinecraftLaunch.Components.Downloader;
using MinecraftLaunch.Components.Installer;
using MinecraftLaunch.Components.Parser;
using MinecraftLaunch.Extensions;
using MinecraftLaunch.Launch;
using MinecraftLaunch.Utilities;
using System.Diagnostics;
using Panuon.WPF.UI;

public class Play
{
    public async Task ALaunch()
        {

            var sw = Stopwatch.StartNew();

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
}