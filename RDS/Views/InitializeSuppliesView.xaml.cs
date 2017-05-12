using System;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using System.Windows.Media.Animation;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace RDS.Views
{
	/// <summary>
	/// InitializeSuppliesView.xaml 的交互逻辑
	/// </summary>
	public partial class InitializeSuppliesView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }

		public ViewModels.InitializeSuppliesViewModel ViewModel { get { return this.DataContext as ViewModels.InitializeSuppliesViewModel; } }

		public InitializeSuppliesView()
		{
			InitializeComponent();

			this.DataContext = new ViewModels.InitializeSuppliesViewModel();

			this.ViewModel.ViewChanged += ViewModel_ViewChanged;

			this.BeginAnimation();
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			var args = (ViewModels.InitializeSuppliesViewModel.InitializeSuppliesViewChangedArgs)e;
			switch (args.Option)
			{
				case ViewModels.InitializeSuppliesViewModel.ViewChangedOption.EnterMonitorView:
				{
					Monitor.MonitorView monitorView = new Monitor.MonitorView();
					General.ShowCricleProgress();

					Task task2 = new Task(() =>
					{
						Thread.Sleep(500);
						this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { this.Content = monitorView; General.HideCircleProgress(); }));
					});
					task2.Start();
					break;
				}
				case ViewModels.InitializeSuppliesViewModel.ViewChangedOption.ChangeBeadSelectionState:
				{
					var state = (ViewModels.InitializeSuppliesViewModel.BeadPlace)args.Value;
					switch (state)
					{
						case ViewModels.InitializeSuppliesViewModel.BeadPlace.Left:
						{
							this.Rectangle_Bead.Visibility = Visibility.Visible;
							this.Rectangle_Bead.Width = 35;
							Canvas.SetLeft(this.Rectangle_Bead, 8);
							break;
						}
						case ViewModels.InitializeSuppliesViewModel.BeadPlace.Right:
						{
							this.Rectangle_Bead.Visibility = Visibility.Visible;
							this.Rectangle_Bead.Width = 35;
							Canvas.SetLeft(this.Rectangle_Bead, 33);
							break;
						}
						case ViewModels.InitializeSuppliesViewModel.BeadPlace.Both:
						{
							this.Rectangle_Bead.Visibility = Visibility.Visible;
							this.Rectangle_Bead.Width = 61;
							Canvas.SetLeft(this.Rectangle_Bead, 8);
							break;
						}
						default: break;
					}
					break;
				}
			}
		}

		private void BeginAnimation(System.Windows.Shapes.Shape shape)
		{
			DoubleAnimation da = new DoubleAnimation();
			da.From = 1;
			da.To = 0.1;
			da.RepeatBehavior = RepeatBehavior.Forever;
			da.AutoReverse = true;
			da.Duration = TimeSpan.FromMilliseconds(600);
			shape.BeginAnimation(UIElement.OpacityProperty, da);
		}

		private void BeginAnimation()
		{
			this.BeginAnimation(this.Rectangle_Olefin);
			this.BeginAnimation(this.Rectangle_300TipRacks);
			this.BeginAnimation(this.Rectangle_1000TipRacks);
			this.BeginAnimation(this.Rectangle_Strips);
			this.BeginAnimation(this.Rectangle_Bead);

			this.BeginAnimation(this.Ellipse_1);
			this.BeginAnimation(this.Ellipse_2);
			this.BeginAnimation(this.Ellipse_3);
			this.BeginAnimation(this.Ellipse_4);
			this.BeginAnimation(this.Ellipse_5);

			this.BeginAnimation(this.Ellipse_6);
			this.BeginAnimation(this.Ellipse_7);
			this.BeginAnimation(this.Ellipse_8);
			this.BeginAnimation(this.Ellipse_9);
			this.BeginAnimation(this.Ellipse_10);

			this.BeginAnimation(this.Ellipse_11);
			this.BeginAnimation(this.Ellipse_12);
			this.BeginAnimation(this.Ellipse_13);
			this.BeginAnimation(this.Ellipse_14);
			this.BeginAnimation(this.Ellipse_15);

			this.BeginAnimation(this.Ellipse_16);
			this.BeginAnimation(this.Ellipse_17);
			this.BeginAnimation(this.Ellipse_18);
			this.BeginAnimation(this.Ellipse_19);
			this.BeginAnimation(this.Ellipse_20);

			this.BeginAnimation(this.Rectangle_Saline);
			this.BeginAnimation(this.Rectangle_Stabilizer);
			this.BeginAnimation(this.Rectangle_MineralOil);
			this.BeginAnimation(this.Rectangle_WashingLiquid);
		}
	}
}
