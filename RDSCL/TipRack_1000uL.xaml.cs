using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RDSCL
{
	/// <summary>
	/// TipRack.xaml 的交互逻辑
	/// </summary>
	public partial class TipRack_1000uL : UserControl
	{
		public object DataSource
		{
			get { return GetValue(DataSourceProperty); }
			set { SetValue(DataSourceProperty, value); }
		}
		public static readonly DependencyProperty DataSourceProperty =
			DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(TipRack_1000uL), new PropertyMetadata(null));


		public bool IsTwinkle
		{
			get { return (bool)GetValue(IsTwinkleProperty); }
			set { SetValue(IsTwinkleProperty, value); }
		}
		public static readonly DependencyProperty IsTwinkleProperty =
			DependencyProperty.Register(nameof(IsTwinkle), typeof(bool), typeof(TipRack_1000uL), new PropertyMetadata(false, new PropertyChangedCallback(Callback_IsTwinkle)));

		private static void Callback_IsTwinkle(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var tipRack = (TipRack_1000uL)d;

			var storyboard = tipRack.Resources[Properties.Resources.TwinkleAnimation] as Storyboard;

			var twinkleModule = (FrameworkElement)tipRack;

			if ((bool)e.NewValue == true)
			{
				storyboard.RepeatBehavior = new RepeatBehavior(1);

				storyboard.Completed += (sender, eventArgs) =>
				{
					if (tipRack.IsTwinkle) storyboard.Begin(twinkleModule);
				};
				storyboard.Begin(twinkleModule);
			}
		}

		public TipRack_1000uL()
		{
			InitializeComponent();
		}
	}
}
