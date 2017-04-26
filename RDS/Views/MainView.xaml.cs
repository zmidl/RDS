using RDS.ViewModels;
using RDS.ViewModels.Common;
using System.Windows;
using System.Windows.Controls;

namespace RDS.Views
{
	/// <summary>
	/// MainView.xaml 的交互逻辑
	/// </summary>
	public partial class MainView : UserControl
	{
		//public Action<int> NotifyParentView;

		private TaskView taskView = new TaskView();

		private HistroyView histroyView = new HistroyView();

		private HelpView helpView = new HelpView();

		private object PreviousContent;

		public MainViewModel ViewModel { get { return this.DataContext as MainViewModel; } }

		public MainView()
		{
			InitializeComponent();

			this.DataContext = new MainViewModel();

			this.ContentControl_CurrentContent.Content = this.taskView;

			this.PreviousContent = this.ContentControl_CurrentContent.Content;

			this.ViewModel.ViewChanged += ViewModel_ViewChanged;

			PopupWindow popupWindow = new PopupWindow();
			popupWindow.DataContext = this.ViewModel.PopupWindowViewModel;
			General.InitializePopupWindow(popupWindow);
		}

		

		private void ViewModel_ViewChanged(object sender, object e)
		{
			switch ((MainViewModel.ViewChange)e)
			{
				case MainViewModel.ViewChange.TaskView: { this.ContentControl_CurrentContent.Content = this.taskView; break; }
				case MainViewModel.ViewChange.HistroyView: { this.ContentControl_CurrentContent.Content = this.histroyView; break; }
				case MainViewModel.ViewChange.HelpView: { this.ContentControl_CurrentContent.Content = this.helpView; break; }
				case MainViewModel.ViewChange.ExitApp: { Application.Current.Shutdown();/*Environment.Exit(0);*/break; }
				case MainViewModel.ViewChange.AdminView: { /*General.ShowAdministratorsView();*/ break; }
				default: break;
			}
		}
	}
}
