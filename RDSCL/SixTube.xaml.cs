using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDSCL
{
	/// <summary>
	/// SixTube.xaml 的交互逻辑
	/// </summary>
	public partial class SixTube : UserControl
    {
		List<SingleTube> singleTubes = new List<SingleTube>(6);



		public bool test
		{
			get { return (bool)GetValue(testProperty); }
			set { SetValue(testProperty, value); }
		}

		// Using a DependencyProperty as the backing store for test.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty testProperty =
			DependencyProperty.Register("test", typeof(bool), typeof(SixTube), new PropertyMetadata(false,new PropertyChangedCallback(Callback_TubeState)));



		private static void Callback_TubeState(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SixTube sixTube = d as SixTube;
			bool tubeStates = (bool)e.NewValue;
			var child = sixTube.FindVisualChild<SingleTube>(sixTube);
			if (tubeStates) child.ContentColor = new SolidColorBrush(Colors.Blue);
			else child.ContentColor = new SolidColorBrush(Colors.Gray);
		}
		private childItem FindVisualChild<childItem>(DependencyObject obj)where childItem : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem)
					return (childItem)child;
				else
				{
					childItem childOfChild = FindVisualChild<childItem>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
		}

		public string NumberValue
		{
			get { return (string)GetValue(NumberValueProperty); }
			set { SetValue(NumberValueProperty, value); }
		}
		public static readonly DependencyProperty NumberValueProperty =
			DependencyProperty.Register(nameof(NumberValue), typeof(string), typeof(SixTube), new PropertyMetadata("0"));

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
				typeof(SixTube), 
				new PropertyMetadata(new SolidColorBrush(Color.FromRgb(40,60,250)))
			);



		public Visibility BodyVisibility
		{
			get { return (Visibility)GetValue(BodyVisibilityProperty); }
			set { SetValue(BodyVisibilityProperty, value); }
		}
		public static readonly DependencyProperty BodyVisibilityProperty =
			DependencyProperty.Register(nameof(BodyVisibility), typeof(Visibility), typeof(SixTube), new PropertyMetadata(Visibility.Visible));



		public SolidColorBrush BackgroundColor
		{
			get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}
		public static readonly DependencyProperty BackgroundColorProperty =
			DependencyProperty.Register(nameof(BackgroundColor), typeof(SolidColorBrush), typeof(SixTube), new PropertyMetadata(new SolidColorBrush(Colors.White)));



		public DoubleCollection FrameStyle
		{
			get { return (DoubleCollection)GetValue(FrameStyleProperty); }
			set { SetValue(FrameStyleProperty, value); }
		}
		public static readonly DependencyProperty FrameStyleProperty =
			DependencyProperty.Register(nameof(FrameStyle), typeof(DoubleCollection), typeof(SixTube), new PropertyMetadata(default(DoubleCollection)));




		public SixTubeState CurrentState
		{
			get { return (SixTubeState)GetValue(CurrentStateProperty); }
			set { SetValue(CurrentStateProperty, value); }
		}
		public static readonly DependencyProperty CurrentStateProperty =
			DependencyProperty.Register(nameof(CurrentState), typeof(SixTubeState), typeof(SixTube), new PropertyMetadata(SixTubeState.Leaving,new PropertyChangedCallback(Callback_CurrentState)));

		private static void Callback_CurrentState(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if(e.NewValue!=null)
			{ 
			SixTube sixTube = d as SixTube;
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

		public SixTube()
        {
            InitializeComponent();
			//this.singleTubes.Add((SingleTube)this.FindName("SingleTube1"));
			//this.singleTubes.Add((SingleTube)this.FindName("SingleTube2"));
			//this.singleTubes.Add((SingleTube)this.FindName("SingleTube3"));
			//this.singleTubes.Add((SingleTube)this.FindName("SingleTube4"));
			//this.singleTubes.Add((SingleTube)this.FindName("SingleTube5"));
			//this.singleTubes.Add((SingleTube)this.FindName("SingleTube6"));
		}
    }

	public enum SixTubeState
	{
		Inexistence = 0,
		Existence = 1,
		Leaving = 2
	}
}
