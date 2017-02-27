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

namespace RDS.Views.Monitor
{
    /// <summary>
    /// DiagramView.xaml 的交互逻辑
    /// </summary>
    public partial class DiagramView : UserControl, IExitView
    {
        Action IExitView.ExitView { get; set; }
        public DiagramView()
        {
            InitializeComponent();
        }

        private void Label_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {
            ((IExitView)this).ExitView();
        } 
    }
}
