using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDS.Models;

namespace RDS.ViewModels
{
	public class PopupWindowViewModel : ViewModel
	{
	

		private readonly char separator = '-';

		private string additionReagentInformation;
		public string AdditionReagentInformation
		{
			get { return additionReagentInformation; }
			set
			{
				additionReagentInformation = value;
				this.RaisePropertyChanged(nameof(AdditionReagentInformation));
			}
		}

		public ObservableCollection<string> Languages { get; set; }

		public string SelectedLanguage { get; set; }

		public ObservableCollection<ReagentInformation> ReagentInformations { get; set; }

		public ReagentInformation SelectedReagentInformation { get; set; }

		public List<string> UsedReagents { get; private set; }

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

		private string password;
		public string Password
		{
			get { return password; }
			set
			{
				password = value;
				this.RaisePropertyChanged(nameof(Password));
			}
		}

		private readonly string[] popupWindowTitle;

		public int PopupTypeIndex { get; set; }

		public RelayCommand RemoveReagentInformation { get; private set; }

		public RelayCommand AddReagentInformation { get; private set; }

		private Action[] Actions;

		public RelayCommand Command { get; private set; }

		public PopupWindowViewModel()
		{
			this.Command = new RelayCommand(this.ExecuteCommand);

			this.RemoveReagentInformation = new RelayCommand(this.ExecuteRemoveReagentInformation);

			this.AddReagentInformation = new RelayCommand(this.ExecuteAddReagentInformation);

			this.InitializeReagentInformations();

			this.InitializeLanguages();

			this.popupWindowTitle = new string[7]
			{
				Properties.Resources.PopupWindow_Title_MessageBox,
				Properties.Resources.PopupWindow_Title_Administrators,
				Properties.Resources.PopupWindow_Title_Administrators,
				Properties.Resources.PopupWindow_Title_Wait,
				Properties.Resources.PopupWindow_Title_Information,
				Properties.Resources.PopupWindow_Title_MessageBox,
				Properties.Resources.PopupWindow_Title_MessageBox
			};
		}

		private void InitializeReagentInformations()
		{
			var reagentInformations = General.ReadConfiguration(Properties.Resources.Reagents).Split(this.separator);
			this.ReagentInformations = new ObservableCollection<ReagentInformation>();
			this.UsedReagents = General.ReadConfiguration(Properties.Resources.SelectedReanents).Split(this.separator).ToList();
			for (int i = 0; i < reagentInformations.Length; i++)
			{
				var reagentInformation = new ReagentInformation(reagentInformations[i], this.UsedReagents.Exists(o => o == reagentInformations[i]));
				reagentInformation.ViewChanged += ReagentInformation_ViewChanged;
				this.ReagentInformations.Add(reagentInformation);
			}
		}

		private void InitializeLanguages()
		{
			var languages = General.ReadConfiguration(Properties.Resources.Languages).Split(this.separator).ToList();
			this.Languages = new ObservableCollection<string>(languages);
			var language = General.ReadConfiguration(Properties.Resources.Language);
			this.SelectedLanguage = this.Languages.FirstOrDefault(o => o == language);
		}

		private void ReagentInformation_ViewChanged(object sender, object e)
		{
		}

		public enum ViewChangedOption
		{
			ExitView = 0,
		}

		public class PopupWindowViewChangedArgs : EventArgs
		{
			public ViewChangedOption Option { get; set; }
			public object Value { get; set; }

			public PopupWindowViewChangedArgs(ViewChangedOption option, object value)
			{
				this.Option = option;
				this.Value = value;
			}
		}

		private void ExecuteCommand(object actionIndex)
		{
			if (this.PopupType != PopupType.ShowAdministratorsLogin) this.OnViewChanged(new PopupWindowViewChangedArgs(ViewChangedOption.ExitView, null));
			int index = int.Parse(actionIndex.ToString());
			if (this.Actions[index] == null) this.Actions[index] = new Action(() => {; });
			this.Actions[index]();
		}

		public void ValidateAdministrators()
		{
			if (this.Password == Properties.Resources.Password)
			{
				this.PopupType = PopupType.ShowAdministratorsView;
				this.Actions = new Action[3];
				this.Actions[0] = new Action(this.SaveConfiguration);
			}
		}

		public void SaveConfiguration()
		{
			this.UsedReagents = this.ReagentInformations.Where(o => o.IsSelected == true).Select(o => o.Name).ToList();
			General.WriteConfiguration(Properties.Resources.SelectedReanents, string.Join(this.separator.ToString(), this.UsedReagents.ToArray()));
			General.WriteConfiguration(Properties.Resources.Language, this.SelectedLanguage);

			var languagePath = General.ReadConfiguration(this.SelectedLanguage);
			var resourceDictionary = System.Windows.Application.LoadComponent(new Uri(languagePath, UriKind.Relative)) as System.Windows.ResourceDictionary;
			General.ChangeLanguage(resourceDictionary);
		}

		public void ExecuteRemoveReagentInformation()
		{
			if (this.SelectedReagentInformation != null) this.ReagentInformations.Remove(this.SelectedReagentInformation);
		}

		public void ExecuteAddReagentInformation()
		{
			if (string.IsNullOrEmpty(this.AdditionReagentInformation) == false)
			{
				this.ReagentInformations.Insert(0, new ReagentInformation(this.AdditionReagentInformation, false));
				this.AdditionReagentInformation = string.Empty;
			}
		}

		public void PopupWindow(PopupType popupType, string message, Action[] actions)
		{
			this.PopupType = popupType;
			this.Message = message;
			this.Actions = actions;
			this.PopupTitle = General.FindStringResource(this.popupWindowTitle[(int)popupType]);
			if (popupType == PopupType.ShowAdministratorsLogin)
			{
				this.Actions = new Action[3];
				this.Actions[0] = new Action(this.ValidateAdministrators);
			}
		}

		public void ShowAdministratorsLogin()
		{
			this.PopupTitle = General.FindStringResource(Properties.Resources.PopupWindow_Title_Administrators).ToString();
			this.PopupType = PopupType.ShowAdministratorsLogin;
		}

		//public void ShowInformation()
		//{
		//	this.PopupTitle = General.FindStringResource(Properties.Resources.PopupWindow_Title_Information).ToString();
		//	this.PopupType = PopupType.ShowInformation;
		//}

		//public void ShowCricleProgress()
		//{
		//	this.PopupTitle = General.FindStringResource(Properties.Resources.PopupWindow_Title_Wait).ToString();
		//	this.PopupType = PopupType.ShowCircleProgress;
		//}
	}

	public enum PopupType
	{
		ShowMessage = 0,
		ShowAdministratorsLogin = 1,
		ShowAdministratorsView = 2,
		ShowCircleProgress = 3,
		ShowInformation = 4,
		ShowMessageWithRetryCancel = 5,
		ShowMessageWithFinishContinue = 6
	}
}
