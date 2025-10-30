using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Components.Parser;
using MinecraftLaunch.Utilities;
using Panuon.WPF.UI;
using System.Diagnostics;
using System.Windows;

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
            Console.WriteLine($"🐾 进程工作集内存: {processMemoryMb} MB");
            Console.WriteLine($"🐾 进程私有内存: {privateMemoryMb} MB");
            Console.WriteLine($"🐾 GC总内存: {GC.GetTotalMemory(false) / 1024 / 1024} MB");
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxX.Show("别急,快速打开是不可能的,要想快就捐100000万!!!!!!!(bushi),没给白子充钱导致的(√)");
            Launch launch = new();
            _ = launch.ALaunch();
    }

        private void JavaButton_CilckClick(object sender, RoutedEventArgs e)
        {
            MessageBoxX.Show("别急,等一下");
            _ = GetJavaVersions();
        }

        private void VerButton_CilckClick(object sender, RoutedEventArgs e)
        {
            MessageBoxX.Show("别急,等一下");
            MinecraftParser minecraftParser = ".\\.minecraft";
            List<MinecraftEntry> VerList = minecraftParser.GetMinecrafts();
            VerCombo.DisplayMemberPath = "Id";
            VerCombo.SelectedValuePath = "Id";
            VerCombo.ItemsSource = VerList;
        }

        private void RamTestButton_CilckClick(object sender, RoutedEventArgs e)
        {
            CheckMemoryStatus();
        }
    }
}
