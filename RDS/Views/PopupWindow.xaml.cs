using RDS.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RDS.Views
{
	/// <summary>
	/// PopupWindow.xaml 的交互逻辑
	/// </summary>
	public partial class PopupWindow : Window
	{
		public PopupWindowViewModel ViewModel { get { return this.DataContext as PopupWindowViewModel; } }

		public PopupWindow()
		{
			InitializeComponent();
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);
			this.ViewModel.ViewChanged += (s, e) => this.Hide();
		}
	}
}
