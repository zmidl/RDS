using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace RDSCL
{
	/// <summary>
	/// SampleTube.xaml 的交互逻辑
	/// </summary>
	public partial class SampleRack : UserControl
	{
		public IEnumerable SamplesContentColor
		{
			get { return (IEnumerable)GetValue(SamplesContentColorProperty); }
			set { SetValue(SamplesContentColorProperty, value); }
		}
		public static readonly DependencyProperty SamplesContentColorProperty =
			DependencyProperty.Register(nameof(SamplesContentColor), typeof(IEnumerable), typeof(SampleRack), new PropertyMetadata(null));

		public SampleRack()
		{
			InitializeComponent();
		}
	}
}
