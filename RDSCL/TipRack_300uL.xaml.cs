using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace RDSCL
{
	/// <summary>
	/// TipRack_300uL.xaml 的交互逻辑
	/// </summary>
	public partial class TipRack_300uL : UserControl
	{


		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(TipRack_300uL), new PropertyMetadata(null));


		public TipRack_300uL()
		{
			InitializeComponent();
		}
	}
}
