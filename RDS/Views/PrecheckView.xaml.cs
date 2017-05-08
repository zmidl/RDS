 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                    Thread.Sleep(50);
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { this.ProgressBar_CheckTemperature.Value += 2; }));
                }
            }).ContinueWith(task =>
            {
				var result = false;
				if (this.testCount == 0) result = true;
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
				{
					this.CheckBox_First.IsChecked=result;
					if(result==false) General.ShowMessageWithRetryCancel(General.FindResource(Properties.Resources.PopupWindow_CheckInstrumentErrorMessage),new Action(()=> this.InitializeInstrument()),new Action(()=> this.ExitView()));
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
