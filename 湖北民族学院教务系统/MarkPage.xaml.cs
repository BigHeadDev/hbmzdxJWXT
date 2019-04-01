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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using 湖北民族学院教务系统.Model;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MarkPage : Page
    {
        public MarkPage()
        {
            this.InitializeComponent();
            Initial();
        }
        List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        private ObservableCollection<ComboxTerm> terms = new ObservableCollection<ComboxTerm>();
        private ObservableCollection<ComboxYear> years = new ObservableCollection<ComboxYear>();
        private HtmlDocument doc = new HtmlDocument();
        private static ObservableCollection<Mark> marklists = new ObservableCollection<Mark>();
        private static ObservableCollection<RankTest> ranktestlists = new ObservableCollection<RankTest>();
        private static string Gradeurl = "https://jwxt.hbmy.edu.cn:8099/edu/gradeEnteringStudent!getAllGradeEnter.action";
        private static string Rankurl = "https://jwxt.hbmy.edu.cn:8099/edu/gradeTestStudent!getStudentTest.action";
        public void Initial()
        {
            ComboxIntial();
            RankTestInitial();
        }
        public void ComboxIntial()
        {
            terms.Add(new ComboxTerm("春季", 0));
            terms.Add(new ComboxTerm("秋季", 1));
            int thisyear = DateTime.Now.Year;
            for (int i = thisyear; i >= thisyear-4; i--)
            {
                years.Add(new ComboxYear(i.ToString()));
            }
            cboxY.ItemsSource = years;
            cboxT.ItemsSource = terms;
            if (DateTime.Now.Month<9&& DateTime.Now.Month>2)
            {
                cboxT.SelectedIndex = 0;
            }
            else
            {
                cboxT.SelectedIndex = 1;
            }
            cboxY.SelectedIndex =1;
        }
        private async void cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboxT.SelectedIndex==-1||cboxY.SelectedIndex==-1)
            {
                return;
            }
            list.Clear();
            list.Add(new KeyValuePair<string, string>("configData.openYear", cboxY.SelectedValue.ToString()));
            list.Add(new KeyValuePair<string, string>("configData.openTerm", cboxT.SelectedValue.ToString()));
            list.Add(new KeyValuePair<string, string>("Submit", "查 询"));
            try
            {
                string html = await HttpHelper.GetHtmlWithPrams(Gradeurl, list);
                marklists = HtmlDocHelper.GetMarks(html, doc);
                if (marklists.Count == 0)
                {
                    ImageBrush brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/no_data.png"));
                    brush.Stretch = Stretch.Uniform;
                    listMark.Background = brush;
                }
            }
            catch
            {

            }
            listMark.ItemsSource = marklists;
        }
        private async void RankTestInitial()
        {
            string html = await HttpHelper.GetHtml(Rankurl);
            try
            {
                ranktestlists = HtmlDocHelper.GetRankTest(html, doc);
            }
            catch
            {

            }
            listRank.ItemsSource = ranktestlists;
        }
    }
}
