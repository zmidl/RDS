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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RDSCL
{
	/// <summary>
	/// RD_Strip.xaml 的交互逻辑
	/// </summary>
	public partial class RD_Strip : UserControl
	{
		public string Number
		{
			get { return (string)GetValue(NumberValueProperty); }
			set { SetValue(NumberValueProperty, value); }
		}
		public static readonly DependencyProperty NumberValueProperty =
			DependencyProperty.Register(nameof(Number), typeof(string), typeof(RD_Strip), new PropertyMetadata(Properties.Resources.StringZero));

		public Visibility NumberVisibility
		{
			get { return (Visibility)GetValue(NumberVisibilityProperty); }
			set { SetValue(NumberVisibilityProperty, value); }
		}
		public static readonly DependencyProperty NumberVisibilityProperty =
			DependencyProperty.Register(nameof(NumberVisibility), typeof(Visibility), typeof(RD_Strip), new PropertyMetadata(Visibility.Hidden));

		public IEnumerable Cells
		{
			get { return (IEnumerable)GetValue(CellsProperty); }
			set { SetValue(CellsProperty, value); }
		}
		public static readonly DependencyProperty CellsProperty =
			DependencyProperty.Register(nameof(Cells), typeof(IEnumerable), typeof(RD_Strip), new PropertyMetadata(null));

		public bool? CurrentState
		{
			get { return (bool)GetValue(CurrentStateProperty); }
			set { SetValue(CurrentStateProperty, value); }
		}
		public static readonly DependencyProperty CurrentStateProperty =
			DependencyProperty.Register(nameof(CurrentState), typeof(bool?), typeof(RD_Strip), new PropertyMetadata(null, new PropertyChangedCallback(Callback_CurrentState)));

		public bool IsMoving
		{
			get { return (bool)GetValue(IsMovingProperty); }
			set { SetValue(IsMovingProperty, value); }
		}
		public static readonly DependencyProperty IsMovingProperty =
			DependencyProperty.Register(nameof(IsMoving), typeof(bool), typeof(RD_Strip), new PropertyMetadata(false,new PropertyChangedCallback(Callback_IsMoving)));

		private static void Callback_IsMoving(DependencyObject d,DependencyPropertyChangedEventArgs e)
		{
			var strip = (RD_Strip)d;
			var from = (Path)strip.Template.FindName(Properties.Resources.Path_From, strip);
			var to = (Path)strip.Template.FindName(Properties.Resources.Path_To, strip);
			if (((bool)e.NewValue) == true)
			{
				if (strip.CurrentState == true)
				{
					from.Visibility = Visibility.Visible;
					to.Visibility = Visibility.Hidden;
				}
				else
				{
					from.Visibility = Visibility.Hidden;
					to.Visibility = Visibility.Visible;
				}
			}
			else
			{
				from.Visibility = Visibility.Hidden;
				to.Visibility = Visibility.Hidden;
			}
		}

		private static void Callback_CurrentState(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				var strip = d as RD_Strip;
				var currentState = (bool)e.NewValue;
				var frame = strip.Template.FindName("Path_Frame",strip) as Path;
				var blue = strip.FindResource("BlueColor") as SolidColorBrush;
				var wathetColor = strip.FindResource("WathetColor2") as SolidColorBrush;
				var body = strip.Template.FindName("StackPanel_Body", strip) as Panel;
				switch (currentState)
				{
					case true: { frame.Fill=blue;body.Visibility = Visibility.Visible; strip.NumberVisibility = Visibility.Visible; break; }
					case false: { frame.Fill = wathetColor; body.Visibility = Visibility.Hidden; strip.NumberVisibility = Visibility.Hidden; break; }
					default: { break; }
				}
			}
		}



		public RD_Strip()
		{
			InitializeComponent();
		}
	}
}
