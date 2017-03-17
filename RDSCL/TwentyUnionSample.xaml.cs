using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDSCL
{
	/// <summary>
	/// SampleTube.xaml 的交互逻辑
	/// </summary>
	public partial class TwentyUnionSample : UserControl
	{
		public IEnumerable SamplesContentColor
		{
			get { return (IEnumerable)GetValue(SamplesContentColorProperty); }
			set { SetValue(SamplesContentColorProperty, value); }
		}
		public static readonly DependencyProperty SamplesContentColorProperty =
			DependencyProperty.Register(nameof(SamplesContentColor), typeof(IEnumerable), typeof(TwentyUnionSample), new PropertyMetadata(null));

		public TwentyUnionSample()
		{
			InitializeComponent();
		}
	}
}
