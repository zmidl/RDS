using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RDS.ViewModels.Common;
using System.Data;

namespace RDS.Views
{
	public partial class HistroyView : UserControl, IExitView
    {
        Action IExitView.ExitView { get; set; }
        private object CurrentContent;

        private DataTable TestData()
        {
            DataTable result = new DataTable();
            DataColumn column = new DataColumn("报告名", typeof(string));
            result.Columns.Add(column);

            for (int i = 0; i < 10; i++)
            {
                DataRow dataRow = result.NewRow();
                dataRow[0] = "测试数据" + i.ToString();
                result.Rows.Add(dataRow);
            }
            return result;
        }

        public HistroyView()
        {
            InitializeComponent();
            this.CurrentContent = this.Content;
        }

        private void Button_SearchReport_Click(object sender, RoutedEventArgs e)
        {
            this.DataGrid_ReportName.ItemsSource = this.TestData().AsDataView();
            this.DataGrid_ReportName.Columns[0].Width = 80;
        }

        private void Label_ShowFirstDiagram_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            General.ExitView(this.CurrentContent, this, ((IExitView)new Monitor.DiagramView()));
        }
    }
}
