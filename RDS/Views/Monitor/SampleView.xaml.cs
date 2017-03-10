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

        public SampleViewModel ViewModel { get { return this.DataContext as SampleViewModel; } }

        public SampleView()
        {
            InitializeComponent();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }


        //private void Canvas_Thumbnail_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        //{
        //    this.StartPoint = e.GetPosition((IInputElement)sender);
        //}

        //private void Canvas_Thumbnail_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        //{
        //    var endPoint = e.GetPosition((IInputElement)sender);
        //    if (Keyboard.IsKeyDown(Key.LeftCtrl) == false)
        //    {
        //        if (this.StartPoint.Y < endPoint.Y) { this.SampleViewSelectedIndex--; e.Handled = true; }
        //        else if (this.StartPoint.Y > endPoint.Y) { this.SampleViewSelectedIndex++; e.Handled = true; }
        //        else e.Handled = false;
        //        this.TabControl_SampleView.SelectedIndex = this.SampleViewSelectedIndex;
               
        //    }
        //}

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
			this.isSelectSingle = true;
            if (this.isMultiselecting == false) this.isMultiselecting = true;

            this.startSelectionPoint = e.GetPosition((IInputElement)sender);

            this.selectionRectangle.Margin = new Thickness
                (this.startSelectionPoint.X, this.startSelectionPoint.Y, this.startSelectionPoint.X, this.startSelectionPoint.Y);

            this.selectionRectangle.Width = 0;

            this.selectionRectangle.Height = 0;

            this.selectionRectangle.Stroke = Brushes.Goldenrod;

			this.selectionRectangle.StrokeDashArray = new DoubleCollection() { 2 };

            this.selectionRectangle.StrokeThickness = 3;

            //this.isSelectionMode = true;


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
				//for (int i = 0; i < 80; i++)
				//{
				//	this.ViewModel.SampleDescritions[i].IsSelected = false;
				//}
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


						//o.Stroke = new SolidColorBrush(Colors.Black);
					}

                    return HitTestResultBehavior.Continue;

                }, new GeometryHitTestParameters(new RectangleGeometry(new Rect(this.startSelectionPoint, this.endSelectionPoint))));
            }
            //else this.isSelectionMode = false;
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
			
			//this.Button_On.Content = this.ViewModel.SampleDescritions.Where(o => o.IsSelected == true).Count().ToString();
		}

	
		private void ucDataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
		
		}
	}
}
