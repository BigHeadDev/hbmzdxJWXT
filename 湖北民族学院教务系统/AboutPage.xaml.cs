using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (e.ClickedItem.ToString())
            {
                case "去评分":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?productid=9PHBZCH1NQD4"));
                    break;
                case "反馈与帮助":
                    this.Frame.Navigate(typeof(FeedBackPage));
                    break;
                case "隐私政策":
                    this.Frame.Navigate(typeof(AppIntroduce));
                    break;
                default:
                    break;
            }
        }
    }
}
