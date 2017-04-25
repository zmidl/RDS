using System.Windows;

namespace RDS.Views
{
	/// <summary>
	/// RdsMessageBox.xaml 的交互逻辑
	/// </summary>
	public partial class RdsMessageBox : Window
	{
		public RdsMessageBox()
		{
			//InitializeComponent();
		}

		public void ShowMessage(string message)
		{
			//this.TextBlock_Message.Text = message;
			base.ShowDialog();
		}

		private void Button_Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
		}
	}
}
