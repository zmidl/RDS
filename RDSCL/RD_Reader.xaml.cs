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
	/// RD_Reader.xaml 的交互逻辑
	/// </summary>
	public partial class RD_Reader : UserControl
	{
		public object DataSource
		{
			get { return (object)GetValue(DataSourceProperty); }
			set { SetValue(DataSourceProperty, value); }
		}
		public static readonly DependencyProperty DataSourceProperty =
			DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(RD_Reader), new PropertyMetadata(null));


		public RD_Reader()
		{
			InitializeComponent();
		}
	}
}
