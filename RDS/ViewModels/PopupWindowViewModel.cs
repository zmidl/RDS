using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.ViewModels
{
	public class PopupWindowViewModel : ViewModel
	{
		private string popupType = new System.Windows.Window().FindResource(Properties.Resources.PopupWindow_MessageBox).ToString();
		public string PopupType
		{
			get { return popupType; }
			set
			{
				popupType = value;
				this.RaisePropertyChanged(nameof(PopupType));
			}
		}

		private string message = string.Empty;
		public string Message
		{
			get { return message; }
			set
			{
				message = value;
				this.RaisePropertyChanged(nameof(Message));
			}
		}

		private int popupTypeIndex = 0;
		public int PopupTypeIndex
		{
			get { return popupTypeIndex; }
			set
			{
				popupTypeIndex = value;
				this.RaisePropertyChanged(nameof(PopupTypeIndex));
			}
		}

		public RelayCommand ExitPopupWindowView { get; private set; }
		public PopupWindowViewModel()
		{
			this.ExitPopupWindowView = new RelayCommand(this.ExecuteExitPopupWindowView);
		}

		public void ExecuteExitPopupWindowView()
		{
			this.OnViewChanged(null);
		}

		public void ShowMessage(string message)
		{
			this.PopupType = new System.Windows.Window().FindResource(Properties.Resources.PopupWindow_MessageBox).ToString();
			this.Message = message;
			this.PopupTypeIndex = 0;
		}

		public void ShowAdmin()
		{
			this.PopupType = new System.Windows.Window().FindResource(Properties.Resources.PopupWindow_Administrators).ToString();
			this.PopupTypeIndex = 1;
		}

		public void ShowCricleProgress()
		{
			this.PopupType = "请稍等";
			this.PopupTypeIndex = 3;
		}
	}

	//public class PopupWindowViewChangedArgs : EventArgs
	//{
	//	public PopupWindowAction PopupWindowAction { get; }
	//	public string Message { get; set; }

	//	public PopupWindowViewChangedArgs(PopupWindowAction popupWindowAction, string message)
	//	{
	//		this.PopupWindowAction = popupWindowAction;
	//		this.Message = message;
	//	}
	//}

	//public enum PopupWindowAction
	//{
	//	ShowMessage = 0,
	//	ShowAdmin = 1,
	//	ShowAbout = 2,
	//	ExitView = 3
	//}
}
