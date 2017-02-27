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
using RDS.Apps.Common;

namespace RDS.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private object CurrentContent;

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
                //for (int i = 12; i >0; i--)
                //{
                //    Thread.Sleep(300);
                //    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { this.TextBlock_count.Text=i.ToString(); }));
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

        private void Window_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R) && Keyboard.IsKeyDown(Key.D))
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
        }
    }
}
