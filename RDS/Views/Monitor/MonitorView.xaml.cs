using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RDS.ViewModels.Common;
using System.Threading;
using RDS.ViewModels;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// MonitorView.xaml 的交互逻辑
	/// </summary>
	public partial class MonitorView : UserControl
    {

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
			switch(showView)
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
			if (e.Index == 10) this.ViewModel.TempelateValue -= 1;
			else if (e.Index == -10) this.ViewModel.TempelateValue += 1;
			else if (e.Index == 5)
			{
				this.SixTube_1.CurrentState = RDSCL.SixTubeState.Inexistence;
				this.SixTube_Warm1.CurrentState = RDSCL.SixTubeState.Existence;
				this.SixTube_Warm1.NumberValue = "1";
			}
			else if (e.Index == 6)
			{
				this.SixTube_1.CurrentState = RDSCL.SixTubeState.Existence;
				this.SixTube_Warm1.CurrentState = RDSCL.SixTubeState.Inexistence;
				this.SixTube_Warm1.NumberValue = "0";
			}
			else if (e.Index == 7)
			{
				this.SixTube_1.test = true;
			}
			else if (e.Index == 8)
			{
				this.SixTube_1.test = false;
			}
			else
			{
				for (int i = 0; i < 80; i++)
				{
				
					this.ViewModel.SampleViewModel.SampleDescriptions[i].State = (Sampling)e.Index;
				}
			}
			this.ViewModel.RaiseTempelateColor();
		}

		private void Button_Emergency_Click(object sender, RoutedEventArgs e)
        {
          

            //General.ExitView(this.CurrentContent, this, (IExitView)new Monitor.HelpView());
        }

        private void Button_OnAndOff_Click_1(object sender, RoutedEventArgs e)
        {
			bool isBroken=false;
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
                  if(isBroken==false)
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
