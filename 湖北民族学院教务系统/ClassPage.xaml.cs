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
    public sealed partial class ClassPage : Page
    {
        public ClassPage()
        {
            Initial();
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        private HtmlDocument doc = new HtmlDocument();
        private static ObservableCollection<Classes> list1 = new ObservableCollection<Classes>();
        private static ObservableCollection<Classes> list2 = new ObservableCollection<Classes>();
        private static ObservableCollection<Classes> list3 = new ObservableCollection<Classes>();
        private static ObservableCollection<Classes> list4 = new ObservableCollection<Classes>();
        private static ObservableCollection<Classes> list5 = new ObservableCollection<Classes>();
        private static ObservableCollection<Classes> list6 = new ObservableCollection<Classes>();
        private static ObservableCollection<Classes> list7 = new ObservableCollection<Classes>();

        private static string url = "https://jwxt.hbmy.edu.cn:8099/edu/studentScheduleAction!getScheduleTable.action";
        public async void Initial()
        {
            List<Classes> classLists = new List<Classes>();
            string html = await HttpHelper.GetHtml(url);
            try
            {
                classLists = HtmlDocHelper.GetClass(html, doc);
            }
            catch
            {

            }
            foreach (Classes item in classLists)
            {
                switch(item.Cday)
                {
                    case 1:
                        list1.Add(item);
                        break;
                    case 2:
                        list2.Add(item);
                        break;
                    case 3:
                        list3.Add(item);
                        break;
                    case 4:
                        list4.Add(item);
                        break;
                    case 5:
                        list5.Add(item);
                        break;
                    case 6:
                        list6.Add(item);
                        break;
                    case 7:
                        list7.Add(item);
                        break;
                    default:
                        break;
                }
            }
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/no_class.png"));
            brush.Stretch = Stretch.Uniform;
            listCls1.ItemsSource = list1;
            listCls2.ItemsSource = list2;
            listCls3.ItemsSource = list3;
            listCls4.ItemsSource = list4;
            listCls5.ItemsSource = list5;
            listCls6.ItemsSource = list6;
            listCls7.ItemsSource = list7;
            if (list1.Count==0)
            {
                listCls1.Background = brush;
                
            }
            if (list2.Count== 0)
            {
                listCls2.Background =brush;
                
            }
            if (list3.Count == 0)
            {
                listCls3.Background = brush;
                
            }
            if (list4.Count == 0)
            {
                listCls4.Background = brush;
                
            }
            if (list5.Count == 0)
            {
                listCls5.Background = brush;
            }
            if (list6.Count== 0)
            {
                listCls6.Background = brush;
            }
            if (list7.Count == 0)
            {
                listCls7.Background = brush;
            }
            
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            switch(Grid.GetColumn(sender as Button))
            {
                case 0:
                    pivotClass.SelectedIndex = 0;
                    Grid.SetColumn(rec, 0);
                    break;
                case 1:
                    pivotClass.SelectedIndex = 1;
                    Grid.SetColumn(rec, 1);
                    break;
                case 2:
                    pivotClass.SelectedIndex = 2;
                    Grid.SetColumn(rec, 2);
                    break;
                case 3:
                    pivotClass.SelectedIndex = 3;
                    Grid.SetColumn(rec, 3);
                    break;
                case 4:
                    pivotClass.SelectedIndex = 4;
                    Grid.SetColumn(rec, 4);
                    break;
                case 5:
                    pivotClass.SelectedIndex = 5;
                    Grid.SetColumn(rec, 5);
                    break;
                case 6:
                    pivotClass.SelectedIndex = 6;
                    Grid.SetColumn(rec, 6);
                    break;
            }
        }

        private void pivotClass_Loaded(object sender, RoutedEventArgs e)
        {
            int  a= Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"));
            if (a==0)
            {
                a = 7;
            }
            pivotClass.SelectedIndex = a - 1;
        }

        private void pivotClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grid.SetColumn(rec, (sender as Pivot).SelectedIndex);
        }
    }
}
