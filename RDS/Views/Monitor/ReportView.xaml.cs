using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// ReportView.xaml 的交互逻辑
	/// </summary>
	public partial class ReportView : UserControl,IExitView
    {
        Action IExitView.ExitView { get; set; }

        private object CurrentContent;

        public ReportView()
        {
            InitializeComponent();
            this.CurrentContent = this.Content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }
    }
}
