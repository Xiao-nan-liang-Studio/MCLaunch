using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Components.Parser;
using MinecraftLaunch.Utilities;
using Panuon.WPF.UI;
using System.Diagnostics;
using System.Windows;
using Serilog;

namespace MCLaunch.Views.Pages
{
    /// <summary>
    /// Go.xaml 的交互逻辑
    /// </summary>
    public partial class Go
    {
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
            try{
                Log.Information("[主界面]按下启动按钮,开始启动");
                MessageBoxX.Show("别急,快速打开是不可能的,要想快就捐100000万!!!!!!!(bushi),没给白子充钱导致的(√)");
                Log.Information("弹出提示框,内容: 别急,快速打开是不可能的,要想快就捐100000万!!!!!!!(bushi),没给白子充钱导致的(√)");
                Launch launch = new();
                await launch.login();
                await launch.ALaunch();
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
