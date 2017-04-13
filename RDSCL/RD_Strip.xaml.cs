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
			DependencyProperty.Register(nameof(Number), typeof(string), typeof(RD_Strip), new PropertyMetadata("0"));

		public IEnumerable Cells
		{
			get { return (IEnumerable)GetValue(CellsProperty); }
			set { SetValue(CellsProperty, value); }
		}
		public static readonly DependencyProperty CellsProperty =
			DependencyProperty.Register(nameof(Cells), typeof(IEnumerable), typeof(RD_Strip), new PropertyMetadata(null));

		public StripState CurrentState
		{
			get { return (StripState)GetValue(CurrentStateProperty); }
			set { SetValue(CurrentStateProperty, value); }
		}
		public static readonly DependencyProperty CurrentStateProperty =
			DependencyProperty.Register(nameof(CurrentState), typeof(StripState), typeof(RD_Strip), new PropertyMetadata(StripState.Leaving, new PropertyChangedCallback(Callback_CurrentState)));

		private static void Callback_CurrentState(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				var strip = d as RD_Strip;
				var currentState = (StripState)e.NewValue;
				switch (currentState)
				{
					case StripState.Existence: { strip.Visibility = Visibility.Visible; strip.Opacity = 1.0; break; }
					case StripState.Inexistence: { strip.Visibility = Visibility.Hidden; break; }
					case StripState.Leaving: { strip.Visibility = Visibility.Visible; strip.Opacity = 0.4; break; }
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
