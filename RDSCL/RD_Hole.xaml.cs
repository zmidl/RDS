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
	/// RD_Hole.xaml 的交互逻辑
	/// </summary>
	public partial class RD_Hole : UserControl
	{
		private static SolidColorBrush DefaultContentColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2AD0D0D0"));

		public Brush ExcircleColor
		{
			get { return (SolidColorBrush)GetValue(ExcircleColorProperty); }
			set { SetValue(ExcircleColorProperty, value); }
		}
		public static readonly DependencyProperty ExcircleColorProperty =
			DependencyProperty.Register(nameof(ExcircleColor), typeof(Brush), typeof(RD_Hole), new PropertyMetadata(new SolidColorBrush(Colors.White)));

		public Brush ContentColor
		{
			get { return (Brush)GetValue(ContentColorProperty); }
			set { SetValue(ContentColorProperty, value); }
		}
		public static readonly DependencyProperty ContentColorProperty =
			DependencyProperty.Register(nameof(ContentColor), typeof(Brush), typeof(RD_Hole), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));


		public Brush ContentColorMask
		{
			get { return (Brush)GetValue(ContentColorMaskProperty); }
			set { SetValue(ContentColorMaskProperty, value); }
		}
		public static readonly DependencyProperty ContentColorMaskProperty =
			DependencyProperty.Register(nameof(ContentColorMask), typeof(Brush), typeof(RD_Hole), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));





		public RD_Hole()
		{
			InitializeComponent();
		}
	}
}
