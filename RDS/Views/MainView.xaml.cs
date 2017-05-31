using RDS.ViewModels;
using RDS.ViewModels.Common;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace RDS.Views
{
	/// <summary>
	/// MainView.xaml 的交互逻辑
	/// </summary>
	public partial class MainView : UserControl
	{

		private const string ShareDLL_PATH= @"\Apps\Dlls\ShareDll.dll";
		private delegate int Callback([MarshalAs(UnmanagedType.LPStr)]string pwszName, int count, string form, string to, IntPtr args);
		private Callback callback;

		[DllImport(ShareDLL_PATH)]
		extern static int Register(
			[MarshalAs(UnmanagedType.LPWStr)]string pwszName,
			[MarshalAs(UnmanagedType.FunctionPtr)]Callback ReciveDataCallback, 
			[MarshalAs(UnmanagedType.SysInt)]IntPtr pParam);

		private TaskView taskView = new TaskView();

		private HistroyView histroyView = new HistroyView();

		private HelpView helpView = new HelpView();

		private object PreviousContent;

		public MainViewModel ViewModel { get { return this.DataContext as MainViewModel; } }

		public MainView()
		{
			InitializeComponent();

			this.DataContext = new MainViewModel();

			this.ContentControl_CurrentContent.Content = this.taskView;

			this.PreviousContent = this.ContentControl_CurrentContent.Content;

			this.ViewModel.ViewChanged += ViewModel_ViewChanged;

			PopupWindow popupWindow = new PopupWindow();

			popupWindow.DataContext = this.ViewModel.PopupWindowViewModel;

			General.InitializePopupWindow(popupWindow);

			this.callback = new Callback(AAA);
			//var bytes = System.Text.Encoding.Default.GetBytes("S1");
			//var count = bytes.Length;

		
		}

		private void ViewModel_ViewChanged(object sender, object e)
		{
			switch ((MainViewModel.ViewChange)e)
			{
				case MainViewModel.ViewChange.TaskView: { this.ContentControl_CurrentContent.Content = this.taskView; break; }
				case MainViewModel.ViewChange.HistroyView: { this.ContentControl_CurrentContent.Content = this.histroyView; break; }
				case MainViewModel.ViewChange.HelpView: { this.ContentControl_CurrentContent.Content = this.helpView; break; }
				case MainViewModel.ViewChange.ExitApp: { Application.Current.Shutdown();/*Environment.Exit(0);*/break; }
				case MainViewModel.ViewChange.AdminView: { /*General.ShowAdministratorsView();*/ break; }
				default: break;
			}
		}

		public int AAA(string a, int b, string c, string d, IntPtr e)
		{
			this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { MessageBox.Show(""); }));
			return 2;
		}

		private void Button_Minimize_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(Register("S1", this.callback, new WindowInteropHelper(Window.GetWindow(this)).Handle).ToString());
		}

		

		private void Button_Information_Click(object sender, RoutedEventArgs e)
		{
			

		}
	}
}
