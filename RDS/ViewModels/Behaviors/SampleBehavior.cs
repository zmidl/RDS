using RDS.ViewModels.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	class SampleBehavior:Behavior<UIElement>
	{
		public MultipeSelection MultipSelection
		{
			get { return (MultipeSelection)GetValue(MultipSelectionProperty); }
			set { SetValue(MultipSelectionProperty, value); }
		}
		public static readonly DependencyProperty MultipSelectionProperty =
			DependencyProperty.Register(nameof(MultipSelection), typeof(MultipeSelection), typeof(SampleBehavior), new PropertyMetadata(MultipeSelection.ColumnA));

		public SampleViewModel ViewModel
		{
			get { return (SampleViewModel)GetValue(TestProperty); }
			set { SetValue(TestProperty, value); }
		}
		public static readonly DependencyProperty TestProperty =
			DependencyProperty.Register(nameof(ViewModel), typeof(SampleViewModel), typeof(SampleBehavior), new PropertyMetadata(default(SampleViewModel)));

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.PreviewMouseUp += AssociatedObject_PreviewMouseUp;
		}

		private void AssociatedObject_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.ViewModel.MultipleSetSampleSelectionState(this.MultipSelection);
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.PreviewMouseUp -= AssociatedObject_PreviewMouseUp;
		}
	}
}
