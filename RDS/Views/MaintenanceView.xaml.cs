using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;

namespace RDS.Views
{
	/// <summary>
	/// MaintenanceView.xaml 的交互逻辑
	/// </summary>
	public partial class MaintenanceView : UserControl,IExitView
    {
        Action IExitView.ExitView { get; set; }
    
        public MaintenanceView()
        {
            InitializeComponent();
         
        }

        private void Button_ExitView_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }

        private void Button_WizardNext_Click_1(object sender, RoutedEventArgs e)
        {
            this.TabControl_MaintenanceWizard.SelectedIndex++;
            if (this.TabControl_MaintenanceWizard.SelectedIndex == 3) this.Content = new FinalView();
        }

        private void Button_WizardPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (this.TabControl_MaintenanceWizard.SelectedIndex > 0) this.TabControl_MaintenanceWizard.SelectedIndex--;
        }
    }
}
