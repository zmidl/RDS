
using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	class SampleRackMouseUp : Behavior<UIElement>
	{
		public int Index
		{
			get { return (int)GetValue(IndexProperty); }
			set { SetValue(IndexProperty, value); }
		}
		public static readonly DependencyProperty IndexProperty =
			DependencyProperty.Register(nameof(Index), typeof(int), typeof(SampleRackMouseUp), new PropertyMetadata(0));

		public SampleViewModel ViewModel
		{
			get { return (SampleViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
		   DependencyProperty.Register(nameof(ViewModel), typeof(SampleViewModel), typeof(SampleRackMouseUp), new PropertyMetadata(null));

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
		}

		private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.ViewModel.SetSampleRackState(new SampleRackStateArgs(this.Index, RDSCL.SampleRackState.PrepareSample));
			this.ViewModel.CurrentColumnIndex = this.Index;
			//this.ViewModel.GetLisTableFromFile();

			//this.ViewModel.DatatableToEntity();
		}

		protected override void OnDetaching()
		{
			base.OnDetaching(); this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
		}
	}
}
