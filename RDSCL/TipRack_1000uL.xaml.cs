using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace RDSCL
{
	/// <summary>
	/// TipRack.xaml 的交互逻辑
	/// </summary>
	public partial class TipRack_1000uL : UserControl
	{
		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(TipRack_1000uL), new PropertyMetadata(null));


		public TipRack_1000uL()
		{
			InitializeComponent();
		}
	}
}
