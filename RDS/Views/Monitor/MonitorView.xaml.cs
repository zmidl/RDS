using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RDS.ViewModels.Common;
using System.Threading;
using RDS.ViewModels;
using System.Windows.Media.Animation;
using System.Configuration;
using RDSCL;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// MonitorView.xaml 的交互逻辑
	/// </summary>
	public partial class MonitorView : UserControl
	{

		private SampleView sampleView;

		private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		private object currentContent;

		public MonitorViewModel ViewModel { get { return this.DataContext as MonitorViewModel; } }

		public MonitorView()
		{
			InitializeComponent();
			this.DataContext = new MonitorViewModel();
			this.ViewModel.ViewChanged += ViewModel_ViewChanged;
			this.currentContent = this.Content;
			MainWindow.GlobalNotify += MainWindow_GlobalNotify;
			this.ViewModel.InitializeRemainingTimer();

			this.sampleView = new SampleView();
			this.sampleView.DataContext = this.ViewModel.SampleViewModel;

			this.ViewModel.SetSamplingResult(false);
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			var args = (MonitorViewModel.MonitorViewChangedArgs)e;
			switch (args.Option)
			{
				case MonitorViewModel.ViewChangedOption.ShowSampleView:
				{
					General.ExitView(this.currentContent, this, (IExitView)sampleView);
					break;
				}
				case MonitorViewModel.ViewChangedOption.TaskStop:
				{
					this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
					{
						General.ShowMessageWithFinishContinue(General.FindStringResource(Properties.Resources.PopupWindow_TaskFinishedMessage), new Action(() => { General.ExitView(this.Content, this, ((IExitView)new MaintenanceView())); }), new Action(() => {; }));
					}));
					break;
				}
				case MonitorViewModel.ViewChangedOption.NotifySamplingResult:
				{
					var storyboard = this.FindResource(Properties.Resources.TwinkleAnimation) as Storyboard;

					var twinkleModule = (FrameworkElement)this.Rectangle_Prompt;

					if ((bool)args.Value == false)
					{
						storyboard.RepeatBehavior = new RepeatBehavior(1);

						storyboard.Completed += (storyboardSender, eventArgs) =>
						{
							if (this.ViewModel.SamplingResult == false) storyboard.Begin(twinkleModule);
						};
						storyboard.Begin(twinkleModule);
					}
					break;
				}
				default:
				{
					break;
				}
			}
		}

		bool[] samples = new bool[4];

		private void MainWindow_GlobalNotify(object sender, GlobalNotifyArgs e)
		{
			var v = 0;

			if (e.Index == $"MixtureState1")
			{
				//string connectionString = string.Format
				//	(
				//		ConfigurationManager.AppSettings[Properties.Resources.DatabaseConnectionString].ToString(),
				//		System.IO.Directory.GetCurrentDirectory()
				//	);

				//SQLiteHelper sqlManager = new SQLiteHelper(connectionString);
				//System.Data.DataTable dt = sqlManager.GetResultTable("select * from RdBarcodeUsages");
				//MessageBox.Show(dt.Rows[0][1].ToString());
				this.ViewModel.SetSamplingResult(true);
			}
			else if (e.Index == $"MixtureState2")
			{
				this.ViewModel.SetSamplingResult(false);
			}
			else if (e.Index == $"SampleState1")
			{
				this.ViewModel.Heating.Strips[1].IsExist = true;

				this.ViewModel.Heating.OlefinBox.Volume = 10;

				this.ViewModel.ShakerRack.IsShark = true;
				this.ViewModel.ShakerRack.Strips[0].IsExist = true;

				this.ViewModel.ReagentRack.AMPBottles[0].Volume = 0;
				this.ViewModel.ReagentRack.AMPBottles[1].Volume = 3;

				this.ViewModel.Reader.Strips[0].IsExist = true;
				this.ViewModel.Reader.Strips[1].IsExist = true;
				this.ViewModel.Reader.Strips[2].IsExist = true;


				this.ViewModel.SetReaderCellState(1, 2, true);
				this.ViewModel.SetReaderEnzymeBottlesValue(3, 45);
				//this.ShakerRack_1.IsShake = true;
				this.ViewModel.SetTipState(0, 1, TipState.Exist);

				this.ViewModel.SetTipState(1, 1, TipState.Exist);

				this.ViewModel.SetReagentBoxVolume(0, 3);
				this.ViewModel.SetReagentBoxVolume(1, 0);


				this.ViewModel.CupRacks[0].Strips[0].Cells[0].IsLoaded = true;
				this.ViewModel.CupRacks[0].Strips[0].Cells[1].IsLoaded = true;

				this.ViewModel.CupRacks[0].Strips[0].IsExist = true;
				this.ViewModel.Heating.Strips[0].IsExist = true;
				this.ViewModel.Heating.Strips[0].Cells[0].IsLoaded = true;

				this.ViewModel.SetShakerRackCellState(1, 2, true);

				for (int i = 0; i < 2; i++)
				{
					this.ViewModel.CupRacks[2].Strips[i].IsExist = true;
				}
			}
			else if (e.Index == $"SampleState2")
			{
				this.ViewModel.Heating.Strips[1].IsExist = false;
				this.ViewModel.Heating.OlefinBox.Volume = 0;
				this.ViewModel.ShakerRack.IsShark = false;
				this.ViewModel.ShakerRack.Strips[0].IsExist = false;
				this.ViewModel.ReagentRack.AMPBottles[0].Volume = 10;
				this.ViewModel.ReagentRack.AMPBottles[1].Volume = 15;
				this.ViewModel.SetReagentBoxVolume(0, 30);


				this.ViewModel.SetTipState(0, 0, TipState.NoExist);


				this.ViewModel.SetSampleTubeState(0, 0, SampleTubeState.Normal);
				this.ViewModel.CupRacks[0].Strips[0].Cells[0].IsLoaded = false;
				this.ViewModel.CupRacks[0].Strips[0].IsExist = false;

				this.ViewModel.Heating.Strips[0].IsExist = false;
				this.ViewModel.Heating.Strips[0].Cells[0].IsLoaded = false;
			}
			else if (e.Index == $"SampleState3")
			{
				this.ViewModel.SetSampleTubeState(1, 1, SampleTubeState.Emergency);
			}
			else if (e.Index == $"SampleState4")
			{
				this.ViewModel.SetSampleTubeState(2, 1, SampleTubeState.Sampling);
			}
			else if (e.Index == $"Enzyme+")
			{
				v++;
				this.ViewModel.Reader.EnzymeBottles[0].Volume += v;

				this.ViewModel.ReagentRack.MBBottles[0].Volume += v;
				this.ViewModel.ReagentRack.MBBottles[1].Volume += v * 2;

				this.ViewModel.Reader.Temperature += v;
				this.ViewModel.SetReaderEnzymeBottlesValue(0, v);

				this.ViewModel.Heating.OlefinBox.Volume += v;
				this.ViewModel.Heating.Temperature += v;
			}
			else if (e.Index == $"Enzyme-")
			{
				v--;
				this.ViewModel.Reader.EnzymeBottles[0].Volume += v;
				this.ViewModel.Reader.Temperature += v;
				this.ViewModel.Heating.Temperature += v;
				this.ViewModel.SetReaderEnzymeBottlesValue(0, v);
				this.ViewModel.Heating.OlefinBox.Volume += v;
			}
			else if (e.Index == "Sample1")
			{
				this.samples[0] = !this.samples[0];
				if (this.samples[0]) this.ViewModel.SetSampleRackState(0, SampleRackState.AlreadySample);
				else this.ViewModel.SetSampleRackState(0, SampleRackState.NotSample);
				this.ViewModel.SampleViewModel.DatatableToEntity(SampleRackIndex.RackA);
			}
			else if (e.Index == "Sample2")
			{
				this.samples[1] = !this.samples[1];
				if (this.samples[1]) this.ViewModel.SetSampleRackState(1, SampleRackState.AlreadySample);
				else this.ViewModel.SetSampleRackState(1, SampleRackState.NotSample);
				this.ViewModel.SampleViewModel.DatatableToEntity(SampleRackIndex.RackB);
			}
			else if (e.Index == "Sample3")
			{
				this.samples[2] = !this.samples[2];
				if (this.samples[2]) this.ViewModel.SetSampleRackState(2, SampleRackState.AlreadySample);
				else this.ViewModel.SetSampleRackState(2, SampleRackState.NotSample);
				this.ViewModel.SampleViewModel.DatatableToEntity(SampleRackIndex.RackC);
			}
			else if (e.Index == "Sample4")
			{
				this.samples[3] = !this.samples[3];
				if (this.samples[3]) this.ViewModel.SetSampleRackState(3, SampleRackState.AlreadySample);
				else this.ViewModel.SetSampleRackState(3, SampleRackState.NotSample);
				this.ViewModel.SampleViewModel.DatatableToEntity(SampleRackIndex.RackD);
			}
		}

		private void Rectangle_PreviewMouseUp_2(object sender, MouseButtonEventArgs e)
		{
			General.ExitView(this.currentContent, this, (IExitView)new ReportView());
		}

		private void SampleRack_MouseUp(object sender, MouseButtonEventArgs e)
		{
			sampleView.DataContext = this.ViewModel.SampleViewModel;
			General.ExitView(this.currentContent, this, (IExitView)sampleView);
		}

		private void MyCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			this.ViewModel.StartRemainingTimer();
		}

		private void MyCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			this.ViewModel.StopRemainingTimer();
		}
	}
}
