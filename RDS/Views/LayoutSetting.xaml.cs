using System.Windows;

namespace RDS.Views
{
	/// <summary>
	/// LayoutSetting.xaml 的交互逻辑
	/// </summary>
	public partial class LayoutSetting : Window
	{
		public LayoutSetting()
		{
			InitializeComponent();
		}

		private void UcButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
