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
					var actions = new Action[3];
					actions[0] = new Action(() => { General.ExitView(this.Content, this, ((IExitView)new MaintenanceView())); });
					this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
					{
						General.PopupWindow(PopupType.ShowMessageWithFinishContinue,General.FindStringResource(Properties.Resources.PopupWindow_TaskFinishedMessage),actions);
					}));
					break;
				}
				case MonitorViewModel.ViewChangedOption.NotifySamplingResult:
				{
					var storyboard = this.FindResource(Properties.Resources.TwinkleAnimation) as Storyboard;

					var twinkleModule = (FrameworkElement)this.Canvas_TwinkleModule;
					//var twinkleModule2 = (FrameworkElement)this.Path_Hand;

					if ((bool)args.Value == false)
					{
						storyboard.RepeatBehavior = new RepeatBehavior(1);

						storyboard.Completed += (storyboardSender, eventArgs) =>
						{
							if (this.ViewModel.SamplingResult == false)
							{
								storyboard.Begin(twinkleModule);
								//storyboard.Begin(twinkleModule2);
							}
						};
						storyboard.Begin(twinkleModule);
						//storyboard.Begin(twinkleModule2);
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

		int v = 0;
		private void MainWindow_GlobalNotify(object sender, GlobalNotifyArgs e)
		{
			

			if (e.Index == $"MixtureState1")
			{
				string connectionString = string.Format
					(
						ConfigurationManager.AppSettings[Properties.Resources.DatabaseConnectionString].ToString(),
						System.IO.Directory.GetCurrentDirectory()
					);

				SQLiteHelper sqlManager = new SQLiteHelper(connectionString);
				System.Data.DataTable dt = sqlManager.GetResultTable("select * from RdBarcodeUsages");
				MessageBox.Show(dt.Rows[0][1].ToString());


				this.ViewModel.SetSamplingResult(true);
			}
			else if (e.Index == $"MixtureState2")
			{
				this.ViewModel.SetSamplingResult(false);
			}
			else if (e.Index == $"SampleState1")
			{
				this.ViewModel.SetCupRackStripState(0, 0, true,1);
				this.ViewModel.SetCupRackStripState(0, 2, true,3);
				this.ViewModel.SetCupRackStripState(0, 4, true,5);
				this.ViewModel.SetCupRackStripState(0, 6, true,7);
				
		
				this.ViewModel.SetCupRackMixtureState(0, 0, 1, true);
				this.ViewModel.SetCupRackMixtureState(0, 0, 3, true);
				this.ViewModel.SetCupRackMixtureState(0, 0, 5, true);
				this.ViewModel.ShakerRack.IsShark = true;
			

				this.ViewModel.ReagentRack.AMPBottles[0].Volume = 0;
				this.ViewModel.ReagentRack.AMPBottles[1].Volume = 3;

				this.ViewModel.SetReaderEnzymeBottleVolume(3, 45);

				this.ViewModel.SetTipState(0, 1, true);
				this.ViewModel.SetTipState(1, 1, true);

				this.ViewModel.SetReagentBoxVolume(0, 3);
				this.ViewModel.SetReagentBoxVolume(1, 0);
			}
			else if (e.Index == $"SampleState2")
			{
				this.ViewModel.Heating.OlefinBox.Volume = 0;
				this.ViewModel.ShakerRack.IsShark = false;

				this.ViewModel.ReagentRack.AMPBottles[0].Volume = 10;
				this.ViewModel.ReagentRack.AMPBottles[1].Volume = 15;
				this.ViewModel.SetReagentBoxVolume(0, 30);

				this.ViewModel.SetTipState(0, 0,false);

				this.ViewModel.SetSampleState(0, 0, true);
				this.ViewModel.CupRacks[0].Strips[0].Cells[0].IsLoaded = false;
				this.ViewModel.CupRacks[0].Strips[0].IsLoaded = false;
			}
			else if (e.Index == $"SampleState3")
			{
				this.ViewModel.SetSampleState(1, 1, false);
			}
			else if (e.Index == $"SampleState4")
			{
				this.ViewModel.SetSampleState(2, 1, false);
			}
			else if (e.Index == $"Enzyme+")
			{
				v++;
				this.ViewModel.SetOlefinBoxVolume(v);
				this.ViewModel.SetHeatingTemperature(v);

				this.ViewModel.SetReaderEnzymeBottleVolume(1, v + 1);
				this.ViewModel.SetReaderTemperature(v);

				this.ViewModel.ReagentRack.MBBottles[0].Volume += v;
				this.ViewModel.ReagentRack.MBBottles[1].Volume += v * 2;


			}
			else if (e.Index == $"Enzyme-")
			{
				v--;
				this.ViewModel.SetOlefinBoxVolume(v);
				this.ViewModel.SetHeatingTemperature(v);

				this.ViewModel.SetReaderEnzymeBottleVolume(1, v - 1);
				this.ViewModel.SetReaderTemperature(v);

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
				this.ViewModel.CupRacks[0].IsTwinkle = true;
				this.ViewModel.TipRacks[3].IsTwinkle = true;
			}
			else if (e.Index == "Sample4")
			{
				this.ViewModel.CupRacks[0].IsTwinkle = false;
				this.ViewModel.TipRacks[3].IsTwinkle = false;
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
