using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using RDS.ViewModels.Common;
using RDS.ViewModels;
using RDSCL;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// SampleView.xaml 的交互逻辑
	/// </summary>
	public partial class SampleView : UserControl, IExitView
    {
        Action IExitView.ExitView { get; set; }
      
        private Point startSelectionPoint;
        private Point endSelectionPoint;

        private Rectangle selectionRectangle = new Rectangle();

        private bool isMultiselecting = false;
		private bool isSelectSingle = false;

		public SampleViewModel ViewModel = new SampleViewModel();

        public SampleView()
        {
            InitializeComponent();
			this.DataContext = new SampleViewModel();
			this.ViewModel.ViewChanged += ViewModel_ViewChanged;
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			var args = (SampleViewChangedArgs)e;
			switch (args.SampleViewChangedName)
			{
				case SampleViewChangedName.MultiSelectMouseDown:
				{
					var v = (Tuple<object, MouseButtonEventArgs>)args.Args;
					this.SampleMultiSelectMouseDown(v.Item1, v.Item2);
					break;
				}
			}
		}

		private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }

        private void SingleTube_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
			var currentTube = (SingleTube)sender;
			if (this.isSelectSingle == true)
			{
				currentTube.IsSelected = !currentTube.IsSelected;
				this.ViewModel.SynchronizeSampleSelectionState();
			}
		}

        private void Canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
			
        }

		private void SampleMultiSelectMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.isSelectSingle = true;
			if (this.isMultiselecting == false) this.isMultiselecting = true;

			this.startSelectionPoint = e.GetPosition((IInputElement)sender);

			this.selectionRectangle.Margin = new Thickness
				(this.startSelectionPoint.X, this.startSelectionPoint.Y, this.startSelectionPoint.X, this.startSelectionPoint.Y);

			this.selectionRectangle.Width = 0;

			this.selectionRectangle.Height = 0;

			this.selectionRectangle.Stroke = Brushes.Goldenrod;

			this.selectionRectangle.StrokeDashArray = new DoubleCollection() { 2 };

			this.selectionRectangle.StrokeThickness = 2;

			if (this.Canvas_SampleViewOne.Children.Contains(this.selectionRectangle) == false) this.Canvas_SampleViewOne.Children.Add(this.selectionRectangle);
			e.Handled = false;
		}

        private void Canvas_PreviewMouseMove(object sender, MouseEventArgs e)
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
				VisualTreeHelper.HitTest(this.Canvas_SampleViewOne, null, f =>
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

        private void Canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
          
            if (this.isMultiselecting)
            {
                this.Canvas_SampleViewOne.Children.Remove(this.selectionRectangle);
                this.isMultiselecting = false;
				this.ViewModel.SynchronizeSampleSelectionState();
                //e.Handled = false;
            }
        }
       

		private void Button_Import_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
			ofd.Filter = "Xm;文件(*.xml;)|*.xml;|所有文件|*.*";
			ofd.ValidateNames = true;
			ofd.CheckPathExists = true;
			ofd.CheckFileExists = true;
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				var a =  ViewModels.Common.XmlOperation.ReadXmlFile(ofd.FileName);
				var b = a.Tables[2];
				this.ViewModel.DatatableToEntity(b);
			}
		}

		private void Button_Layout_Click(object sender, RoutedEventArgs e)
		{
			RDS.Views.LayoutSetting LS = new LayoutSetting();
			LS.ShowDialog();
		}

		private void Button_On_Click(object sender, RoutedEventArgs e)
		{
			//var a=this.FindResource("Uid").ToString();
			//MessageBox.Show(a);
			this.ViewModel.RaiseSampleViewChanged(new SampleViewChangedArgs(SampleViewChangedName.MultiSelectMouseDown, 1));
		}

		//private void ucDataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
		//{
		//	if(e.RightButton==MouseButtonState.Pressed)
		//	{
		//		var property = typeof(DataGrid).GetField(
		//											  "_isDraggingSelection",
		//											  System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);
		//		if (property != null)
		//		{
		//			property.SetValue(this.ucDataGrid, false);
		//		}
		//		DragDrop.DoDragDrop(this.ucDataGrid, this.ucDataGrid.SelectedItems, DragDropEffects.Copy);
		//	}
		//}
	}
}
