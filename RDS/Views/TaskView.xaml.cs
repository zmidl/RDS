using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using System.Data;

namespace RDS.Views
{
	/// <summary>
	/// TaskView.xaml 的交互逻辑
	/// </summary>
	public partial class TaskView : UserControl
    {
       private object PreviousContent;

        public TaskView()
        {
            InitializeComponent();
            this.PreviousContent = this.Content;
        }

        private void Button_NewExperiment_Click(object sender, RoutedEventArgs e)
        {
            General.ExitView(this.PreviousContent, this, ((IExitView)new PrecheckView()));
        }

        private void Button_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            General.ExitView(this.PreviousContent, this, ((IExitView)new MaintenanceView()));
        }
    }
}
