using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	class SampleViewMouseUp:Behavior<UIElement>
	{

		public SampleViewModel Test
		{
			get { return (SampleViewModel)GetValue(TestProperty); }
			set { SetValue(TestProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Test.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TestProperty =
			DependencyProperty.Register("Test", typeof(SampleViewModel), typeof(SampleViewMouseUp), new PropertyMetadata(default(SampleViewModel)));





		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.PreviewMouseUp += AssociatedObject_PreviewMouseUp;
		}

		private void AssociatedObject_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			MessageBox.Show(Test.SampleDescritions.Count.ToString());
			//throw new System.NotImplementedException();
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.PreviewMouseUp -= AssociatedObject_PreviewMouseUp;
		}
	}
}
