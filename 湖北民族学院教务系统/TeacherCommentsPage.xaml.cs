using HtmlAgilityPack;
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
using 湖北民族学院教务系统.Model;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 湖北民族学院教务系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TeacherCommentsPage : Page
    {
        public  Comments comments = new Comments();
        public static HtmlDocument doc = new HtmlDocument();
        private static evaluationCourse evaluationCourses = new evaluationCourse();
        string url = "https://jwxt.hbmy.edu.cn:8099/edu/teachingEvaluation!courseEvaluate.action";
        public TeacherCommentsPage()
        {
            this.InitializeComponent();
            for (int i = 0; i <=30; i++)
            {
                if (i<=20)
                {
                    cbox1.Items.Add(i);
                }
                if (i<=25)
                {
                    cbox2.Items.Add(i);
                    cbox3.Items.Add(i);
                }
                if (i<=30)
                {
                    cbox4.Items.Add(i);
                }
            }
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.comments = e.Parameter as Comments;
            string html = await HttpHelper.GetHtml("https://jwxt.hbmy.edu.cn:8099/edu/" + comments.Curl);
            evaluationCourses = HtmlDocHelper.GetEvaluationCourse(html, doc);
        }
        private async void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            //判断部分
            if (cbox1.SelectedIndex==-1||cbox2.SelectedIndex==-1 || cbox3.SelectedIndex == -1 || cbox4.SelectedIndex == -1)
            {
                new myNotification("请填写完整的分数!").show();
                return;
            }
            //提交部分
            ContentDialog contentDialog = new ContentDialog
            {
                Content="你评价的总分是:"+(Convert.ToInt32(cbox1.SelectedValue.ToString())+ Convert.ToInt32(cbox2.SelectedValue.ToString()) + Convert.ToInt32(cbox3.SelectedValue.ToString()) + Convert.ToInt32(cbox4.SelectedValue.ToString()))+" 确认提交吗?",
                PrimaryButtonText="提交",
                IsPrimaryButtonEnabled=true,
                SecondaryButtonText="取消",
                IsSecondaryButtonEnabled=true
            };
            contentDialog.PrimaryButtonClick += SubmitScore;
            await contentDialog.ShowAsync();
        }

        private async void SubmitScore(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("score", cbox1.SelectedValue.ToString()));
            list.Add(new KeyValuePair<string, string>("score", cbox2.SelectedValue.ToString()));
            list.Add(new KeyValuePair<string, string>("score", cbox3.SelectedValue.ToString()));
            list.Add(new KeyValuePair<string, string>("score", cbox4.SelectedValue.ToString()));
            list.Add(new KeyValuePair<string, string>("suggestion", txtSuggest.Text));
            list.Add(new KeyValuePair<string, string>("evaluationCourse.id", evaluationCourses.id));
            list.Add(new KeyValuePair<string, string>("evaluationCourse.teacherId", evaluationCourses.teacherId));
            list.Add(new KeyValuePair<string, string>("evaluationCourse.studentId", evaluationCourses.studentId));
            list.Add(new KeyValuePair<string, string>("evaluationCourse.tcId", evaluationCourses.tcId));
            list.Add(new KeyValuePair<string, string>("evaluationCourse.evaluationCourseType", evaluationCourses.evaluationCourseType));
            list.Add(new KeyValuePair<string, string>("evaluationCourse.evaluatorType", evaluationCourses.evaluatorType));
            string result = await HttpHelper.GetHtmlWithPrams(url, list);
            if (result.Contains("成功"))
            {
                new myNotification("对" + comments.Cteacher + "老师评教成功!", new TimeSpan(0, 0, 3)).show();
                this.Frame.Navigate(typeof(CommentPage));
            }
            else
            {
                new myNotification("对" + comments.Cteacher + "老师评教失败,请重试!", new TimeSpan(0, 0, 3)).show();
                return;
            }
        }
    }
}
