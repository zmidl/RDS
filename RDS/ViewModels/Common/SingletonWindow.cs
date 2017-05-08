using System;
using System.Windows;

namespace RDS.ViewModels.Common
{
	class SingletonWindow
	{
		//注册附加属性
		public static readonly DependencyProperty IsEnabledProperty =
	DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(SingletonWindow), new FrameworkPropertyMetadata(OnIsEnabledChanged));

		public static void SetIsEnabled(DependencyObject element, Boolean value)
		{
			element.SetValue(IsEnabledProperty, value);
		}
		public static Boolean GetIsEnabled(DependencyObject element)
		{
			return (Boolean)element.GetValue(IsEnabledProperty);
		}

		public static void OnIsEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if ((bool)args.NewValue != true)
			{
				return;
			}

			Process();
			return;
		}

		public static void Process()
		{
			var process = GetRunningInstance();
			if (process != null)
			{
				HandleRunningInstance(process);
				Environment.Exit(0);
			}
		}

		const int WS_SHOWNORMAL = 1;
		const int WS_SHOWMAXIMIZE = 3;

		[System.Runtime.InteropServices.DllImport("User32.dll")]
		static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		static extern bool SetForegroundWindow(IntPtr hWnd);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

		static System.Diagnostics.Process GetRunningInstance()
		{
			var current = System.Diagnostics.Process.GetCurrentProcess();
			var processes = System.Diagnostics.Process.GetProcessesByName(current.ProcessName);

			foreach (var process in processes)
			{
				if (process.Id != current.Id)
					if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
						return process;
			}
			return null;
		}

		static void HandleRunningInstance(System.Diagnostics.Process instance)
		{
			if (instance.MainWindowHandle != IntPtr.Zero)
			{
				for (int i = 0; i < 2; i++)
				{
					FlashWindow(instance.MainWindowHandle, 100);
				}
				SetForegroundWindow(instance.MainWindowHandle);
				ShowWindowAsync(instance.MainWindowHandle, SingletonWindow.WS_SHOWMAXIMIZE);
			}
		}

		static void FlashWindow(IntPtr hanlde, int interval)
		{
			FlashWindow(hanlde, true);
			System.Threading.Thread.Sleep(interval);
			FlashWindow(hanlde, false);
			System.Threading.Thread.Sleep(interval);
		}
	}
}
