﻿using RDS.ViewModels;
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

		public MainViewViewModel ViewModel { get { return this.DataContext as MainViewViewModel; } }

		public MainView()
		{
			InitializeComponent();

			this.DataContext = new MainViewViewModel();

			this.ContentControl_CurrentContent.Content = this.taskView;

			this.PreviousContent = this.ContentControl_CurrentContent.Content;

			this.ViewModel.ViewChanged += ViewModel_ViewChanged;

			PopupWindow popupWindow = new PopupWindow();
			popupWindow.DataContext = this.ViewModel.PopupWindowViewModel;
			popupWindow.Owner = Window.GetWindow(this);
			General.InitializePopupWindow(popupWindow);
		}

		

		private void ViewModel_ViewChanged(object sender, object e)
		{
			switch ((MainViewViewModel.ViewChange)e)
			{
				case MainViewViewModel.ViewChange.TaskView: { this.ContentControl_CurrentContent.Content = this.taskView; break; }
				case MainViewViewModel.ViewChange.HistroyView: { this.ContentControl_CurrentContent.Content = this.histroyView; break; }
				case MainViewViewModel.ViewChange.HelpView: { this.ContentControl_CurrentContent.Content = this.helpView; break; }
				case MainViewViewModel.ViewChange.ExitApp: { Application.Current.Shutdown();/*Environment.Exit(0);*/break; }
				case MainViewViewModel.ViewChange.AdminView: { /*General.ShowAdministratorsView();*/ break; }
				default: break;
			}
		}

		private void Button_Admin_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
