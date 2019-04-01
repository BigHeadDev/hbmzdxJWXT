using HtmlAgilityPack;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using 湖北民族学院教务系统.Model;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    { 
        public MainPage()
        {
            this.InitializeComponent();
            Initial();
        }
        public OneWord one = new OneWord();
        private static bool b = true;
        private UserInfo user = new UserInfo();
        private HtmlDocument doc = new HtmlDocument();
        private string url = "https://jwxt.hbmy.edu.cn:8099/edu/gradeTestStudent!getStudentTest.action";
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                this.one = e.Parameter as OneWord;
            }
            catch
            {
            }
            finally
            {
                if (one==null)
                {
                    b = false;
                }
            }
        }
        private void StackPanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var stack = (sender as StackPanel);
            string name = stack.Name;
            switch (name)
            {
                case "成绩查询":
                    this.Frame.Navigate(typeof(MarkPage));
                    break;
                case "考试安排":
                    this.Frame.Navigate(typeof(ExamPage));
                    break;
                case "课表查询":
                    this.Frame.Navigate(typeof(ClassPage));
                    break;
                case "创新学分":
                    this.Frame.Navigate(typeof(InnovatePage));
                    break;
                case "通知公告":
                    this.Frame.Navigate(typeof(AnnouncePage));
                    break;
                case "教学评价":
                    this.Frame.Navigate(typeof(CommentPage));
                    break;
                case "圈存服务":
                    if (user.Uidnum=="test")
                    {
                        new myNotification("暂未开启").show();
                        return;
                    }
                    this.Frame.Navigate(typeof(CardPage),user.Uidnum);
                    break;
                case "关于软件":
                    this.Frame.Navigate(typeof(AboutPage));
                    break;
                default:
                    break;
            }
        }
        public async void Initial()
        {
            string html =await HttpHelper.GetHtml(url);
            try
            {
                user = HtmlDocHelper.GetUserInfo(html, doc);
            }
            catch
            {
                user.Uclass = "test";
                user.Ucol = "test";
                user.Uidnum = "test";
                user.Uimage = "test";
                user.Umajor = "test";
                user.Uname = "test";
                user.Unum = "test";
                user.Usex = "test";
            }
            if (b)
            {
                rpImage.Visibility = Visibility.Visible;
            }
        }
        private void Center_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserCenter),user);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            b = false;
            rpImage.Visibility = Visibility.Collapsed;
        }
    }

}

