using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using RDS.ViewModels;

namespace RDS.Views
{
	/// <summary>
	/// MaintenanceView.xaml 的交互逻辑
	/// </summary>
	public partial class MaintenanceView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }
		public MaintenanceViewModel ViewModel { get { return this.DataContext as MaintenanceViewModel; } }
		public MaintenanceView()
		{
			InitializeComponent();

			this.DataContext = new MaintenanceViewModel();

			this.ViewModel.ViewChanged += ViewModel_ViewChanged;
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			switch (((MaintenanceViewModel.MaintenanceViewChangedArgs)e).Option)
			{
				case MaintenanceViewModel.ViewChangedOption.ExitMaintenanceView:
				{
					((IExitView)this).ExitView();
					break;
				}
				case MaintenanceViewModel.ViewChangedOption.EnterFinalView:
				{
					this.Content = new FinalView();
					break;
				}
				default:break;
			}
		}
	}
}
