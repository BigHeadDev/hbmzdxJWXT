using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace 湖北民族学院教务系统.Model
{
    public class HtmlDocHelper
    {
        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Announce> GetAnnounce(string html, HtmlDocument doc)
        {
            ObservableCollection<Announce> lists = new ObservableCollection<Announce>();
            doc.LoadHtml(html);
            HtmlNode table = doc.DocumentNode.SelectSingleNode("//table[@class='select tac']");
            HtmlNodeCollection trs = table.SelectNodes("./tr");
            foreach (HtmlNode items in trs)
            {
                HtmlNodeCollection td = items.SelectNodes("./td");
                if (td!=null)
                {
                    int id = Convert.ToInt32(td[0].InnerText);
                    lists.Add(new Announce
                    {
                        Aid = Convert.ToInt32(td[0].InnerText),
                        Atitle = td[1].InnerText,
                        Aurl = "https://jwxt.hbmy.edu.cn:8099/edu/notice!showNotice.action?notice.id=" + (4 - id) + "&notice.state=2",
                        Aauthor = td[3].InnerText,
                        Atime = td[4].InnerText
                    });
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取通知文章正文
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string GetArticle(string html,HtmlDocument doc)
        {
            doc.LoadHtml(html.Replace("<br/>", "\r\n"));
            HtmlNode font = doc.DocumentNode.SelectSingleNode("//table[@class='select tac']/tr[3]/td/font");
            html = (font.InnerText.Replace("&nbsp;", "")).Replace("\r\n\t    \t", "");
            return html;
        }
        /// <summary>
        /// 获取课程信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<Classes> GetClass(string html,HtmlDocument doc)
        {
            Random random = new Random();
            List<Classes> listsByclass = new List<Classes>();
            List<string> times = new List<string>() { "  上午1-2节", "  上午3-4节", "  下午5-6节", "  下午7-8节", "晚上9-10节" };
            doc.LoadHtml(html);
            HtmlNode tbody= doc.DocumentNode.SelectSingleNode("//tbody");
            HtmlNodeCollection trs = tbody.SelectNodes("./tr");
            for (int i = 0; i < 5; i++)
            {
                HtmlNodeCollection tds = trs[i].SelectNodes("./td");
                for (int j = 0; j < 7; j++)
                {
                    if (tds[j].InnerText!="")
                    {
                        foreach (HtmlNode ps in tds[j].SelectNodes("./p"))
                        {
                            HtmlNodeCollection spans = ps.SelectNodes("./span");
                            listsByclass.Add(new Classes
                            {
                                Ctime = times[i],
                                Cday=j+1,
                                Ccolor= Color.FromArgb(255, (byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)),
                                Cclname =spans[0].InnerText,
                                Cweek=spans[1].InnerText,
                                Croom=spans[2].InnerText.Replace("&nbsp;",""),
                                Cteacher=spans[3].InnerText

                            });
                        }
                    }
                }
            }
            return listsByclass;
        }
        /// <summary>
        /// 获取创新学分
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Innovate> GetInnovates(string html,HtmlDocument doc)
        {
            ObservableCollection<Innovate> lists = new ObservableCollection<Innovate>();
            doc.LoadHtml(html);
            HtmlNode table = doc.DocumentNode.SelectSingleNode("//table[3]");
            HtmlNodeCollection trs = table.SelectNodes("./tr");
            foreach (HtmlNode tr in trs)
            {
                HtmlNodeCollection td = tr.SelectNodes("./td");
                if (td!=null)
                {
                    lists.Add(new Innovate
                    {
                        Iname=td[0].InnerText,
                        Igrade=td[1].InnerText,
                        Istatus=td[2].InnerText.Trim(),
                        Iyear=td[3].InnerText
                    });
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取成绩
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Mark> GetMarks(string html,HtmlDocument doc)
        {
            ObservableCollection<Mark> lists = new ObservableCollection<Mark>();
            doc.LoadHtml(html);
            HtmlNode table = doc.DocumentNode.SelectSingleNode("//table[@class='list_table tac']");
            HtmlNodeCollection trs = table.SelectNodes("./tr");
            foreach (HtmlNode tr in trs)
            {
                HtmlNodeCollection td = tr.SelectNodes("./td");
                if (td!=null)
                {

                    string g = td[2].InnerText;
                    try
                    {
                        lists.Add(new Mark
                        {
                            Mname = td[1].InnerText,
                            Mgrade = g,
                            MisFailed = (Convert.ToDouble(g) < 60 ? "Assets/ic_fail.png" : "Assets/ic_pass.png")
                        });
                    }
                    catch
                    {
                        lists.Add(new Mark
                        {
                            Mname = td[1].InnerText,
                            Mgrade = g,
                            MisFailed = "Assets/ic_no.png"
                        });
                    }
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取等级考试
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<RankTest> GetRankTest(string html,HtmlDocument doc)
        {
            ObservableCollection<RankTest> lists = new ObservableCollection<RankTest>();
            doc.LoadHtml(html);
            HtmlNode table = doc.DocumentNode.SelectSingleNode("//table[2]");
            HtmlNodeCollection trs = table.SelectNodes("./tr");
            foreach (HtmlNode tr in trs)
            {
                HtmlNodeCollection td = tr.SelectNodes("./td");
                if (td!=null)
                {
                    lists.Add(new RankTest
                    {
                        Ryear = td[0].InnerText,
                        Rname = td[1].InnerText,
                        Rstate = td[5].InnerText.Trim(),
                        Rcost = "¥"+td[6].InnerText,
                        Rgrade = td[7].InnerText.Trim()
                    });
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string html,HtmlDocument doc)
        {
            UserInfo user = new UserInfo();
            doc.LoadHtml(html);
            HtmlNode table =doc.DocumentNode.SelectSingleNode("//table[3]");
            //第一个tr
            HtmlNode tr1 = table.SelectSingleNode(".//tr[1]");
            HtmlNodeCollection td1s = tr1.SelectNodes("./td");
            user.Uimage = "https://jwxt.hbmy.edu.cn:8099/" + td1s[0].SelectSingleNode("./img").Attributes[1].Value;
            user.Unum = td1s[1].InnerText.Trim();
            user.Uname = td1s[2].InnerText.Trim();
            user.Usex = td1s[3].InnerText;
            //第二个tr
            HtmlNode tr2 = table.SelectSingleNode(".//tr[2]");
            HtmlNodeCollection td2s = tr2.SelectNodes("./td");
            user.Uidnum = td2s[0].InnerText;
            //第三个tr
            HtmlNode tr3 = table.SelectSingleNode(".//tr[3]");
            HtmlNodeCollection td3s = tr3.SelectNodes("./td");
            user.Ugrade = td3s[1].InnerText;
            user.Uclass = td3s[2].InnerText.Trim();
            //第四个
            HtmlNode tr4 = table.SelectSingleNode(".//tr[4]");
            HtmlNodeCollection td4s = tr4.SelectNodes("./td");
            user.Ucol = td4s[0].InnerText;
            user.Umajor = td4s[1].InnerText.Trim();

            return user;
        }
        /// <summary>
        /// 获取电费信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ElectricInfo GetElectricInfo(string html,HtmlDocument doc)
        {
            ElectricInfo info = new ElectricInfo();
            doc.LoadHtml(html);
            HtmlNode li = doc.DocumentNode.SelectSingleNode(".//li[@class='clearfix']");
            //第1个div
            HtmlNode div2 = li.SelectSingleNode(".//div[2]");
            HtmlNodeCollection spans2 = div2.SelectNodes("./span");
            info.EYue = spans2[1].InnerText;
            //第二个div
            HtmlNode div3 = li.SelectSingleNode(".//div[3]");
            HtmlNodeCollection spans3 = div3.SelectNodes("./span");
            info.EDu = spans3[1].InnerText;
            return info;
        }
        /// <summary>
        /// 获取一卡通余额
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string GetMoney(string html, HtmlDocument doc)
        {
            string yue = "";
            doc.LoadHtml(html);
            HtmlNode td = doc.DocumentNode.SelectSingleNode(".//table[@id='T1']/tr/td");
            yue = td.InnerText.Split(new char[] { '（', '元' })[2].Replace("卡余额:", "").Trim();
            return "¥" + yue;
        }
        /// <summary>
        /// 获取一卡通操作记录
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string GetOperations(string html,HtmlDocument doc)
        {
            doc.LoadHtml(html);
            HtmlNode tr = doc.DocumentNode.SelectSingleNode(".//table[@id='T1']/tr");
            string s = tr.InnerHtml.Replace("<p><img src='images_card/fenge.png' style='width: 90%;' border='0' '/=\"\"></p>", "");
            return s;
        }
        /// <summary>
        /// 获取教评列表
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Comments> GetComments(string html,HtmlDocument doc)
        {
            ObservableCollection<Comments> list = new ObservableCollection<Comments>();
            doc.LoadHtml(html);
            HtmlNode tbody = doc.DocumentNode.SelectSingleNode("//table[@class='list_table list_table_nohover tal']/tbody");
            HtmlNodeCollection trs = tbody.SelectNodes("./tr");
            foreach (HtmlNode items in trs)
            {
                HtmlNodeCollection td = items.SelectNodes("./td");
                if (td != null)
                {
                    string url = "";
                    if (td[3].HasChildNodes)
                    {
                        url = td[3].SelectSingleNode("./a").Attributes[3].Value;
                    }
                    else
                    {
                        url = "";
                    }
                    list.Add(new Comments
                    {
                        Cteacher = td[0].InnerText,
                        CclassNum = td[1].InnerText.Split(new char[] { '(' })[0],
                        Cstatus = td[2].SelectSingleNode("./span").InnerText,
                        Curl = url,
                        CColor= td[2].SelectSingleNode("./span").InnerText=="未评教"? new SolidColorBrush(Colors.Red):new SolidColorBrush(Colors.Green)
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 获取单个课程评分
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static evaluationCourse GetEvaluationCourse(string html,HtmlDocument doc)
        {
            evaluationCourse evaluation = new evaluationCourse();
            doc.LoadHtml(html);
            HtmlNode td = doc.DocumentNode.SelectSingleNode("//td[@class='bakfff']");
            HtmlNodeCollection inputs = td.SelectNodes("./input");
            evaluation.id = inputs[0].Attributes[2].Value;
            evaluation.teacherId = inputs[1].Attributes[2].Value;
            evaluation.studentId = inputs[2].Attributes[2].Value;
            evaluation.tcId = inputs[3].Attributes[2].Value;
            evaluation.evaluationCourseType = inputs[4].Attributes[2].Value;
            evaluation.evaluatorType = inputs[5].Attributes[2].Value;
            return evaluation;
        }
        /// <summary>
        /// 获取考试安排
        /// </summary>
        /// <param name="html"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Exam> GetExma(string html,HtmlDocument doc)
        {
            ObservableCollection<Exam> list = new ObservableCollection<Exam>();
            doc.LoadHtml(html);
            HtmlNodeCollection tables =doc.DocumentNode.SelectNodes("//table[@class='list_table tac']");
            HtmlNodeCollection trs = tables[1].SelectNodes("./tr");
            foreach (HtmlNode items in trs)
            {
                HtmlNodeCollection td = items.SelectNodes("./td");
                if (td != null)
                {
                    list.Add(new Exam
                    {
                        EClassName = td[1].InnerText,
                        ETime = td[2].InnerText.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", ""),
                        EExamRoom = td[3].InnerText
                    });
                }
            }
            return list;
        }
    }
}
