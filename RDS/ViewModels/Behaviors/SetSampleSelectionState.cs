using RDS.ViewModels.Common;
using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	class SetSampleSelectionState:Behavior<UIElement>
	{
		public MultipeSelection MultipSelection
		{
			get { return (MultipeSelection)GetValue(MultipSelectionProperty); }
			set { SetValue(MultipSelectionProperty, value); }
		}
		public static readonly DependencyProperty MultipSelectionProperty =
			DependencyProperty.Register(nameof(MultipSelection), typeof(MultipeSelection), typeof(SetSampleSelectionState), new PropertyMetadata(MultipeSelection.ColumnA));

		public SampleViewModel ViewModel
		{
			get { return (SampleViewModel)GetValue(TestProperty); }
			set { SetValue(TestProperty, value); }
		}
		public static readonly DependencyProperty TestProperty =
			DependencyProperty.Register(nameof(ViewModel), typeof(SampleViewModel), typeof(SetSampleSelectionState), new PropertyMetadata(default(SampleViewModel)));

		protected override void OnAttached()
		{
			base.OnAttached();

			this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
		}

		private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.ViewModel.MultipleSetSampleSelectionState(this.MultipSelection);
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
		}
	}
}
