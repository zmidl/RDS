using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RDS.ViewModels.Common;
using System.Threading;
using RDS.ViewModels;
using System.Runtime.InteropServices;
using System.Configuration;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// MonitorView.xaml 的交互逻辑
	/// </summary>
	public partial class MonitorView : UserControl
	{
		//C:\zhaomin\sourcecode\CppTestDll\Debug
		[DllImport("CppTestDll.dll", EntryPoint = "Test1")]
		extern static int Test1();

		[DllImport("CppTestDll.dll", EntryPoint = "Test2")]
		extern static int Test2();

		[DllImport("ShareDll_d.dll", EntryPoint = "InitDLL")]
		extern static int InitDLL(byte[] targetName);

		[DllImport("ShareDll_d.dll", EntryPoint = "SendData")]
		extern static int SendData(byte[] dataArray, int dataArrayLength, string targetName);

		[DllImport("ShareDll_d.dll", EntryPoint = "ReciveData")]
		extern static int ReciveData(byte[] receivedDataArray, int dataArrayLength, string plpszSrc);

		private SampleView sampleView = new SampleView();

		private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		private object currentContent;

		public MonitorViewModel ViewModel = new MonitorViewModel();

		public MonitorView()
		{
			InitializeComponent();
			this.ViewModel.ViewChanged += ViewModel_ViewChanged;
			this.DataContext = this.ViewModel;

			this.currentContent = this.Content;
			MainWindow.GlobalNotify += MainWindow_GlobalNotify;
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			var showView = (ShowView)e;
			switch (showView)
			{
				case ShowView.ShowSampleView:
				{
					this.ViewModel.SampleViewModel.MultipeSetSampleStateToEmergency();
					sampleView.DataContext = this.ViewModel.SampleViewModel;
					General.ExitView(this.currentContent, this, (IExitView)sampleView);
					break;
				}
				default:
				{
					break;
				}
			}
		}

		private void MainWindow_GlobalNotify(object sender, GlobalNotifyArgs e)
		{
			if (e.Index == $"MixtureState1")
			{


			}
			else if (e.Index == $"MixtureState2")
			{
			}
			else if (e.Index == $"SampleState1")
			{
				this.ViewModel.Reader.Strips[0].State = RDSCL.StripState.Leaving;
				var test = this.Reader_1.Source;
				var test2 = this.Heating_1.ItemsSource;
				this.ShakerRack_1.IsShake = true;
				this.ViewModel.SetTipState(0, 0, TipState.Exist);
				this.ViewModel.SetTipState(0, 10, TipState.Exist);
				this.ViewModel.SetTipState(0, 20, TipState.Exist);
				this.ViewModel.SetTipState(0, 30, TipState.Exist);
				this.ViewModel.SetTipState(0, 40, TipState.Exist);
				this.ViewModel.SetTipState(0, 50, TipState.Exist);
				this.ViewModel.SetTipState(0, 60, TipState.Exist);
				this.ViewModel.SetTipState(0, 70, TipState.Exist);
				this.ViewModel.SetTipState(0, 80, TipState.Exist);
				this.ViewModel.SetTipState(0, 90, TipState.Exist);

				this.ViewModel.SetTipState(1, 0, TipState.Exist);
				this.ViewModel.SetTipState(1, 10, TipState.Exist);
				this.ViewModel.SetTipState(1, 20, TipState.Exist);
				this.ViewModel.SetTipState(1, 30, TipState.Exist);
				this.ViewModel.SetTipState(1, 40, TipState.Exist);
				this.ViewModel.SetTipState(1, 50, TipState.Exist);
				this.ViewModel.SetTipState(1, 60, TipState.Exist);
				this.ViewModel.SetTipState(1, 70, TipState.Exist);
				this.ViewModel.SetTipState(1, 80, TipState.Exist);
				this.ViewModel.SetTipState(1, 90, TipState.Exist);
				//this.ViewModel.SampleViewModel.TwentyUnionSampleHoles[0].Samples[0].SampleState = SampleState.Emergency;
				this.ViewModel.SetSampleTubeState(0, 0, SampleTubeState.Sampled);
				this.ViewModel.CupRacks[0].Strips[0].Cells[0].CellState = CellState.Full;
				this.ViewModel.CupRacks[0].Strips[0].State = RDSCL.StripState.Leaving;
				this.ViewModel.Heating.Strips[0].State = RDSCL.StripState.Inexistence;
				this.ViewModel.Heating.Strips[0].Cells[0].CellState = CellState.Full;

				this.ViewModel.SetShakerRackCellState(1, 2, CellState.Full);

				for (int i = 0; i < 7; i++)
				{
					this.ViewModel.CupRacks[2].Strips[i].State = RDSCL.StripState.Inexistence;
				}
			}
			else if (e.Index == $"SampleState2")
			{
				this.ShakerRack_1.IsShake = false;
				this.ViewModel.SetShakerRackCellState(1, 2, CellState.NoCell);
				this.ViewModel.SetTipState(0, 0, TipState.NoExist);
				this.ViewModel.SetTipState(0, 10, TipState.NoExist);
				this.ViewModel.SetTipState(0, 20, TipState.NoExist);
				this.ViewModel.SetTipState(0, 30, TipState.NoExist);
				this.ViewModel.SetTipState(0, 40, TipState.NoExist);
				this.ViewModel.SetTipState(0, 50, TipState.NoExist);
				this.ViewModel.SetTipState(0, 60, TipState.NoExist);
				this.ViewModel.SetTipState(0, 70, TipState.NoExist);
				this.ViewModel.SetTipState(0, 80, TipState.NoExist);
				this.ViewModel.SetTipState(0, 90, TipState.NoExist);

				this.ViewModel.SetSampleTubeState(0, 0, SampleTubeState.Normal);
				this.ViewModel.CupRacks[0].Strips[0].Cells[0].CellState = CellState.Empty;
				this.ViewModel.CupRacks[0].Strips[0].State = RDSCL.StripState.Existence;

				this.ViewModel.Heating.Strips[0].State = RDSCL.StripState.Leaving;
				this.ViewModel.Heating.Strips[0].Cells[0].CellState = CellState.Empty;
			}
			else if (e.Index == $"SampleState3")
			{
				this.ViewModel.SetSampleTubeState(1, 1, SampleTubeState.Emergency);
			}
			else if (e.Index == $"SampleState4")
			{
				this.ViewModel.SetSampleTubeState(2, 1, SampleTubeState.Sampling);
			}
		}

		private void Button_Emergency_Click(object sender, RoutedEventArgs e)
		{
			//var name = $"EXE2";
			//var name1 = new byte[10] { 0x00, 0x45, 0x00, 0x58, 0x00, 0x45, 0x00, 0x32, 0x00, 0x00 };
			//var name2= System.Text.Encoding.UTF8.GetBytes(name);
			//var name3 = new byte[5] { 0x45, 0x58, 0x45,0x32,  0x00 };
			//var name4 = System.Text.Encoding.Default.GetBytes(name);


			//var isInitDlloK = MonitorView.InitDLL(name4);
			//try
			//{
			//	var rr = MonitorView.SendData(name4, name4.Length, "EXE1");
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}

			MessageBox.Show( ConfigurationManager.AppSettings["Language"].ToString());

		}

		private void Button_OnAndOff_Click_1(object sender, RoutedEventArgs e)
		{
			bool isBroken = false;
			if (this.Button_OnAndOff.Content.ToString() == "启动") { cancellationTokenSource = new CancellationTokenSource(); this.Button_End.IsEnabled = false; }
			else { isBroken = true; cancellationTokenSource.Cancel(); }

			Task t = Task.Factory.StartNew(() =>
		   {
			   for (int i = 12; i > 0; i--)
			   {
				   if (!cancellationTokenSource.IsCancellationRequested)
				   {
					   Thread.Sleep(300);
				   }
				   else
				   {

					   break;
				   }
				   this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { this.TextBlock_RemainingTime.Text = i.ToString(); }));
			   }
		   }).ContinueWith(task =>
		   {
			   this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
			   {

				   this.Button_End.IsEnabled = true;
				   this.Button_OnAndOff.Content = "启动";
				   if (isBroken == false)
				   {
					   MessageBoxResult taskFinishedResult = MessageBox.Show("是否结束实验?", "任务结束提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
					   if (taskFinishedResult == MessageBoxResult.Yes)
					   {
						   MessageBox.Show("请关闭设备");
						   General.ExitView(this.currentContent, this, ((IExitView)new MaintenanceView()));
					   }
				   }
			   }));
		   });

			this.Button_OnAndOff.Content = this.Button_OnAndOff.Content.ToString() == "暂停" ? "启动" : "暂停";
		}

		private void Button_End_Click_1(object sender, RoutedEventArgs e)
		{
			General.ExitView(this.currentContent, this, ((IExitView)new MaintenanceView()));
		}

		private void Canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{

			sampleView.DataContext = this.ViewModel.SampleViewModel;
			General.ExitView(this.currentContent, this, (IExitView)sampleView);
		}

		private void Rectangle_PreviewMouseUp_2(object sender, MouseButtonEventArgs e)
		{
			General.ExitView(this.currentContent, this, (IExitView)new ReportView());
		}

		private void Canvas_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
		{
			General.ExitView(this.currentContent, this, (IExitView)new ReagentView());
		}
	}
}
