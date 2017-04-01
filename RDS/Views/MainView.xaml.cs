using System;
using System.Windows;
using System.Windows.Controls;

namespace RDS.Views
{
	/// <summary>
	/// MainView.xaml 的交互逻辑
	/// </summary>
	public partial class MainView : UserControl
    {
		public Action<int> NotifyParentView;  

        private TaskView taskView = new TaskView();

		private HistroyView histroyView = new HistroyView();

		private HelpView helpView = new HelpView();

		private object PreviousContent;

        public MainView()
        {
            InitializeComponent();

			MainWindow.GlobalNotify += MainWindow_GlobalNotify;

            this.ContentControl_CurrentContent.Content = this.taskView;

            this.PreviousContent = this.ContentControl_CurrentContent.Content;
        }

		private void MainWindow_GlobalNotify(object sender, GlobalNotifyArgs e)
		{
			if(e.Index=="Admin")
			{
				this.Canvas_Admin.Visibility = Visibility.Visible;
			}
			//throw new System.NotImplementedException();
		}

		private void Button_HistroyData_Click(object sender, RoutedEventArgs e)
        {
			this.ContentControl_CurrentContent.Content = this.histroyView;
        }

        private void Button_CurrentTask_Click(object sender, RoutedEventArgs e)
        {
            this.ContentControl_CurrentContent.Content = this.taskView;
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
			this.ContentControl_CurrentContent.Content = this.helpView;
        }

		private void Button_Admin_Click(object sender, RoutedEventArgs e)
		{
			this.Canvas_Admin.Visibility = Visibility.Collapsed;
		}
	}
}
