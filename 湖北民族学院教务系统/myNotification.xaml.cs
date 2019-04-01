﻿using System;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace 湖北民族学院教务系统
{
    public sealed partial class myNotification : UserControl
    {
        private string content;
        private TimeSpan showTime;
        private Popup popup;
        private myNotification()
        {
            this.InitializeComponent();
            this.popup = new Popup();
            this.Width = Window.Current.Bounds.Width;
            this.Height = Window.Current.Bounds.Height;
            popup.Child = this;
            this.Loaded += Notification_Loaded;
            this.Unloaded += Notification_Unloaded;
        }
        public myNotification(string content, TimeSpan showTime) : this()
        {
            this.content = content;
            this.showTime = showTime;
        }
        public myNotification(string content) : this(content, TimeSpan.FromSeconds(1))
        {

        }
        public void show()
        {
            this.popup.IsOpen = true;
        }
        private void Notification_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Current_SizeChanged;
        }

        private void Notification_Loaded(object sender, RoutedEventArgs e)
        {
            NotificationContent.Text = this.content;
            this.Notification.BeginTime = this.showTime;
            this.Notification.Begin();
            this.Notification.Completed += Notification_Completed;
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.Width = e.Size.Width;
            this.Height = e.Size.Height;
        }

        private void Notification_Completed(object sender, object e)
        {
            this.popup.IsOpen = false;
        }
    }
}
