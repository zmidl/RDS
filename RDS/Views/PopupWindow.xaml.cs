using RDS.ViewModels;
using System.Windows;
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
			this.ViewModel.ViewChanged += (s, e) =>
			{
				switch (((PopupWindowViewModel.PopupWindowViewChangedArgs)e).Option)
				{
					case PopupWindowViewModel.ViewChangedOption.ExitView: { this.Hide(); break; }
					case PopupWindowViewModel.ViewChangedOption.EnterAdministratorsView:
					{
						if (this.PasswordBox1.Password == Properties.Resources.Password) this.ViewModel.EnterAdministratorsView();
						else { this.PasswordBox1.Password = string.Empty; this.ViewModel.PasswordMessage = "密码错误"; }
						break;
					}
					default: { break; }
				}
			};
		}

		public void InitializeLanguage(ResourceDictionary resourceDictionary)
		{
			if (resourceDictionary != null)
			{
				if (this.Resources.MergedDictionaries.Count > 0)
				{
					this.Resources.MergedDictionaries.Clear();
				}
				this.Resources.MergedDictionaries.Add(resourceDictionary);
			}
		}

		private void Button_ClearPassword_Click(object sender, RoutedEventArgs e)
		{
			this.PasswordBox1.Focus();
			this.PasswordBox1.Password = string.Empty;
			this.ViewModel.PasswordMessage = "请输入密码";
		}
	}
}
