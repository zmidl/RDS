using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RDS.ViewModels.Common;
using System.Threading;

namespace RDS.Views
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		private object CurrentContent;

		public static event EventHandler<GlobalNotifyArgs> GlobalNotify;

		public virtual void OnGlobalNotify(GlobalNotifyArgs myArgs)
		{
			MainWindow.GlobalNotify?.Invoke(null, myArgs);
		}

		public MainWindow()
		{
			InitializeComponent();
			this.CurrentContent = this.Content;
			this.DisplayTwoButton();
		}

		private void DisplayTwoButton()
		{
			Task task2 = new Task(() =>
			{
				//for (int i = 12; i > 0; i--)
				//{
				//	Thread.Sleep(300);
				//	this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { this.TextBlock_count.Text = i.ToString(); }));
				//}
			});
			task2.ContinueWith(t =>
			{
				this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { this.Content = new MainView(); }));
			});

			task2.Start();
		}

		private void Button_ShowPrecheckView_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new PrecheckView();
		}

		private void Button_ShowReportView_Click(object sender, RoutedEventArgs e)
		{
			General.ExitView(this.CurrentContent, this, ((IExitView)new Monitor.ReportView()));
		}

		private void Button_ShowMaintenanceView_Click_1(object sender, RoutedEventArgs e)
		{
			General.ExitView(this.CurrentContent, this, ((IExitView)new MaintenanceView()));
		}

		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.Content = this.CurrentContent;
		}

		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				if (Keyboard.IsKeyDown(Key.L))
				{
					ResourceDictionary langRd = null;
					try
					{
						langRd = Application.LoadComponent(new Uri("/RDS;component/Apps/Languages/" + "en-US" + ".xaml", UriKind.Relative)) as ResourceDictionary;
					}
					catch
					{

					}

					if (langRd != null)
					{
						if (this.Resources.MergedDictionaries.Count > 0)
						{
							this.Resources.MergedDictionaries.Clear();
						}
						this.Resources.MergedDictionaries.Add(langRd);
					}
				}
				else if(Keyboard.IsKeyDown(Key.S))
				{
					if (Keyboard.IsKeyDown(Key.NumPad1)) this.OnGlobalNotify(new GlobalNotifyArgs($"SampleState1"));
					else if (Keyboard.IsKeyDown(Key.NumPad2)) this.OnGlobalNotify(new GlobalNotifyArgs($"SampleState2"));
					else if (Keyboard.IsKeyDown(Key.NumPad3)) this.OnGlobalNotify(new GlobalNotifyArgs($"SampleState3"));
					else if (Keyboard.IsKeyDown(Key.NumPad4)) this.OnGlobalNotify(new GlobalNotifyArgs($"SampleState4"));
				}
				else if (Keyboard.IsKeyDown(Key.M))
				{
					if (Keyboard.IsKeyDown(Key.NumPad1))
					{
						this.OnGlobalNotify(new GlobalNotifyArgs($"MixtureState1"));
					}
					else if (Keyboard.IsKeyDown(Key.NumPad2))
					{
						this.OnGlobalNotify(new GlobalNotifyArgs($"MixtureState2"));
					}
				}
			}
		}
	}

	public class GlobalNotifyArgs : EventArgs
	{
		public string Index { get; set; }

		public GlobalNotifyArgs(string index)
		{
			this.Index = index;
		}
	}
}
