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
using System.Collections.ObjectModel;
using System.Data;
using RDS.Apps.Common;

namespace RDS.Views.History
{
    /// <summary>
    /// ReportView.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryView : UserControl,IExitView
    {
        Action IExitView.ExitView { get; set; }

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

        public HistoryView()
        {
            InitializeComponent();
        }

        private void Button_SearchReport_Click(object sender, RoutedEventArgs e)
        {
            this.DataGrid_ReportName.ItemsSource = this.TestData().AsDataView();
            this.DataGrid_ReportName.Columns[0].Width = 80;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }
    }
}
