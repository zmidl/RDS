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
					this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(()=> {General.ShowMessageWithYesNo("是否结束任务"); }));
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
				string connectionString = string.Format
					(
						ConfigurationManager.AppSettings[Properties.Resources.DatabaseConnectionString].ToString(),
						System.IO.Directory.GetCurrentDirectory()
					);

				SQLiteHelper sqlManager = new SQLiteHelper(connectionString);
				System.Data.DataTable dt = sqlManager.GetResultTable("select * from RdBarcodeUsages");
				MessageBox.Show(dt.Rows[0][1].ToString());
			}
			else if (e.Index == $"MixtureState2")
			{
				
			}
			else if (e.Index == $"SampleState1")
			{
				this.ViewModel.SampleViewModel.FourSampleRackDescriptions[1].Samples[10].IsSampling = true;
				this.ViewModel.Reader.EnzymeBottles[0].State = ReagentState.Full;
				this.ViewModel.Reader.Strips[0].State = StripState.Leaving;
				this.ViewModel.SetReaderCellState(1, 2, HoleState.Full);
				this.ViewModel.SetReaderEnzymeBottlesValue(3, 45);
				//this.ShakerRack_1.IsShake = true;
				this.ViewModel.SetTipState(0, 1, TipState.Exist);

				this.ViewModel.SetTipState(1, 1, TipState.Exist);

				this.ViewModel.SetReagentBoxState(0, ReagentState.Full);
				this.ViewModel.SetReagentBoxState(7, ReagentState.Few);

				this.ViewModel.SetMBBottleState(0, ReagentState.Normal);
				this.ViewModel.SetMBBottleState(1, ReagentState.Few);
				this.ViewModel.SetAMPBottleState(0, ReagentState.Full);
				this.ViewModel.SetNPBottleState(1, ReagentState.Few);
				this.ViewModel.SetISBottleState(2, ReagentState.Full);
				this.ViewModel.SetNPBottleState(7, ReagentState.Normal);

				this.ViewModel.Heating.OlefinBox.State = ReagentState.Full;
				//this.ViewModel.SampleViewModel.TwentyUnionSampleHoles[0].Samples[0].SampleState = SampleState.Emergency;
				this.ViewModel.SetSampleTubeState(0, 0, SampleTubeState.Sampled);
				this.ViewModel.SetSampleTubeState(3, 5, SampleTubeState.Sampled);

				this.ViewModel.CupRacks[0].Strips[0].Cells[0].State = HoleState.Full;
				this.ViewModel.CupRacks[0].Strips[0].Cells[1].State = HoleState.Full;

				this.ViewModel.CupRacks[0].Strips[0].State = StripState.Leaving;
				this.ViewModel.Heating.Strips[0].State = StripState.Inexistence;
				this.ViewModel.Heating.Strips[0].Cells[0].State = HoleState.Full;

				this.ViewModel.SetShakerRackCellState(1, 2, HoleState.Full);

				for (int i = 0; i < 2; i++)
				{
					this.ViewModel.CupRacks[2].Strips[i].State = StripState.Inexistence;
				}
			}
			else if (e.Index == $"SampleState2")
			{
				this.ViewModel.SampleViewModel.FourSampleRackDescriptions[0].Samples[10].IsSampling = false;
				this.ViewModel.SetShakerRackCellState(1, 2, HoleState.None);
				this.ViewModel.SetTipState(0, 0, TipState.NoExist);


				this.ViewModel.SetSampleTubeState(0, 0, SampleTubeState.Normal);
				this.ViewModel.CupRacks[0].Strips[0].Cells[0].State = HoleState.Empty;
				this.ViewModel.CupRacks[0].Strips[0].State = StripState.Existence;

				this.ViewModel.Heating.Strips[0].State = StripState.Leaving;
				this.ViewModel.Heating.Strips[0].Cells[0].State = HoleState.Empty;
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
				this.ViewModel.ReagentRack.MBBottles[0].Volume += v;
				this.ViewModel.ReagentRack.MBBottles[1].Volume += v * 2;

				this.ViewModel.Reader.Temperature += v;
				this.ViewModel.SetReaderEnzymeBottlesValue(0, v);
			}
			else if (e.Index == $"Enzyme-")
			{
				v--;
				this.ViewModel.Reader.Temperature += v;
				this.ViewModel.SetReaderEnzymeBottlesValue(0, v);
			}
			else if (e.Index == "Sample1")
			{
				this.samples[0] = !this.samples[0];
				if(this.samples[0]) this.ViewModel.SetSampleRackState(0, SampleRackState.AlreadySample);
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
