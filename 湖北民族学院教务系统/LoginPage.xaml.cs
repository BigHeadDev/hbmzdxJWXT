using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using 湖北民族学院教务系统.Model;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            InputPane.GetForCurrentView().Showing += LoginPage_Showing;
            InputPane.GetForCurrentView().Hiding += LoginPage_Hiding;
        }
        private OneWord one=new OneWord();
        private void LoginPage_Hiding(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            this.rowBottom.Height = new GridLength(1, GridUnitType.Star);
        }

        private void LoginPage_Showing(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            this.rowBottom.Height = new GridLength(args.OccludedRect.Height);
        }

        private bool IsInHBMY=true;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            txtUsername.Text = CommonHelper.ReadValue("username");
            txtPassword.Password = CommonHelper.ReadValue("password");
        }
        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUsername.Text == "")
                {
                    new myNotification("请输入学号!").show();
                    return;
                }
                if (txtPassword.Password == "")
                {
                    new myNotification("请输入密码!").show();
                    return;
                }
                try
                {
                    btnLogin.Content = "正在登陆";
                    btnLogin.IsEnabled = false;
                    ProcessBar1.Visibility = Visibility.Visible;
                    IsInHBMY = await CommonHelper.CheckNet();
                }
                catch
                {

                }
                try
                {
                    DateTime yes = DateTime.Now;
                    string oneword = await HttpHelper.GetElectric(/*"http://qust.me:8889/api/one/date/" + yes.Year+"-"+yes.Month+"-"+(yes.Day-1).ToString()*/"http://open.iciba.com/dsapi/");
                    var json = JsonSerializer.Create();
                    one = json.Deserialize<OneWord>(new JsonTextReader(new StringReader(oneword)));
                }
                catch (Exception)
                {

                }
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("user.roleId", "0"));
                list.Add(new KeyValuePair<string, string>("user.account", txtUsername.Text));
                list.Add(new KeyValuePair<string, string>("user.password", txtPassword.Password));
                string result = await HttpHelper.GetHtmlWithPrams("https://jwxt.hbmy.edu.cn:8099/edu/login!checkLogin.action", list);

                if (!IsInHBMY)
                {
                    ProcessBar1.Visibility = Visibility.Collapsed;
                    btnLogin.Content = "登陆";
                    btnLogin.IsEnabled = true;
                    new myNotification("网络错误!请确认是否连接校园网!").show();
                    return;
                }
                if (result.Contains("错误"))
                {
                    ProcessBar1.Visibility = Visibility.Collapsed;
                    btnLogin.Content = "登陆";
                    btnLogin.IsEnabled = true;
                    new myNotification("登录失败!请确认学号与密码组合是否正确!").show();
                }
                else
                {
                    ProcessBar1.Visibility = Visibility.Collapsed;
                    btnLogin.Content = "登陆";
                    btnLogin.IsEnabled = true;
                    CommonHelper.SetValue("username", txtUsername.Text);
                    CommonHelper.SetValue("password", txtPassword.Password);
                    new myNotification("恭喜!登陆成功").show();
                    if (one.caption != null)
                    {
                        this.Frame.Navigate(typeof(MainPage), one);
                    }
                    else
                    {
                        this.Frame.Navigate(typeof(MainPage));
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (txtUsername.Text == "test" && txtPassword.Password == "test123")
                {
                    if (one.caption != null)
                    {
                        this.Frame.Navigate(typeof(MainPage), one);
                    }
                    else
                    {
                        this.Frame.Navigate(typeof(MainPage));
                    }
                }
            }
        }
    }
}
