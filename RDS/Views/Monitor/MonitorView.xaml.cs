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
using RDS.Apps.Common;
using System.Timers;
using System.Threading;

namespace RDS.Views.Monitor
{
    /// <summary>
    /// MonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorView : UserControl
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private object currentContent;

        public MonitorView()
        {
            InitializeComponent();
            this.currentContent = this.Content;
        }

        private void Button_Emergency_Click(object sender, RoutedEventArgs e)
        {
          

            //General.ExitView(this.CurrentContent, this, (IExitView)new Monitor.HelpView());
        }

        private void Button_OnAndOff_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.Button_OnAndOff.Content.ToString() == "启动") { cancellationTokenSource = new CancellationTokenSource(); this.Button_End.IsEnabled = false; }
            else cancellationTokenSource.Cancel();

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
                   MessageBoxResult taskFinishedResult = MessageBox.Show("是否继续试实验任务?", "任务结束提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                   if (taskFinishedResult == MessageBoxResult.Yes)
                   {
                       
                   }
                   else
                   {
                       MessageBox.Show("请关闭设备");
                       General.ExitView(this.currentContent, this, ((IExitView)new MaintenanceView()));
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
            General.ExitView(this.currentContent, this, (IExitView)new Monitor.SampleView());
        }

        private void Rectangle_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {
            General.ExitView(this.currentContent, this, (IExitView)new ReagentView());
        }

        private void Rectangle_PreviewMouseUp_2(object sender, MouseButtonEventArgs e)
        {
            General.ExitView(this.currentContent, this, (IExitView)new Monitor.ReportView());
        }
    }
}
