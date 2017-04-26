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
		private string popupTitle = string.Empty;
		public string PopupTitle
		{
			get { return popupTitle; }
			set
			{
				popupTitle = value;
				this.RaisePropertyChanged(nameof(PopupTitle));
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

		private PopupType popupType;
		public PopupType PopupType
		{
			get { return popupType; }
			set
			{
				popupType = value;
				this.RaisePropertyChanged(nameof(PopupType));
				this.PopupTypeIndex = (int)value;
				this.RaisePropertyChanged(nameof(this.PopupTypeIndex));
			}
		}

		public int PopupTypeIndex { get; set; }

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
			this.PopupTitle = General.FindResource(Properties.Resources.PopupWindow_MessageBox).ToString();
			this.Message = message;
			this.PopupType = PopupType.ShowMessage;
		}

		public void ShowAdministratorsLogin()
		{
			this.PopupTitle = General.FindResource(Properties.Resources.PopupWindow_Administrators).ToString();
			this.PopupType = PopupType.ShowAdministratorsLogin;
		}

		public void ShowInformation()
		{
			this.PopupTitle = General.FindResource(Properties.Resources.PopupWindow_Information).ToString();
			this.PopupType = PopupType.ShowInformation;
		}

		public void ShowCricleProgress()
		{
			this.PopupTitle = General.FindResource(Properties.Resources.PopupWindow_Wait).ToString();
			this.PopupType = PopupType.ShowCircleProgress;
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

	public enum PopupType
	{
		ShowMessage = 0,
		ShowAdministratorsLogin = 1,
		ShowAdministratorsView = 2,
		ShowCircleProgress = 3,
		ShowInformation = 4
	}
}
