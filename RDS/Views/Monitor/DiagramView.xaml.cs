using System;
using System.Windows.Controls;
using System.Windows.Input;
using RDS.ViewModels.Common;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// DiagramView.xaml 的交互逻辑
	/// </summary>
	public partial class DiagramView : UserControl, IExitView
    {
        Action IExitView.ExitView { get; set; }
        public DiagramView()
        {
            InitializeComponent();
        }

        private void Label_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {
            ((IExitView)this).ExitView();
        } 
    }
}
