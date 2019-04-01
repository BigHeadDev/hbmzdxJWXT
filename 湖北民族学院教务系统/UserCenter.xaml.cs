using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using 湖北民族学院教务系统.Model;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UserCenter : Page
    {
        public UserCenter()
        {
            this.InitializeComponent();
        }
        public UserInfo user { get; set; } = new UserInfo();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                this.user = e.Parameter as UserInfo;
            }
            catch
            {

            }
        }

        private async void LoginOut_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog content = new ContentDialog()
            {
                Content = "确认要切换账号吗",
                IsPrimaryButtonEnabled = true,
                PrimaryButtonText = "切换",
                IsSecondaryButtonEnabled = true,
                SecondaryButtonText = "取消",
            };
            content.PrimaryButtonClick += LoginOut;
            await content.ShowAsync();
        }

        private void LoginOut(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
