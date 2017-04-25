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

		bool aa = false;

		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				if (Keyboard.IsKeyDown(Key.L))
				{
					this.aa = !this.aa;

					ResourceDictionary langRd = null;
					try
					{
						if (aa) langRd = Application.LoadComponent(new Uri(Properties.Resources.English, UriKind.Relative)) as ResourceDictionary;
						else langRd = Application.LoadComponent(new Uri(Properties.Resources.Chinese, UriKind.Relative)) as ResourceDictionary;
					}
					catch
					{

					}

					if (langRd != null)
					{
						if (this.Resources.MergedDictionaries.Count > 0)
						{
							this.Resources.MergedDictionaries.Clear();
						}
						this.Resources.MergedDictionaries.Add(langRd);
					}
					this.ViewModel.ShowAdmin();
				}
			}
		}
	}
}
