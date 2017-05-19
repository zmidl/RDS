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
	/// CupRack.xaml 的交互逻辑
	/// </summary>
	public partial class RD_CupRack : UserControl
	{
		public object DataSource
		{
			get { return GetValue(DataSourceProperty); }
			set { SetValue(DataSourceProperty, value); }
		}
		public static readonly DependencyProperty DataSourceProperty =
			DependencyProperty.Register(nameof(DataSource), typeof(object), typeof(RD_CupRack), new PropertyMetadata(null));


		public bool IsTwinkle
		{
			get { return (bool)GetValue(IsTwinkleProperty); }
			set { SetValue(IsTwinkleProperty, value); }
		}
		public static readonly DependencyProperty IsTwinkleProperty =
			DependencyProperty.Register(nameof(IsTwinkle), typeof(bool), typeof(RD_CupRack), new PropertyMetadata(false, new PropertyChangedCallback(Callback_IsTwinkle)));

		private static void Callback_IsTwinkle(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var rD_CupRack = (RD_CupRack)d;

			var storyboard = rD_CupRack.Resources[Properties.Resources.TwinkleAnimation] as Storyboard;

			var twinkleModule = (FrameworkElement)rD_CupRack;

			if ((bool)e.NewValue == true)
			{
			    ((FrameworkElement)rD_CupRack.Template.FindName(Properties.Resources.Strip0, rD_CupRack)).Opacity = 1;
				((FrameworkElement)rD_CupRack.Template.FindName(Properties.Resources.Strip1, rD_CupRack)).Opacity = 1;
				((FrameworkElement)rD_CupRack.Template.FindName(Properties.Resources.Strip2, rD_CupRack)).Opacity = 1;
				((FrameworkElement)rD_CupRack.Template.FindName(Properties.Resources.Strip3, rD_CupRack)).Opacity = 1;
				((FrameworkElement)rD_CupRack.Template.FindName(Properties.Resources.Strip4, rD_CupRack)).Opacity = 1;
				((FrameworkElement)rD_CupRack.Template.FindName(Properties.Resources.Strip5, rD_CupRack)).Opacity = 1;
				((FrameworkElement)rD_CupRack.Template.FindName(Properties.Resources.Strip6, rD_CupRack)).Opacity = 1;

				storyboard.RepeatBehavior = new RepeatBehavior(1);

				storyboard.Completed += (sender, eventArgs) =>
				{
					if (rD_CupRack.IsTwinkle) storyboard.Begin(twinkleModule);
				};
				storyboard.Begin(twinkleModule);
			}
		}

		public RD_CupRack()
		{
			InitializeComponent();
		}
	}
}
