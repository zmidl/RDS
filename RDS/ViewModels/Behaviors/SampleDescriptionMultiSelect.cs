using RDSCL;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace RDS.ViewModels.Behaviors
{
	public class SampleDescriptionMultiSelect : Behavior<UIElement>
	{
		public SampleViewModel ViewModel
		{
			get { return (SampleViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
		   DependencyProperty.Register(nameof(ViewModel), typeof(SampleViewModel), typeof(SampleDescriptionMultiSelect), new PropertyMetadata(null));

		protected override void OnAttached()
		{
			base.OnAttached();
		}
		protected override void OnDetaching()
		{
			base.OnDetaching();
	
		}
	}
}
