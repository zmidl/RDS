using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// ReagentView.xaml 的交互逻辑
	/// </summary>
	public partial class ReagentView : UserControl, IExitView
    {
        Action IExitView.ExitView { get; set; }
        

        public ReagentView()
        {
            InitializeComponent();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }
    }
}
