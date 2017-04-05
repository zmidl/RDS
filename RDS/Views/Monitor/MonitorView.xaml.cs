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
		private readonly string NotSample = Properties.Resources.NotSample;
		private readonly string PrepareSample = Properties.Resources.PrepareSample;
		private readonly string AlreadySample = Properties.Resources.AlreadySample;
		

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
		    this.ViewModel.A();

			this.sampleView = new SampleView();
			this.sampleView.DataContext = this.ViewModel.SampleViewModel;
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			var showView = (ShowView)e;
			switch (showView)
			{
				case ShowView.ShowSampleView:
				{
					this.ViewModel.SampleViewModel.MultipeSetSampleStateToEmergency();
					
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
				var lisFilesPath = string.Format(ConfigurationManager.AppSettings[Properties.Resources.LisFilesPath].ToString(), System.IO.Directory.GetCurrentDirectory(), DateTime.Now.ToString(Properties.Resources.LisFileNameFormat));
				try
				{
					var dt = XmlOperation.ReadXmlFile(lisFilesPath).Tables[2];
				}
				catch (Exception e1)
				{
					MessageBox.Show(e1.Message);
				}
			}
			else if (e.Index == $"SampleState1")
			{
				this.ViewModel.Reader.Strips[0].State = StripState.Leaving;
				var test = this.Reader_1.Source;
				var test2 = this.Heating_1.ItemsSource;
				this.ShakerRack_1.IsShake = true;
				this.ViewModel.SetTipState(0, 0, TipState.Exist);

				this.ViewModel.SetTipState(1, 0, TipState.Exist);

				//this.ViewModel.SampleViewModel.TwentyUnionSampleHoles[0].Samples[0].SampleState = SampleState.Emergency;
				this.ViewModel.SetSampleTubeState(0, 0, SampleTubeState.Sampled);
				this.ViewModel.CupRacks[0].Strips[0].Cells[0].CellState = HoleState.Full;
				this.ViewModel.CupRacks[0].Strips[0].State = StripState.Leaving;
				this.ViewModel.Heating.Strips[0].State = StripState.Inexistence;
				this.ViewModel.Heating.Strips[0].Cells[0].CellState = HoleState.Full;

				this.ViewModel.SetShakerRackCellState(1, 2, HoleState.Full);

				for (int i = 0; i < 7; i++)
				{
					this.ViewModel.CupRacks[2].Strips[i].State = StripState.Inexistence;
				}
			}
			else if (e.Index == $"SampleState2")
			{
				this.ShakerRack_1.IsShake = false;
				this.ViewModel.SetShakerRackCellState(1, 2, HoleState.None);
				this.ViewModel.SetTipState(0, 0, TipState.NoExist);


				this.ViewModel.SetSampleTubeState(0, 0, SampleTubeState.Normal);
				this.ViewModel.CupRacks[0].Strips[0].Cells[0].CellState = HoleState.Empty;
				this.ViewModel.CupRacks[0].Strips[0].State = StripState.Existence;

				this.ViewModel.Heating.Strips[0].State = StripState.Leaving;
				this.ViewModel.Heating.Strips[0].Cells[0].CellState = HoleState.Empty;
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
				this.ViewModel.SetReaderEnzymeValue(0, v);
			}
			else if (e.Index == $"Enzyme-")
			{
				v--;
				this.ViewModel.SetReaderEnzymeValue(0, v);
			}
			else if (e.Index == this.NotSample || e.Index == this.PrepareSample || e.Index == this.AlreadySample)
			{
				this.ViewModel.SetSampleRackState(1, e.Index);
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
			this.ViewModel.B();
		}

		private void MyCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			this.ViewModel.C();
		}
	}
}
