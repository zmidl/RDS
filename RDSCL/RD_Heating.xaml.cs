using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RDSCL
{
	/// <summary>
	/// WarmUpRack.xaml 的交互逻辑
	/// </summary>
	public partial class RD_Heating : UserControl
	{
		Storyboard storyboard;

		public object DataSource
		{
			get { return (object)GetValue(DataSourceProperty); }
			set { SetValue(DataSourceProperty, value); }
		}
		public static readonly DependencyProperty DataSourceProperty =
			DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(RD_Heating), new PropertyMetadata(null));

		public bool IsTwinkle
		{
			get { return (bool)GetValue(IsTwinkleProperty); }
			set { SetValue(IsTwinkleProperty, value); }
		}
		public static readonly DependencyProperty IsTwinkleProperty =
			DependencyProperty.Register(nameof(IsTwinkle), typeof(bool), typeof(RD_Heating), new PropertyMetadata(false, new PropertyChangedCallback(Callback_IsTwinkle)));

		private static void Callback_IsTwinkle(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var rD_Heating = (RD_Heating)d;

			if (rD_Heating.storyboard == null) rD_Heating.storyboard = rD_Heating.Resources[Properties.Resources.TwinkleAnimation] as Storyboard;

			var twinkleModule = (FrameworkElement)rD_Heating.Template.FindName(Properties.Resources.ReagentContent, rD_Heating);

			if ((bool)e.NewValue == true)
			{
				rD_Heating.storyboard.RepeatBehavior = new RepeatBehavior(1);
				
				rD_Heating.storyboard.Completed += (sender, eventArgs) =>
				{
					if (rD_Heating.IsTwinkle) rD_Heating.storyboard.Begin(twinkleModule);
				};
				rD_Heating.storyboard.Begin(twinkleModule);
			}
		}

		public RD_Heating()
		{
			InitializeComponent();
		}
	}
}
