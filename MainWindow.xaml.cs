using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Components.Authenticator;
using MinecraftLaunch.Components.Parser;
using MinecraftLaunch.Extensions;
using MinecraftLaunch.Launch;
using MinecraftLaunch.Utilities;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
using MCLaunch.MyClass;
using Panuon.WPF.UI;
using Serilog;

namespace MCLaunch
{


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
                Log.Information("[Start]开始搜索Java版本");
                await foreach (var java in asyncJavas)
                {
                    Log.Information("{JavaEntry}\n", java);
                }
            }
            catch(Exception ex)
            {
                MessageBoxX.Show(ex.Message, "哎呀~出了一点问题~~~主人不要介意嘛~~~~~~~", MessageBoxButton.OK, MessageBoxIcon.Error);
                Log.Error(ex,"[窗体]java搜索时错误");
            }
        }
    }
}