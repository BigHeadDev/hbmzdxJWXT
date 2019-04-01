using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace 湖北民族学院教务系统.Model
{
    public class CommonHelper
    {
        public static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        /// <summary>
        /// 检查校园网状态
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckNet()
        {
            string result=await HttpHelper.GetHtml("https://jwxt.hbmy.edu.cn:8099/");
            if (result=="0")
            {
                return false;
            }
            else
            {
                HttpHelper.GetCookie();
                return true;
            }
        }
        /// <summary>
        /// 土司通知
        /// </summary>
        /// <param name="assetsImageFileName"></param>
        /// <param name="text"></param>
        public static void ShowToastNotification(/*string assetsImageFileName, */string text)
        {
            // 1. create element
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            // 2. provide text
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(text));

            // 3. provide image
            //XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("src", $"ms-appx:///Assets/{assetsImageFileName}");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "logo");

            // 4. duration
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "short");

            // 5. audio
            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", $"ms-winsoundevent:Notification.Default");
            toastNode.AppendChild(audio);

            // 6. app launch parameter
            //((XmlElement)toastNode).SetAttribute("launch", "{\"type\":\"toast\",\"param1\":\"12345\",\"param2\":\"67890\"}");

            // 7. send toast
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
        /// <summary>
        /// 设置读取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadValue(string key)
        {
            if (localSettings.Values.ContainsKey(key))
            {
                return localSettings.Values[key].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 设置更新
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(string key,string value)
        {
            localSettings.Values[key] = value;
        }

    }
}
