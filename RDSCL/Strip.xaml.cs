using System;
using System.Collections;
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
	public partial class Strip : UserControl
	{
		private static SolidColorBrush NumverNormalColor = new SolidColorBrush(Colors.RosyBrown);

		public Strip()
		{
			InitializeComponent();
		}

		public IEnumerable HolesContentColor
		{
			get { return (IEnumerable)GetValue(HolesContentColorProperty); }
			set { SetValue(HolesContentColorProperty, value); }
		}
		public static readonly DependencyProperty HolesContentColorProperty =
			DependencyProperty.Register(nameof(HolesContentColor), typeof(IEnumerable), typeof(Strip), new PropertyMetadata(null));

		public string Number
		{
			get { return (string)GetValue(NumberValueProperty); }
			set { SetValue(NumberValueProperty, value); }
		}
		public static readonly DependencyProperty NumberValueProperty =
			DependencyProperty.Register(nameof(Number), typeof(string), typeof(Strip), new PropertyMetadata("0"));

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
				typeof(Strip),
				new PropertyMetadata(Strip.NumverNormalColor)
			);



		public Visibility BodyVisibility
		{
			get { return (Visibility)GetValue(BodyVisibilityProperty); }
			set { SetValue(BodyVisibilityProperty, value); }
		}
		public static readonly DependencyProperty BodyVisibilityProperty =
			DependencyProperty.Register(nameof(BodyVisibility), typeof(Visibility), typeof(Strip), new PropertyMetadata(Visibility.Visible));



		public SolidColorBrush BackgroundColor
		{
			get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}
		public static readonly DependencyProperty BackgroundColorProperty =
			DependencyProperty.Register(nameof(BackgroundColor), typeof(SolidColorBrush), typeof(Strip), new PropertyMetadata(new SolidColorBrush(Colors.White)));



		public DoubleCollection FrameStyle
		{
			get { return (DoubleCollection)GetValue(FrameStyleProperty); }
			set { SetValue(FrameStyleProperty, value); }
		}
		public static readonly DependencyProperty FrameStyleProperty =
			DependencyProperty.Register(nameof(FrameStyle), typeof(DoubleCollection), typeof(Strip), new PropertyMetadata(default(DoubleCollection)));

		public StripState CurrentState
		{
			get { return (StripState)GetValue(CurrentStateProperty); }
			set { SetValue(CurrentStateProperty, value); }
		}
		public static readonly DependencyProperty CurrentStateProperty =
			DependencyProperty.Register(nameof(CurrentState), typeof(StripState), typeof(Strip), new PropertyMetadata(StripState.Leaving, new PropertyChangedCallback(Callback_CurrentState)));

		private static void Callback_CurrentState(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				Strip strip = d as Strip;
				var currentState = (StripState)e.NewValue;
				switch (currentState)
				{
					case StripState.Existence:
					{
						strip.BodyVisibility = Visibility.Visible;
						strip.BackgroundColor = new SolidColorBrush(Colors.White);
						strip.FrameStyle = default(DoubleCollection);
						strip.Opacity = 1.0;
						strip.NumberColor = Strip.NumverNormalColor;
						break;
					}
					case StripState.Inexistence:
					{
						strip.BodyVisibility = Visibility.Hidden;
						strip.BackgroundColor = new SolidColorBrush(Colors.Transparent);
						strip.FrameStyle = new DoubleCollection() { 2 };
						strip.Opacity = 1.0;
						strip.NumberColor = new SolidColorBrush(Colors.Transparent);
						break;
					}
					case StripState.Leaving:
					{
						strip.BodyVisibility = Visibility.Visible;
						strip.BackgroundColor = new SolidColorBrush(Colors.White);
						strip.FrameStyle = default(DoubleCollection);
						strip.Opacity = 0.5;
						strip.NumberColor = new SolidColorBrush(Colors.Gray);
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

	public enum StripState
	{
		Inexistence = 0,
		Existence = 1,
		Leaving = 2
	}
}
