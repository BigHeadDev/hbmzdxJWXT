using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class LoginFlashPage : Page
    {
        public LoginFlashPage()
        {
            this.InitializeComponent();
            StartFlash();
        }
        public async void StartFlash()
        {
            //三张图片
            for (int i = 0; i < 3; i++)
            {
                Image img = LoginGrid.Children[i] as Image;
                while (img.Opacity<1)
                {
                    img.Opacity += 0.1;
                    await Task.Delay(100);
                }
            }
            //logo动画
            var speed = 5.0;
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            int sign = 0;
            timer.Tick += (p1, p2) =>
            {
                speed += 5;
                var current = (double)logo1.GetValue(Canvas.TopProperty);
                if (current >= 200)
                {
                    current = 200;
                    speed = -speed * 0.6;
                    sign++;
                }
                logo1.SetValue(Canvas.TopProperty, current + speed);
                logo2.SetValue(Canvas.TopProperty, current + 70 + speed);
                if (sign == 4)
                {
                    timer.Stop();
                    this.Frame.Navigate(typeof(LoginPage));
                }
            };
            timer.Start();
        }
    }
}
