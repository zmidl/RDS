using System;
using System.Windows.Controls;
using System.Windows;
using RDS.Views;

namespace RDS.ViewModels.Common
{
	static class General
    {
		public static Window owner { get; set; }

		private static PopupWindow popupWindow;

		public static void InitializePopupWindow(PopupWindow popupWindow)
		{
			General.popupWindow = popupWindow;
		}

		public static void ExitPopupWindow()
		{
			General.popupWindow.ViewModel.ExecuteExitPopupWindowView();
		}

		public static void ShowMessage(string message)
		{
			General.popupWindow.ViewModel.ShowMessage(message);
			General.popupWindow.ShowDialog();
		}

		public static void ShowAdministrators()
		{
			General.popupWindow.ViewModel.ShowAdmin();
			General.popupWindow.ShowDialog();
		}

		public static void ShowCricleProgress()
		{
			General.popupWindow.ViewModel.ShowCricleProgress();
			
			General.popupWindow.Show();
		}

		public static void HideCircleProgress()
		{

			General.popupWindow.Hide();
		}

		public static void ExitView(object oldContent, Window newContent, IExitView iExitView)
        {
            iExitView.ExitView = new Action(() => { newContent.Content = oldContent; });
            newContent.Content = iExitView;
        }

        public static void ExitView(object oldContent, UserControl newContent, IExitView iExitView)
        {
            iExitView.ExitView = new Action(() => { newContent.Content = oldContent; });
            newContent.Content = iExitView;
        }
    }  
}
