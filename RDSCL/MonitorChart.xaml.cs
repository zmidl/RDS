using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDSCL
{
	/// <summary>
	/// MonitorChart.xaml 的交互逻辑
	/// </summary>
	public partial class MonitorChart : UserControl
	{
		public RackState RackState
		{
			get { return (RackState)GetValue(RackStateProperty); }
			set { SetValue(RackStateProperty, value); }
		}
		public static readonly DependencyProperty RackStateProperty =
			DependencyProperty.Register(nameof(RackState), typeof(RackState), typeof(MonitorChart), new PropertyMetadata(RackState.None,new PropertyChangedCallback(RackState_Callback)));




		public SolidColorBrush ReagentMask
		{
			get { return (SolidColorBrush)GetValue(ReagentMaskProperty); }
			set { SetValue(ReagentMaskProperty, value); }
		}
		public static readonly DependencyProperty ReagentMaskProperty =
			DependencyProperty.Register("ReagentMask", typeof(SolidColorBrush), typeof(MonitorChart), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C000000"))));




		static void RackState_Callback(DependencyObject d,DependencyPropertyChangedEventArgs e)
		{
			var monitorChart = d as MonitorChart;
			var rackState = (RackState)e.NewValue;
			switch (rackState)
			{
				case RackState.Reagent: { break; }
			}

		}

		public MonitorChart()
		{
			InitializeComponent();
		}
	}

	public enum RackState
	{
		None = 0,
		Reagent = 1,
		Tip = 2,
		Cup = 3,
		Heating = 4,
		Reader = 4
	}

}
