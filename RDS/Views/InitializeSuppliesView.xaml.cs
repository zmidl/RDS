﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RDS.Apps.Common;

namespace RDS.Views
{
    /// <summary>
    /// InitializeSuppliesView.xaml 的交互逻辑
    /// </summary>
    public partial class InitializeSuppliesView : UserControl,IExitView
    {
        Action IExitView.ExitView { get; set; }
        public InitializeSuppliesView()
        {
            InitializeComponent();
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckBox_First.IsChecked == true &&
             this.CheckBox_Second.IsChecked == true &&
             this.CheckBox_Third.IsChecked == true &&
             this.CheckBox_Fourth.IsChecked == true) this.Content = new Monitor.MonitorView();
        }
    }
}
