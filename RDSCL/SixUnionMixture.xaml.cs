using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RDSCL
{
	/// <summary>
	/// SixTube.xaml 的交互逻辑
	/// </summary>
	public partial class SixUnionMixture : UserControl
	{
		public SixUnionMixture()
		{
			InitializeComponent();
		}

		public IEnumerable<SolidColorBrush	> HolesContentColor
		{
			get { return (IEnumerable<SolidColorBrush>)GetValue(HolesContentColorProperty); }
			set { SetValue(HolesContentColorProperty, value); }
		}
		public static readonly DependencyProperty HolesContentColorProperty =
			DependencyProperty.Register(nameof(HolesContentColor), typeof(IEnumerable<SolidColorBrush>), typeof(SixUnionMixture), new PropertyMetadata(null));

		public string NumberValue
		{
			get { return (string)GetValue(NumberValueProperty); }
			set { SetValue(NumberValueProperty, value); }
		}
		public static readonly DependencyProperty NumberValueProperty =
			DependencyProperty.Register(nameof(NumberValue), typeof(string), typeof(SixUnionMixture), new PropertyMetadata("0"));

		public SolidColorBrush NumberColor
		{
			get { return (SolidColorBrush)GetValue(NumberColorProperty); }
			set { SetValue(NumberColorProperty, value); }
		}
		public static readonly DependencyProperty NumberColorProperty =
			DependencyProperty.Register
			(
				nameof(NumberColor),
				typeof(SolidColorBrush),
				typeof(SixUnionMixture),
				new PropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 60, 250)))
			);



		public Visibility BodyVisibility
		{
			get { return (Visibility)GetValue(BodyVisibilityProperty); }
			set { SetValue(BodyVisibilityProperty, value); }
		}
		public static readonly DependencyProperty BodyVisibilityProperty =
			DependencyProperty.Register(nameof(BodyVisibility), typeof(Visibility), typeof(SixUnionMixture), new PropertyMetadata(Visibility.Visible));



		public SolidColorBrush BackgroundColor
		{
			get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}
		public static readonly DependencyProperty BackgroundColorProperty =
			DependencyProperty.Register(nameof(BackgroundColor), typeof(SolidColorBrush), typeof(SixUnionMixture), new PropertyMetadata(new SolidColorBrush(Colors.White)));



		public DoubleCollection FrameStyle
		{
			get { return (DoubleCollection)GetValue(FrameStyleProperty); }
			set { SetValue(FrameStyleProperty, value); }
		}
		public static readonly DependencyProperty FrameStyleProperty =
			DependencyProperty.Register(nameof(FrameStyle), typeof(DoubleCollection), typeof(SixUnionMixture), new PropertyMetadata(default(DoubleCollection)));

		public SixTubeState CurrentState
		{
			get { return (SixTubeState)GetValue(CurrentStateProperty); }
			set { SetValue(CurrentStateProperty, value); }
		}
		public static readonly DependencyProperty CurrentStateProperty =
			DependencyProperty.Register(nameof(CurrentState), typeof(SixTubeState), typeof(SixUnionMixture), new PropertyMetadata(SixTubeState.Leaving, new PropertyChangedCallback(Callback_CurrentState)));

		private static void Callback_CurrentState(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				SixUnionMixture sixTube = d as SixUnionMixture;
				var currentState = (SixTubeState)e.NewValue;
				switch (currentState)
				{
					case SixTubeState.Existence:
					{
						sixTube.BodyVisibility = Visibility.Visible;
						sixTube.BackgroundColor = new SolidColorBrush(Colors.White);
						sixTube.FrameStyle = default(DoubleCollection);
						sixTube.Opacity = 1.0;
						sixTube.NumberColor = new SolidColorBrush(Color.FromRgb(40, 60, 250));
						break;
					}
					case SixTubeState.Inexistence:
					{
						sixTube.BodyVisibility = Visibility.Hidden;
						sixTube.BackgroundColor = new SolidColorBrush(Colors.Transparent);
						sixTube.FrameStyle = new DoubleCollection() { 2 };
						sixTube.Opacity = 1.0;
						sixTube.NumberColor = new SolidColorBrush(Colors.Transparent);
						break;
					}
					case SixTubeState.Leaving:
					{
						sixTube.BodyVisibility = Visibility.Visible;
						sixTube.BackgroundColor = new SolidColorBrush(Colors.White);
						sixTube.FrameStyle = default(DoubleCollection);
						sixTube.Opacity = 0.5;
						sixTube.NumberColor = new SolidColorBrush(Colors.Gray);
						break;
					}
					default:
					{
						break;
					}
				}
			}
		}
	}

	public enum SixTubeState
	{
		Inexistence = 0,
		Existence = 1,
		Leaving = 2
	}
}
