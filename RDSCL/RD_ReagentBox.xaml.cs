using System;
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
	/// RD_ReagentBox.xaml 的交互逻辑
	/// </summary>
	public partial class RD_ReagentBox : UserControl
	{
		Storyboard storyboard;

		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(nameof(Value), typeof(double), typeof(RD_ReagentBox), new PropertyMetadata(0d));


		public SolidColorBrush ContentColor
		{
			get { return (SolidColorBrush)GetValue(ContentColorProperty); }
			set { SetValue(ContentColorProperty, value); }
		}
		public static readonly DependencyProperty ContentColorProperty =
			DependencyProperty.Register(nameof(ContentColor), typeof(SolidColorBrush), typeof(RD_ReagentBox), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));


		public bool IsTwinkle
		{
			get { return (bool)GetValue(IsTwinkleProperty); }
			set { SetValue(IsTwinkleProperty, value); }
		}
		public static readonly DependencyProperty IsTwinkleProperty =
			DependencyProperty.Register(nameof(IsTwinkle), typeof(bool), typeof(RD_ReagentBox), new PropertyMetadata(false, new PropertyChangedCallback(Callback_IsTwinkle)));

		private static void Callback_IsTwinkle(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var rD_ReagentBox = (RD_ReagentBox)d;

			if (rD_ReagentBox.storyboard == null) rD_ReagentBox.storyboard = rD_ReagentBox.Resources[Properties.Resources.TwinkleAnimation] as Storyboard;

			var twinkleModule = (FrameworkElement)rD_ReagentBox.Template.FindName(Properties.Resources.ReagentContent,rD_ReagentBox);

			if ((bool)e.NewValue == true)
			{
				rD_ReagentBox.storyboard.RepeatBehavior = new RepeatBehavior(1);

				rD_ReagentBox.storyboard.Completed += (sender, eventArgs) =>
				{
					if (rD_ReagentBox.IsTwinkle) rD_ReagentBox.storyboard.Begin(twinkleModule);
				};
				rD_ReagentBox.storyboard.Begin(twinkleModule);
			}
		}

		public RD_ReagentBox()
		{
			InitializeComponent();
		}
	}
}
