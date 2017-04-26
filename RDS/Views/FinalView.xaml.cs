using RDS.ViewModels;
using RDS.ViewModels.Common;
using System.Windows;
using System.Windows.Controls;

namespace RDS.Views
{
	/// <summary>
	/// FinalView.xaml 的交互逻辑
	/// </summary>
	public partial class FinalView : UserControl
    {
		public FinalViewModel ViewModel { get { return this.DataContext as FinalViewModel; } }

		public FinalView()
		{
			InitializeComponent();
			this.DataContext = new FinalViewModel();
		}
    }
}
