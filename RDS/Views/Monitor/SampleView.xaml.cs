using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using RDS.ViewModels;
using System.Windows.Media;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// SampleView.xaml 的交互逻辑
	/// </summary>
	public partial class SampleView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }

		public SampleViewModel ViewModel { get { return this.DataContext as SampleViewModel; } }

		public SampleView()
		{
			InitializeComponent();
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);
			this.ViewModel.ViewChanged += ViewModel_ViewChanged;
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			((IExitView)this).ExitView();
		}

	
	}
}
