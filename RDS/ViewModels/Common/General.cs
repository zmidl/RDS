﻿using System;
using System.Windows.Controls;
using System.Windows;
using RDS.Views;
using System.Configuration;
using System.Collections.Generic;

namespace RDS.ViewModels.Common
{
	static class General
	{
		private static MainWindow mainWindow;

		private static PopupWindow popupWindow;

		public static List<string> UsedReagents { get { return General.popupWindow.ViewModel.UsedReagents; } }

		public static void ShutDown()
		{
			Application.Current.Shutdown();
		}

		public static void InitializePopupWindow(PopupWindow popupWindow)
		{
			General.popupWindow = popupWindow;
		}

		public static void InitializeMainWindow(MainWindow mainWindow)
		{
			General.mainWindow = mainWindow;
		}

		public static string FindResource(string resourceKey)
		{
			return General.mainWindow.FindResource(resourceKey).ToString();
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

		public static void ShowAdministratorsLogin()
		{
			General.popupWindow.ViewModel.ShowAdministratorsLogin();
			General.popupWindow.ShowDialog();
		}

		public static void ShowInformation()
		{
			General.popupWindow.ViewModel.ShowInformation();
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

		public static string ReadConfiguration(string configurationKey)
		{
			return ConfigurationManager.AppSettings[configurationKey].ToString();
		}

		public static bool WriteConfiguration(string configurationKey, string value)
		{
			var result = false;
			try
			{
				Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				configuration.AppSettings.Settings[configurationKey].Value = value;
				configuration.Save();
				result = true;
			}
			catch { }
			return result;
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
