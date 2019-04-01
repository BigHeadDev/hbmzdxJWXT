using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class CommentPage : Page
    {
        public CommentPage()
        {
            this.InitializeComponent();
            Initial();
        }
        private HtmlDocument doc = new HtmlDocument();
        private static string url = "https://jwxt.hbmy.edu.cn:8099/edu/teachingEvaluation!getTeachingEvaluation.action";
        private static ObservableCollection<Comments> list = new ObservableCollection<Comments>();
        public async void Initial()
        {
            string html = await HttpHelper.GetHtml(url);
            try
            {
                list = HtmlDocHelper.GetComments(html, doc);
            }
            catch
            {
                //无数据
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/no_open.png"));
                brush.Stretch = Stretch.Uniform;
                listComments.Background = brush;
            }
            listComments.ItemsSource = list;
        }

        private void listComments_ItemClick(object sender, ItemClickEventArgs e)
        {
            Comments comments = e.ClickedItem as Comments;
            if (comments.Cstatus=="未评教")
            {
                this.Frame.Navigate(typeof(TeacherCommentsPage),comments);
            }
            else
            {
                new myNotification("已经对" + comments.Cteacher + "老师评教过了").show();
                return;
            }
        }
    }
}
