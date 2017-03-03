using System.Windows;
using System.Windows.Controls;

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
