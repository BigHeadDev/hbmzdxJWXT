using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http.Filters;

namespace 湖北民族学院教务系统.Model
{
    class HttpHelper
    {
        private static string cookie = "";
        /// <summary>
        /// 尝试登陆
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async static Task<string> GetHtmlWithPrams(string url, List<KeyValuePair<string, string>> data)
        {
            try
            {
                Windows.Web.Http.HttpClient hc = new Windows.Web.Http.HttpClient();
                hc.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                hc.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.5");
                hc.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
                hc.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                hc.DefaultRequestHeaders.Add("DNT", "1");
                hc.DefaultRequestHeaders.Add("Cookie",cookie);
                var content = new Windows.Web.Http.HttpFormUrlEncodedContent(data);
                var response = await hc.PostAsync(new Uri(url), content);
                var resdata = await response.Content.ReadAsStringAsync();
                return resdata;
            }
            catch
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取一卡通带参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<string> GetCardWithPrams(string url, List<KeyValuePair<string, string>> data)
        {
            try
            {
                Windows.Web.Http.HttpClient hc = new Windows.Web.Http.HttpClient();
                hc.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                hc.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.5");
                hc.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
                hc.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                hc.DefaultRequestHeaders.Add("DNT", "1");
                var content = new Windows.Web.Http.HttpFormUrlEncodedContent(data);
                var response = await hc.PostAsync(new Uri(url), content);
                var resdata = await response.Content.ReadAsStringAsync();
                return resdata;
            }
            catch
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取页面信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async static Task<string> GetHtml(string url)
        {
            try
            {
                var handler = new HttpClientHandler() { UseCookies = false, AutomaticDecompression = DecompressionMethods.GZip };
                HttpClient httpClient = new HttpClient(handler);
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                message.Headers.Add("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                message.Headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.5");
                message.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
                message.Headers.Add("Accept-Encoding", "gzip, deflate");
                message.Headers.Add("DNT", "1");
                message.Headers.Add("Cookie", cookie);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encodingutf8 = Encoding.GetEncoding("utf-8");
                var response = await httpClient.SendAsync(message);
                var resdata = await response.Content.ReadAsStreamAsync();
                StreamReader sr = new StreamReader(resdata, encodingutf8);
                string result = await sr.ReadToEndAsync();
                return result;
            }
            catch
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取网页信息Get
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> GetElectric(string url)
        {
            string strReturn = string.Empty;
            String urlEsc = url;
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(urlEsc);
            req.Method = "GET";
            try
            {
                using (System.Net.WebResponse wr = await req.GetResponseAsync())
                {
                    System.IO.Stream stream = wr.GetResponseStream();
                    byte[] buf = new byte[1024];
                    while (true)
                    {
                        int len = stream.Read(buf, 0, buf.Length);
                        if (len <= 0)//len <= 0 跳出循环
                            break;
                        strReturn += System.Text.Encoding.GetEncoding("utf-8").GetString(buf, 0, len);//指定编码格式
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strReturn;
        }
        /// <summary>
        /// 更新cookie
        /// </summary>
        /// <returns></returns>
        public async static void GetCookie()
        {
            var myClientHandler = new HttpClientHandler();
            myClientHandler.AllowAutoRedirect = false;
            System.Net.Http.HttpClient myClient = new System.Net.Http.HttpClient(myClientHandler);
            HttpResponseMessage response = await myClient.GetAsync("https://jwxt.hbmy.edu.cn:8099/edu/login2.jsp");
            var resourceUri = new Uri("https://jwxt.hbmy.edu.cn:8099/edu/login2.jsp");
            var cookieCollection = myClientHandler.CookieContainer.GetCookies(resourceUri);
            foreach (Cookie item in cookieCollection)
            {
                cookie = item.ToString();
            }
        }

        
    }
}
