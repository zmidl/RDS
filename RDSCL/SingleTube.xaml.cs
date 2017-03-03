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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class SingleTube : UserControl
    {

        public SolidColorBrush ExcircleColor
        {
            get { return (SolidColorBrush)GetValue(ExcircleColorProperty); }
            set { SetValue(ExcircleColorProperty, value); }
        }
        public static readonly DependencyProperty ExcircleColorProperty =
            DependencyProperty.Register(nameof(ExcircleColor), typeof(SolidColorBrush), typeof(SingleTube), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public SolidColorBrush ContentColor
        {
            get { return (SolidColorBrush)GetValue(ContentColorProperty); }
            set { SetValue(ContentColorProperty, value); }
        }
        public static readonly DependencyProperty ContentColorProperty =
            DependencyProperty.Register
            (nameof(ContentColor), typeof(SolidColorBrush), typeof(SingleTube), 
            new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2AD0D0D0"))));

		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}
		public static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(SingleTube), new PropertyMetadata(false, new PropertyChangedCallback(Callback)));
		private static void Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SingleTube singleTube = d as SingleTube;
			var isSelected = (bool)e.NewValue;
			if (isSelected) singleTube.ExcircleColor = new SolidColorBrush(Colors.DarkGoldenrod);
			else singleTube.ExcircleColor = new SolidColorBrush(Colors.White);
		}


		public SingleTube()
        {
            InitializeComponent();
        }
    }
}
