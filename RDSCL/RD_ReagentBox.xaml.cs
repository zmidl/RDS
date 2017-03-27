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

namespace RDSCL
{
	/// <summary>
	/// RD_ReagentBox.xaml 的交互逻辑
	/// </summary>
	public partial class RD_ReagentBox : UserControl
	{
		public double Value
		{
			get { return (double)GetValue(ValueProperty)*2; }
			set { SetValue(ValueProperty, value*2); }
		}
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(nameof(Value), typeof(double), typeof(RD_ReagentBox), new PropertyMetadata(100d));


		public RD_ReagentBox()
		{
			InitializeComponent();
		}
	}
}
