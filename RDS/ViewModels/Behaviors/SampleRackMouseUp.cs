
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

		public object ViewModel
		{
			get { return GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
		   DependencyProperty.Register(nameof(ViewModel), typeof(object), typeof(SampleRackMouseUp), new PropertyMetadata(null));

		protected override void OnAttached()
		{
			base.OnAttached();
			this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
		}

		private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (sender is RDSCL.RD_SampleRack)
			{
				var viewModel = (SampleViewModel)this.ViewModel;
				viewModel.SetSampleRackState(new SampleRackStateArgs(this.SampleRackIndex, RDSCL.SampleRackState.PrepareSample));
				viewModel.CurrentSampleRackIndex = this.SampleRackIndex;
			}
			else
			{
				var viewModel = (MonitorViewModel)this.ViewModel;
				viewModel.ShowSampleView();
			}
		}

		protected override void OnDetaching()
		{
			base.OnDetaching(); this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
		}
	}
}
