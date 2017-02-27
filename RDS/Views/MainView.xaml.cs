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
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        private TaskView taskView;

        private object PreviousContent;

        public MainView()
        {
            InitializeComponent();
            this.taskView = new TaskView();
            this.ContentControl_CurrentContent.Content = this.taskView;
            this.PreviousContent = this.ContentControl_CurrentContent.Content;
        }

        private void Button_HistroyData_Click(object sender, RoutedEventArgs e)
        {
            this.ContentControl_CurrentContent.Content = new HistroyView();
        }

        private void Button_CurrentTask_Click(object sender, RoutedEventArgs e)
        {
            this.ContentControl_CurrentContent.Content = this.taskView;
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            this.ContentControl_CurrentContent.Content = new HelpView();
        }
    }
}
