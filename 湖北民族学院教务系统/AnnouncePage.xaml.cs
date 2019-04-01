using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using 湖北民族学院教务系统.Model;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AnnouncePage : Page
    {
        public AnnouncePage()
        {
            Initial();
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        private HtmlDocument doc = new HtmlDocument();
        private static ObservableCollection<Announce> annlists = new ObservableCollection<Announce>();
        private static string url = "https://jwxt.hbmy.edu.cn:8099/edu/notice!getStuNotices.action";
        public async void Initial()
        {
            string html = await HttpHelper.GetHtml(url);
            try
            {
                annlists = HtmlDocHelper.GetAnnounce(html, doc);
            }
            catch
            {

            }
            foreach (Announce item in annlists)
            {
               item.Aarticle= HtmlDocHelper.GetArticle(await HttpHelper.GetHtml(item.Aurl), doc);
            }
            listAnnounce.ItemsSource = annlists;
        }

        private void listAnnounce_ItemClick(object sender, ItemClickEventArgs e)
        {
            Announce ann = e.ClickedItem as Announce;
            this.Frame.Navigate(typeof(ArticalContent),ann);
        }
    }
}
