using HtmlAgilityPack;
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
using 湖北民族学院教务系统.Model;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CardPage : Page
    {
        public CardPage()
        {
            this.InitializeComponent();
        }
        public string IdCardNum = "";
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                this.IdCardNum = e.Parameter.ToString();
            }
            catch
            {

            }
        }
        private HtmlDocument doc = new HtmlDocument();
        private ElectricInfo electricInfo;
        private string yue;
        private async void panel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            switch ((sender as RelativePanel).Name)
            {
                case "pguashi":
                    await CDguashi.ShowAsync();
                    break;
                case "picbc":
                    await CDquancun.ShowAsync();
                    break;
                case "pxiaofei":
                    await CDxiaofei.ShowAsync();
                    break;
                case "pqinshi":
                    await CDqinshi.ShowAsync();
                    //http://mcp.hbmy.edu.cn/mobileapi/electricity/search/index.do?ldh=7&fjh=417&lx=2
                    break;
                case "pyue":
                    await CDyue.ShowAsync();
                    break;
                case "pcaozuo":
                    await CDcaozuo.ShowAsync();
                    break;
                default:
                    break;
            }
        }

        private async void showAsyncs(string msg)
        {
            ContentDialog dialog = new ContentDialog
            {
                Content = msg,
                PrimaryButtonText = "确定"
            };
            await dialog.ShowAsync();
        }
        //挂失方法
        private async Task<bool> Guashi(string pwd)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("userid", tboxGID.Text));
            list.Add(new KeyValuePair<string, string>("password", pwd));
            string html = await HttpHelper.GetCardWithPrams("http://219.138.209.78:9999/YKTAUTO/pages/card/kaguashi", list);
            if (html.Contains("成功"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //圈存方法
        private async Task<bool> Quancun(string pwd)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("userid", tboxQID.Text));
            list.Add(new KeyValuePair<string, string>("money", tboxQmoney.Text));
            list.Add(new KeyValuePair<string, string>("password", pwd));
            string html = await HttpHelper.GetCardWithPrams("http://219.138.209.78:9999/YKTAUTO/pages/card/quancun", list);
            if (html.Contains("成功"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //设置消费方法
        private async Task<bool> Xiaofei(string pwd)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("userid", tboxXID.Text));
            list.Add(new KeyValuePair<string, string>("money", tboxXmoney.Text));
            list.Add(new KeyValuePair<string, string>("password", pwd));
            string html = await HttpHelper.GetCardWithPrams("http://219.138.209.78:9999/YKTAUTO/pages/card/setxiaofei", list);
            if (html.Contains("成功"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //寝室交电费
        private async Task<bool> Qinshi(string pwd)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("userid", tboxQSID.Text));
            list.Add(new KeyValuePair<string, string>("lou", tboxQSloudonghao.Text));
            list.Add(new KeyValuePair<string, string>("qinshi", tboxQSfangjianhao.Text));
            list.Add(new KeyValuePair<string, string>("money", tboxQSmoney.Text));
            list.Add(new KeyValuePair<string, string>("password", pwd));
            string html = await HttpHelper.GetCardWithPrams("http://219.138.209.78:9999/YKTAUTO/pages/card/jiaodianfei", list);
            if (html.Contains("成功"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //查询余额 
        private async Task<string> Yue(string pwd)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("userid", tboxYID.Text));
            list.Add(new KeyValuePair<string, string>("password", pwd));
            string html = await HttpHelper.GetCardWithPrams("http://219.138.209.78:9999/YKTAUTO/pages/card/chaxunyue", list);
            if (html.Contains("成功"))
            {
                return HtmlDocHelper.GetMoney(html,doc);
            }
            else
            {
                return "失败";
            }
        }
        //操作查询
        private async Task<string> Result(string pwd)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("userid", tboxCID.Text));
            list.Add(new KeyValuePair<string, string>("pdate", tboxCdate.Date.Value.ToString("yyyy-MM-dd")));
            list.Add(new KeyValuePair<string, string>("password", pwd));
            string html = await HttpHelper.GetCardWithPrams("http://219.138.209.78:9999/YKTAUTO/pages/card/caozuochaxun", list);
            if (html.Contains("成功"))
            {
                return HtmlDocHelper.GetOperations(html, doc);
            }
            else
            {
                return "失败";
            }
        }
        private async void PasswordBox_PasswordChangd(object sender, RoutedEventArgs e)
        {
            PasswordBox pwdbox = sender as PasswordBox;
            if (pwdbox.Password.Length < 4)
            {
                return;
            }
            switch (pwdbox.Name)
            {
                case "pwdguashi":
                    CDguashi.Hide();
                    if (await Guashi(pwdbox.Password))
                    {
                        pwdbox.Password = "";
                        new myNotification("挂失成功!").show();
                    }
                    else
                    {
                        pwdbox.Password = "";
                        new myNotification("挂失失败!检查身份证或密码组合").show();
                    }
                    break;
                case "pwdquancun":
                    CDquancun.Hide();
                    if (await Quancun(pwdbox.Password))
                    {
                        pwdbox.Password = "";
                        new myNotification("圈存成功!").show();
                    }
                    else
                    {
                        pwdbox.Password = "";
                        new myNotification("圈存失败!检查身份证或密码组合").show();
                    }
                    break;
                case "pwdxiaofei":
                    CDxiaofei.Hide();
                    if (await Xiaofei(pwdbox.Password))
                    {
                        pwdbox.Password = "";
                        new myNotification("设置消费金额成功!").show();
                    }
                    else
                    {
                        pwdbox.Password = "";
                        new myNotification("设置消费金额失败!检查身份证或密码组合").show();
                    }
                    break;
                case "pwdqinshi":
                    CDqinshi.Hide();
                    if (await Qinshi(pwdbox.Password))
                    {
                        pwdbox.Password = "";
                        new myNotification("寝室电费缴费成功!").show();
                    }
                    else
                    {
                        pwdbox.Password = "";
                        new myNotification("寝室电费缴费失败!检查身份证或密码组合").show();
                    }
                    break;
                case "pwdyue":
                    CDyue.Hide();
                    yue = await Yue(pwdbox.Password);
                    if (yue!="失败")
                    {
                        pwdbox.Password = "";
                        tboxCardyue.Text = yue;
                        new myNotification("余额查询成功!").show();
                    }
                    else
                    {
                        pwdbox.Password = "";
                        new myNotification("余额查询失败!检查身份证或密码组合").show();
                    }
                    break;
                case "pwdcaozuo":
                    CDcaozuo.Hide();
                    string result = await Result(pwdbox.Password);
                    if (result!="失败")
                    {
                        pwdbox.Password = "";
                        webResult.NavigateToString(result);
                        await CDresultcaozuo.ShowAsync();
                    }
                    else
                    {
                        pwdbox.Password = "";
                        new myNotification("操作查询失败!检查身份证或密码组合").show();
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 寝室号更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tblqinshi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tboxQSloudonghao.Text == ""||tboxQSfangjianhao.Text=="")
            {
                tboxQSyue.Text = "请输入完整的楼栋号和房间号";
                tboxQSdu.Text= "请输入完整的楼栋号和房间号";
                return;
            }
            string url = "http://mcp.hbmy.edu.cn/mobileapi/electricity/search/index.do?ldh=" + tboxQSloudonghao.Text + "&fjh=" + tboxQSfangjianhao.Text + "&lx=2";
            string html = await HttpHelper.GetElectric(url);
            electricInfo = HtmlDocHelper.GetElectricInfo(html, doc);
            tboxQSyue.Text = electricInfo.EYue;
            tboxQSdu.Text = electricInfo.EDu;
        }
    }
}
