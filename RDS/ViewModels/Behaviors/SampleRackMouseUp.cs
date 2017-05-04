
using RDS.ViewModels.Common;
using System.Windows;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Behaviors
{
	class SampleRackMouseUp : Behavior<UIElement>
	{
		public SampleRackIndex SampleRackIndex
		{
			get { return (SampleRackIndex)GetValue(SampleRackProperty); }
			set { SetValue(SampleRackProperty, value); }
		}
		public static readonly DependencyProperty SampleRackProperty =
			DependencyProperty.Register(nameof(SampleRackIndex), typeof(SampleRackIndex), typeof(SampleRackMouseUp), new PropertyMetadata(SampleRackIndex.RackA));

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
			this.ViewModel.SetSampleRackState(new SampleRackStateArgs(this.SampleRackIndex, RDSCL.SampleRackState.PrepareSample));
			this.ViewModel.CurrentSampleRackIndex = this.SampleRackIndex;
			//this.ViewModel.GetLisTableFromFile();

			//this.ViewModel.DatatableToEntity();
		}

		protected override void OnDetaching()
		{
			base.OnDetaching(); this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
		}
	}
}
