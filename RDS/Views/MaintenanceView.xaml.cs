using System;
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
            if (this.TabControl_MaintenanceWizard.SelectedIndex == 2) this.Content = new FinalView();
        }

        private void Button_WizardPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (this.TabControl_MaintenanceWizard.SelectedIndex > 0) this.TabControl_MaintenanceWizard.SelectedIndex--;
        }
    }
}
