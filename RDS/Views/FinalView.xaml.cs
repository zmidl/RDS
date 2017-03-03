using System.Windows;
using System.Windows.Controls;

namespace RDS.Views
{
	/// <summary>
	/// FinalView.xaml 的交互逻辑
	/// </summary>
	public partial class FinalView : UserControl
    {
		private bool test = false;
        public FinalView()
        {
            InitializeComponent();
        }

        private void Button_ExitApp_Click(object sender, RoutedEventArgs e)
        {
			if (test==false)
			{
				if (MessageBox.Show("请打开紫外线。", "维护提示", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
				{
					this.test = true;
					this.TextBlock_message.Text = $"请关闭计算机";
					this.Button_ExitApp.Content = $"关闭计算机";
				}
			}
			else
			{
				this.test = false;
				Application.Current.Shutdown();
			}
            //Application.Current.Shutdown();
            //System.Environment.Exit(0); 
        }
    }
}
