using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using RDS.ViewModels.Common;

namespace RDS.Views
{
	/// <summary>
	/// PrecheckView.xaml 的交互逻辑
	/// </summary>
	public partial class PrecheckView : UserControl,IExitView
    {

		private int testCount = 3;

        Action IExitView.ExitView { get; set; }

        private object PreviousContent;

        public PrecheckView()
        {
            InitializeComponent();
            this.PreviousContent = this.Content;
            this.InitializeInstrument();
        }

     
        private void InitializeInstrument()
        {
			this.ProgressBar_CheckTemperature.Value = 0;
			this.testCount--;
            Task checkTemperature = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Thread.Sleep(10);
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { this.ProgressBar_CheckTemperature.Value += 2; }));
                }
            }).ContinueWith(task =>
            {
				var result = false;
				if (this.testCount == 0) result = true;
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
				{
					this.CheckBox_First.IsChecked=result;
					var actions = new Action[3];
					actions[0] = new Action(() => this.InitializeInstrument());
					actions[1] = new Action(() => this.ExitView());
					if (result==false) General.PopupWindow(ViewModels.PopupType.ShowMessageWithRetryCancel,General.FindStringResource(Properties.Resources.PopupWindow_CheckInstrumentErrorMessage),actions);
				}));
            });
        }

		public void ExitView()
		{
			((IExitView)this).ExitView();
		}

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
			if (this.CheckBox_First.IsChecked == true &&

				this.CheckBox_Second.IsChecked == true &&
				this.CheckBox_Fourth.IsChecked == true &&
				this.CheckBox_Fifth.IsChecked == true)
			{
				General.ExitView(this.PreviousContent, this, ((IExitView)new InitializeSuppliesView()));
			}
        }

		private void CheckBox_Second_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
