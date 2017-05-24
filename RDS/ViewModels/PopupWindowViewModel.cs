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
		private string passwordMessage = "请输入密码";
		public string PasswordMessage
		{
			get { return passwordMessage; }
			set
			{
				passwordMessage = value;
				this.RaisePropertyChanged(nameof(PasswordMessage));
			}
		}


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

		public ObservableCollection<ReagentSeries> ReagentSeries { get; set; } = new ObservableCollection<ReagentSeries>();

		public object SelectedReagentItem { get; set; }

		public List<string> UsedReagentItems { get; private set; }

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

		private readonly string[] popupWindowTitle;

		public int PopupTypeIndex { get; set; }

		public RelayCommand RemoveReagentItem { get; private set; }

		public RelayCommand AddReagentItem { get; private set; }

		private Action[] Actions;

		public RelayCommand Command { get; private set; }

		public PopupWindowViewModel()
		{
			this.Command = new RelayCommand(this.ExecuteCommand);

			this.RemoveReagentItem = new RelayCommand(this.ExecuteRemoveReagentItem);

			this.AddReagentItem = new RelayCommand(this.ExecuteAddReagentItem);

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
			this.UsedReagentItems = General.ReadConfiguration(Properties.Resources.SelectedReanents).Split(this.separator).ToList();
			var reagentSeries = General.ReadConfiguration(Properties.Resources.ReagentSeries).Split(this.separator);
			for (int i = 0; i < reagentSeries.Length; i++)
			{
				var reagentItemNames = General.ReadConfiguration(reagentSeries[i]).Split(this.separator);
				var reagentItems = new ObservableCollection<ReagentItem>();
				for (int j = 0; j < reagentItemNames.Length; j++)
				{
					reagentItems.Add(new ReagentItem(reagentItemNames[j], this.UsedReagentItems.Exists(o => o == reagentItemNames[j])));
				}
				this.ReagentSeries.Add(new ReagentSeries(reagentSeries[i], reagentItems));
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
			EnterAdministratorsView = 1
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
			int index = int.Parse(actionIndex.ToString());
			if ((this.PopupType == PopupType.ShowAdministratorsLogin && index == 0) == false) this.OnViewChanged(new PopupWindowViewChangedArgs(ViewChangedOption.ExitView, null));
			if (this.Actions[index] == null) this.Actions[index] = new Action(() => {; });
			this.Actions[index]();
		}

		public void ValidateAdministrators()
		{
			this.OnViewChanged(new PopupWindowViewChangedArgs(ViewChangedOption.EnterAdministratorsView, null));
		}

		public void EnterAdministratorsView()
		{
			this.PopupType = PopupType.ShowAdministratorsView;
			this.Actions = new Action[3];
			this.Actions[0] = new Action(this.SaveConfiguration);
			this.PasswordMessage = "请输入密码";
		}

		public void SaveConfiguration()
		{
			var reagentItems = this.ReagentSeries.Select(o => o.ReagentItems).ToList();
			var usedItems = new List<string>();
			for (int i = 0; i < reagentItems.Count; i++) usedItems.AddRange(reagentItems[i].Where(o => o.IsUsed == true).Select(o => o.Name));
			var usedItemsFormatString = string.Join(this.separator.ToString(), usedItems.ToArray());
			General.WriteConfiguration(Properties.Resources.SelectedReanents, usedItemsFormatString);
			General.WriteConfiguration(Properties.Resources.Language, this.SelectedLanguage);
			var languagePath = General.ReadConfiguration(this.SelectedLanguage);
			var resourceDictionary = System.Windows.Application.LoadComponent(new Uri(languagePath, UriKind.Relative)) as System.Windows.ResourceDictionary;
			General.ChangeLanguage(resourceDictionary);
		}

		public void ExecuteRemoveReagentItem(object param)
		{
			var itemName = param.ToString();
			for (int i = 0; i < this.ReagentSeries.Count; i++)
			{
				var item = this.ReagentSeries[i].ReagentItems.FirstOrDefault(o => o.Name == itemName);
				if (item != null) { this.ReagentSeries[i].ReagentItems.Remove(item); break; }
			}
		}

		public void ExecuteAddReagentItem(object param)
		{

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
