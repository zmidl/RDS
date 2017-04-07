using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using System.Data;

namespace RDS.Views
{
	/// <summary>
	/// TaskView.xaml 的交互逻辑
	/// </summary>
	public partial class TaskView : UserControl
    {
       private object PreviousContent;

        public TaskView()
        {
            InitializeComponent();
            this.PreviousContent = this.Content;

			this.DataGrid1.ItemsSource = this.Test().AsDataView();
        }

        private void Button_NewExperiment_Click(object sender, RoutedEventArgs e)
        {
            General.ExitView(this.PreviousContent, this, ((IExitView)new PrecheckView()));
        }

        private void Button_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            General.ExitView(this.PreviousContent, this, ((IExitView)new MaintenanceView()));
        }

		private DataTable Test()
		{
			DataTable result = new DataTable();
			DataColumn dc = new DataColumn($"Nmae", typeof(string));
			result.Columns.Add(dc);
			dc = new DataColumn($"Age", typeof(int));
			result.Columns.Add(dc);

			for (int i = 0; i < 10; i++)
			{
				DataRow dr = result.NewRow();
				dr[0] = $"Name{i}";
				dr[1] = i;
				result.Rows.Add(dr);
			}
			return result;
		}
    }
}
