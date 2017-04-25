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
			//this.TextBlock_message.Text= this.FindResource(Properties.Resources.FinalView_Message1).ToString();
		}

        private void Button_ExitApp_Click(object sender, RoutedEventArgs e)
        {

			////this.AAA.Text= this.FindResource("Test").ToString();
			if (test==false)
			{
				if (MessageBox.Show($"打开紫外灯。", "维护提示", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
				{
					this.test = true;
					this.TextBlock_message.Text = this.FindResource(Properties.Resources.FinalView_Message2).ToString();
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
