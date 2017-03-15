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
		private Point startSelectionPoint;

		private Point endSelectionPoint;

		private Rectangle selectionRectangle = new Rectangle();

		private bool isMultiselecting = false;

		private bool isSelectSingle = false;

		private Canvas Canvas_AssociatedObject { get { return this.AssociatedObject as Canvas; } }

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
			this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
			this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
			this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
		}

		private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Tuple<object, MouseButtonEventArgs> args = new Tuple<object, MouseButtonEventArgs>(sender,e);
				this.ViewModel.RaiseSampleViewChanged(new SampleViewChangedArgs(Common.SampleViewChangedName.MultiSelectMouseDown, 2));
				//this.isSelectSingle = true;

				//if (this.isMultiselecting == false) this.isMultiselecting = true;

				//this.startSelectionPoint = e.GetPosition((IInputElement)sender);

				//this.selectionRectangle.Margin = new Thickness
				//	(this.startSelectionPoint.X, this.startSelectionPoint.Y, this.startSelectionPoint.X, this.startSelectionPoint.Y);

				//this.selectionRectangle.Width = 0;

				//this.selectionRectangle.Height = 0;

				//this.selectionRectangle.Stroke = Brushes.Goldenrod;

				//this.selectionRectangle.StrokeDashArray = new DoubleCollection() { 2 };

				//this.selectionRectangle.StrokeThickness = 2;

				//if (Canvas_AssociatedObject.Children.Contains(this.selectionRectangle) == false) Canvas_AssociatedObject.Children.Add(this.selectionRectangle);

				//e.Handled = false;
			}
			catch
			{
				throw new System.NotImplementedException();
			}
		}

		private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			try
			{
				this.endSelectionPoint = e.GetPosition((IInputElement)sender);
				var width = this.endSelectionPoint.X - this.startSelectionPoint.X;
				var height = this.endSelectionPoint.Y - this.startSelectionPoint.Y;

				if (width > 5 && height > 5 && this.isMultiselecting)
				{
					this.isSelectSingle = false;
					this.selectionRectangle.Width = width;
					this.selectionRectangle.Height = height;

					this.ViewModel.ResetSampleSelection();
					VisualTreeHelper.HitTest(this.Canvas_AssociatedObject, null, f =>
					{
						var o = f.VisualHit as Ellipse;
						if (o != null)
						{
							DependencyObject parent = VisualTreeHelper.GetParent(o);
							while (parent != null)
							{
								if (parent is SingleTube)
								{
									((SingleTube)parent).IsSelected = true;
									break;
								}
								else
								{
									parent = VisualTreeHelper.GetParent(parent);
								}
							}
						}
						return HitTestResultBehavior.Continue;
					}, new GeometryHitTestParameters(new RectangleGeometry(new Rect(this.startSelectionPoint, this.endSelectionPoint))));
				}
			}
			catch
			{
				throw new System.NotImplementedException();
			}
		}

		private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			try
			{
				if (this.isMultiselecting)
				{
					this.Canvas_AssociatedObject.Children.Remove(this.selectionRectangle);
					this.isMultiselecting = false;
					this.ViewModel.SynchronizeSampleSelectionState();
				}
			}
			catch
			{
				throw new System.NotImplementedException();
			}
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
			this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
			this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
		}
	}
}
