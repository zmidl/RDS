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
            General.ExitView(this.PreviousContent, this, ((IExitView)new Monitor.SampleView()));
        }

        private void Button_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            General.ExitView(this.PreviousContent, this, ((IExitView)new MaintenanceView()));
        }
    }
}
