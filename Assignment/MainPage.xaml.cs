﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AssignmentUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            collection = new ObservableCollection<RootObject>();
            this.DataContext = this;
        }

        public ObservableCollection<RootObject> collection { get; set; }

        private static string RemoveHtmlTag(string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<RootObject> data = await APIManager.GetData();

            for (int i = 0; i < data.Count; i++)
            {
                data[i].content.rendered = RemoveHtmlTag(data[i].content.rendered);
                collection.Add(data[i]);
            }
            APIGridView.ItemsSource = collection;
        }
    }
}