using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace RDSCL
{
	/// <summary>
	/// ShakerRack.xaml 的交互逻辑
	/// </summary>
	public partial class RD_ShakerRack : UserControl
	{
		System.Windows.Media.Animation.Storyboard storyboard;

		public object DataSource
		{
			get { return GetValue(DataSourceProperty); }
			set { SetValue(DataSourceProperty, value); }
		}
		public static readonly DependencyProperty DataSourceProperty =
			DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(RD_ShakerRack), new PropertyMetadata(null));

		public bool IsShake
		{
			get { return (bool)GetValue(IsShakeProperty); }
			set { SetValue(IsShakeProperty, value); }
		}
		public static readonly DependencyProperty IsShakeProperty =
			DependencyProperty.Register(nameof(IsShake), typeof(bool), typeof(RD_ShakerRack), new PropertyMetadata(false, new PropertyChangedCallback(Callback_IsShake)));

		public RD_ShakerRack()
		{
			InitializeComponent();
		}

		private static void Callback_IsShake(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var sharkerRack = (RD_ShakerRack)d;

			if (sharkerRack.storyboard == null) sharkerRack.storyboard = sharkerRack.Resources[Properties.Resources.ShakeAnimation] as System.Windows.Media.Animation.Storyboard;
			
			var shakerModule = (FrameworkElement)sharkerRack.Template.FindName(Properties.Resources.StackPanel_ShakerModule, sharkerRack);

			if ((bool)e.NewValue == true)
			{
				sharkerRack.storyboard.RepeatBehavior = new System.Windows.Media.Animation.RepeatBehavior(1);

				sharkerRack.storyboard.Completed += (sender, eventArgs) =>
				{
					if (sharkerRack.IsShake) sharkerRack.storyboard.Begin(shakerModule);
				};
				sharkerRack.storyboard.Begin(shakerModule);
			}
		}
	}
}
