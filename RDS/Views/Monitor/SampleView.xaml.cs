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
using RDS.Apps.Common;
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

        private Point StartPoint;
        private Point StartTouchPoint;

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
            this.DataContext = new SampleViewModel();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            ((IExitView)this).ExitView();
        }

        private void Canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
       
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
                this.Button_On.Content = this.SampleViewSelectedIndex.ToString();
            }
        }

        private void UcTextBlock_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {
            
                this.ViewModel.A1 = true;
                this.ViewModel.A2 = true;
                this.ViewModel.A3 = true;
                this.ViewModel.A5 = true;
                this.ViewModel.A6 = true;
                this.ViewModel.A7 = true;
                this.ViewModel.A8 = true;
                this.ViewModel.A9 = true;
                this.ViewModel.A10 = true;
                this.ViewModel.A1 = true;
                this.ViewModel.A2 = true;
                this.ViewModel.A3 = true;
                this.ViewModel.A5 = true;
                this.ViewModel.A6 = true;
                this.ViewModel.A7 = true;
                this.ViewModel.A8 = true;
                this.ViewModel.A9 = true;
                this.ViewModel.A10 = true;
                this.ViewModel.A1 = true;
                this.ViewModel.A2 = true;
                this.ViewModel.A3 = true;
                this.ViewModel.A5 = true;
                this.ViewModel.A6 = true;
                this.ViewModel.A7 = true;
                this.ViewModel.A8 = true;
                this.ViewModel.A9 = true;
                this.ViewModel.A10 = true;

        }

        SingleTube previousTube;
        private void SingleTube_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.previousTube != null) this.previousTube.IsSelected = false;
            var currentTube = (SingleTube)sender;
            if (currentTube.Equals(previousTube) == false)
            {
                previousTube = currentTube;
                currentTube.IsSelected = true;
            }
            else
            {
                currentTube.IsSelected = false;
            }
        }
    }
}
