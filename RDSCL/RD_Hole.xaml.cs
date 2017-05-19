using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RDSCL
{
	/// <summary>
	/// RD_Hole.xaml 的交互逻辑
	/// </summary>
	public partial class RD_Hole : UserControl
	{
		//Storyboard storyboard;

		private static SolidColorBrush DefaultContentColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2AD0D0D0"));

		public Brush ExcircleColor
		{
			get { return (SolidColorBrush)GetValue(ExcircleColorProperty); }
			set { SetValue(ExcircleColorProperty, value); }
		}
		public static readonly DependencyProperty ExcircleColorProperty =
			DependencyProperty.Register(nameof(ExcircleColor), typeof(Brush), typeof(RD_Hole), new PropertyMetadata(new SolidColorBrush(Colors.White)));

		public Brush ContentColor
		{
			get { return (Brush)GetValue(ContentColorProperty); }
			set { SetValue(ContentColorProperty, value); }
		}
		public static readonly DependencyProperty ContentColorProperty =
			DependencyProperty.Register(nameof(ContentColor), typeof(Brush), typeof(RD_Hole), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));


		public Brush ContentColorMask
		{
			get { return (Brush)GetValue(ContentColorMaskProperty); }
			set { SetValue(ContentColorMaskProperty, value); }
		}
		public static readonly DependencyProperty ContentColorMaskProperty =
			DependencyProperty.Register(nameof(ContentColorMask), typeof(Brush), typeof(RD_Hole), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));




		public bool IsTwinkle
		{
			get { return (bool)GetValue(IsTwinkleProperty); }
			set { SetValue(IsTwinkleProperty, value); }
		}
		public static readonly DependencyProperty IsTwinkleProperty =
			DependencyProperty.Register(nameof(IsTwinkle), typeof(bool), typeof(RD_Hole), new PropertyMetadata(false, new PropertyChangedCallback(Callback_IsTwinkle)));

		private static void Callback_IsTwinkle(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			//var rD_Hole = (RD_Hole)d;

			//if (rD_Hole.storyboard == null) rD_Hole.storyboard = rD_Hole.Resources[Properties.Resources.TwinkleAnimation] as Storyboard;

			//var twinkleModule = (Canvas)rD_Hole.Template.FindName(Properties.Resources.ReagentContent, rD_Hole);

			//if ((bool)e.NewValue == true)
			//{
			//	rD_Hole.storyboard.RepeatBehavior = new RepeatBehavior(1);

			//	rD_Hole.storyboard.Completed += (sender, eventArgs) =>
			//	{
			//		if (rD_Hole.IsTwinkle) rD_Hole.storyboard.Begin(twinkleModule);
			//	};
			//	rD_Hole.storyboard.Begin(twinkleModule);
			//}


			
			var rD_Hole = (RD_Hole)d;

			var storyboard = rD_Hole.Resources[Properties.Resources.TwinkleAnimation] as Storyboard;

			var twinkleModule = (Canvas)rD_Hole.Template.FindName(Properties.Resources.ReagentContent, rD_Hole);

			if ((bool)e.NewValue == true)
			{
				storyboard.RepeatBehavior = new RepeatBehavior(1);

				storyboard.Completed += (sender, eventArgs) =>
				{
					if (rD_Hole.IsTwinkle) storyboard.Begin(twinkleModule);
				};
				storyboard.Begin(twinkleModule);
			}
		}

		public RD_Hole()
		{
			InitializeComponent();
		}
	}
}
