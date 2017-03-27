using Microsoft.Expression.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System;
using System.Globalization;

namespace RDSCL
{
	/// <summary>
	/// UserControl1.xaml 的交互逻辑
	/// </summary>
	public partial class Hole : UserControl
	{
		private static SolidColorBrush DefaultContentColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2AD0D0D0"));


		public SolidColorBrush ExcircleColor
		{
			get { return (SolidColorBrush)GetValue(ExcircleColorProperty); }
			set { SetValue(ExcircleColorProperty, value); }
		}
		public static readonly DependencyProperty ExcircleColorProperty =
			DependencyProperty.Register(nameof(ExcircleColor), typeof(SolidColorBrush), typeof(Hole), new PropertyMetadata(new SolidColorBrush(Colors.White)));

		public SolidColorBrush ContentColor
		{
			get { return (SolidColorBrush)GetValue(ContentColorProperty); }
			set { SetValue(ContentColorProperty, value); }
		}
		public static readonly DependencyProperty ContentColorProperty =
			DependencyProperty.Register
			(nameof(ContentColor), typeof(SolidColorBrush), typeof(Hole),
			new PropertyMetadata(Hole.DefaultContentColor));

		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(nameof(Value), typeof(double), typeof(Hole), new PropertyMetadata(100d));

		public double ThicknessValue
		{
			get { return (double)GetValue(ThicknessValueProperty); }
			set { SetValue(ThicknessValueProperty, value); }
		}
		public static readonly DependencyProperty ThicknessValueProperty =
			DependencyProperty.Register(nameof(ThicknessValue), typeof(double), typeof(Hole), new PropertyMetadata(6d));

		public Hole()
		{
			InitializeComponent();
		}
	}

	public class EndAngleConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var input = (double)value;
			if (input < 0) input = 0;
			else if (input > 100) input = 100;
			return input * 3.6d;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
