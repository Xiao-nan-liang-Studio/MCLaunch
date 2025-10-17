using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Components.Parser;
using MinecraftLaunch.Utilities;
using Panuon.WPF.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCLaunch.Views.Pages
{
    /// <summary>
    /// Go.xaml 的交互逻辑
    /// </summary>
    public partial class Go : Page
    {
        private void CheckMemoryStatus()
        {
            var process = Process.GetCurrentProcess();

            // 当前进程内存使用
            long processMemoryMB = process.WorkingSet64 / 1024 / 1024;
            long privateMemoryMB = process.PrivateMemorySize64 / 1024 / 1024;

            Debug.WriteLine($"🐾 进程工作集内存: {processMemoryMB} MB");
            Debug.WriteLine($"🐾 进程私有内存: {privateMemoryMB} MB");
            Debug.WriteLine($"🐾 GC总内存: {GC.GetTotalMemory(false) / 1024 / 1024} MB");
        }
        async private void GetJavaVersions()
        {


            var asyncJavas = JavaUtil.EnumerableJavaAsync();
            await foreach (var java in asyncJavas)
            {
                Debug.WriteLine(java);
                JavaCombo.Items.Add(java);
            }


        }
        public Go()
        {
            InitializeComponent();



            MinecraftParser minecraftParser = ".\\.minecraft";
            Init.AInit();
            List<MinecraftEntry> Minelist;
            Minelist = minecraftParser.GetMinecrafts();
            CheckMemoryStatus();
            GetJavaVersions();

            VerCombo.DisplayMemberPath = "Id";
            VerCombo.SelectedValuePath = "Id";
            VerCombo.ItemsSource = Minelist;

        }
        private static readonly Dictionary<Type, Page> bufferedPages =
       new Dictionary<Type, Page>();

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxX.Show("别急,快速打开是不可能的,要想快就捐100000万!!!!!!!(bushi),没给白子充钱导致的(√)");
            //Launch launch = new();
            //launch.ALaunch();
        }


    }
}
