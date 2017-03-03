using System;
using System.Windows.Controls;
using RDS.ViewModels.Common;

namespace RDS.Views
{
    /// <summary>
    /// InitializeSuppliesView.xaml 的交互逻辑
    /// </summary>
    public partial class InitializeSuppliesView : UserControl,IExitView
    {
        Action IExitView.ExitView { get; set; }

        public ViewModels.InitializeSuppliesViewModel ViewModel { get { return this.DataContext as ViewModels.InitializeSuppliesViewModel; } }

        public InitializeSuppliesView()
        {
            InitializeComponent();
			
            this.DataContext = new ViewModels.InitializeSuppliesViewModel();
        }

		private void Button_Next_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.ViewModel.WizardIndex == 5) this.Content = new Monitor.MonitorView();
        }
    }
}
