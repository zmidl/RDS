using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	public class SampleInformationDrop : Behavior<UIElement>
	{
		public SampleViewModel ViewModel
		{
			get { return (SampleViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(nameof(ViewModel), typeof(SampleViewModel), typeof(SampleInformationDrop), new PropertyMetadata(null));


		public SampleInformationDrop()
		{

		}

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.Drop += AssociatedObject_Drop;
			this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.Drop -= AssociatedObject_Drop;
			this.AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
		}

		private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
		{
		
		}

		private void AssociatedObject_Drop(object sender, DragEventArgs e)
		{
			
		}
	}
}
