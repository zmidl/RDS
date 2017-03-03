using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
        private bool isMouseButtonDown = false;
        private Point StartPoint;

        private Point startSelectionPoint;
        private Point endSelectionPoint;

        private Rectangle selectionRectangle = new Rectangle();

        private bool isSelectionMode = false;

        private int sampleViewSelectedIndex;
        public int SampleViewSelectedIndex
        {
            get { return sampleViewSelectedIndex; }
            set
            {
                if (value < 0) value = 0;
                else if (value > 3) value = 3;
                else sampleViewSelectedIndex = value;
            }
        }

        public SampleViewModel ViewModel { get { return this.DataContext as SampleViewModel; } }

        public SampleView()
        {
            InitializeComponent();
            //this.DataContext = new SampleViewModel();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }





        private void Canvas_Thumbnail_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.StartPoint = e.GetPosition((IInputElement)sender);
        }

        private void Canvas_Thumbnail_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {
            var endPoint = e.GetPosition((IInputElement)sender);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) == false)
            {
                if (this.StartPoint.Y < endPoint.Y) { this.SampleViewSelectedIndex--; e.Handled = true; }
                else if (this.StartPoint.Y > endPoint.Y) { this.SampleViewSelectedIndex++; e.Handled = true; }
                else e.Handled = false;
                this.TabControl_SampleView.SelectedIndex = this.SampleViewSelectedIndex;
               
            }
        }

       

        private void SingleTube_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var currentTube = (SingleTube)sender;
            currentTube.IsSelected = !currentTube.IsSelected;
        }



        private void Canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.isSelectionMode == false) this.isSelectionMode = true;

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
			
            if (width > 5 && height > 5 && this.isSelectionMode)
            {
                this.selectionRectangle.Width = width;
                this.selectionRectangle.Height = height;
				for (int i = 0; i < 80; i++)
				{
					this.ViewModel.SampleSelectionStates[i] = false;
				}
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
          
            if (this.isSelectionMode)
            {
                this.Canvas_SampleViewOne.Children.Remove(this.selectionRectangle);
                this.isSelectionMode = false;
                //e.Handled = false;
            }
        }

     

     

        private void UcTextBlock_Acolumn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ViewModel.SetAHole();
        }

        private void UcTextBlock_Bcolumn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ViewModel.SetBHole();
        }

        private void UcTextBlock_Ccolumn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ViewModel.SetCHole();
        }

        private void UcTextBlock_Dcolumn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ViewModel.SetDHole();
        }
    }
}
