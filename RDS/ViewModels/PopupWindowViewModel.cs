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

		public ObservableCollection<string> Languages { get; set; }

		private string newReagentSerierName;
		public string NewReagentSerierName
		{
			get { return newReagentSerierName; }
			set
			{
				newReagentSerierName = value;
				this.RaisePropertyChanged(nameof(NewReagentSerierName));
			}
		}

		private string newReagentItemName;
		public string NewReagentItemName
		{
			get { return newReagentItemName; }
			set
			{
				newReagentItemName = value;
				this.RaisePropertyChanged(nameof(NewReagentItemName));
			}
		}



		public string SelectedLanguage { get; set; }

		public ObservableCollection<ReagentSeries> ReagentSeries { get; set; } = new ObservableCollection<ReagentSeries>();

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

		public RelayCommand RemoveReagentInformation { get; private set; }

		public RelayCommand AddReagentItem { get; private set; }

		public RelayCommand AddReagentSeries { get; private set; }

		private Action[] Actions;

		public RelayCommand Command { get; private set; }

		public PopupWindowViewModel()
		{
			this.Command = new RelayCommand(this.ExecuteCommand);

			this.RemoveReagentInformation = new RelayCommand(this.ExecuteRemoveReagentInformation);

			this.AddReagentItem = new RelayCommand(this.ExecuteAddReagentItem);

			this.AddReagentSeries = new RelayCommand(this.ExecuteAddReagentSeries);

			this.InitializeReagentInformation();

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

		private void InitializeReagentInformation()
		{
			this.UsedReagentItems = General.ReadConfiguration(Properties.Resources.ReagentInformation).Split(Properties.Resources.Separator1.ToCharArray()[0]).ToList();
			for (int i = 0; i < this.UsedReagentItems.Count; i++)
			{
				var reagentItemsList = this.UsedReagentItems[i].Split(Properties.Resources.Separator2.ToCharArray()[0]).ToList();
				var reagentItems = new ObservableCollection<ReagentItem>();
				for (int j = 1; j < reagentItemsList.Count; j++)
				{
					var item = reagentItemsList[j].Split(Properties.Resources.Separator3.ToCharArray()[0]);
					var itemName = item[0];
					reagentItems.Add(new ReagentItem(reagentItemsList[0], itemName, item[1] == Properties.Resources.One ? true : false));
				}
				this.ReagentSeries.Add(new ReagentSeries(reagentItemsList[0], reagentItems));
			}
		}

		private void InitializeLanguages()
		{
			var languages = General.ReadConfiguration(Properties.Resources.Languages).Split(Properties.Resources.Separator1.ToCharArray()[0]).ToList();
			this.Languages = new ObservableCollection<string>(languages);
			var language = General.ReadConfiguration(Properties.Resources.Language);
			this.SelectedLanguage = this.Languages.FirstOrDefault(o => o == language);
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
			var usedItemsFormatString = string.Join(Properties.Resources.Separator1, usedItems.ToArray());
			General.WriteConfiguration(Properties.Resources.SelectedReanents, usedItemsFormatString);

			this.SaveLanguage();
			this.SaveReagentInformation();
		}

		private void SaveLanguage()
		{
			General.WriteConfiguration(Properties.Resources.Language, this.SelectedLanguage);
			var languagePath = General.ReadConfiguration(this.SelectedLanguage);
			var resourceDictionary = System.Windows.Application.LoadComponent(new Uri(languagePath, UriKind.Relative)) as System.Windows.ResourceDictionary;
			General.ChangeLanguage(resourceDictionary);
		}

		private void SaveReagentInformation()
		{
			var seriesCount = this.ReagentSeries.Count;
			string[] result = new string[seriesCount];
			StringBuilder[] seriesTemp = new StringBuilder[seriesCount];
			for (int i = 0; i < seriesCount; i++)
			{
				var currentSeries = this.ReagentSeries[i];
				seriesTemp[i] = new StringBuilder();
				seriesTemp[i].AppendFormat("{0}=", currentSeries.Name);
				var items = new string[currentSeries.ReagentItems.Count];
				for (int j = 0; j < currentSeries.ReagentItems.Count; j++)
				{
					var currentItem = currentSeries.ReagentItems[j];
					items[j]=string.Format
					(
						Properties.Resources.ReagentItemsFormat, 
						currentItem.Name,currentItem.IsUsed ? Properties.Resources.One: Properties.Resources.Zero
					);
				}
				seriesTemp[i].Append(string.Join(Properties.Resources.Separator2, items));
				result[i] = seriesTemp[i].ToString();
			}
			General.WriteConfiguration(Properties.Resources.ReagentInformation, string.Join(Properties.Resources.Separator1, result));
		}

		/// <summary>
		/// remove one ReagentSeries or ReagentItem object   
		/// </summary>
		/// <param name="param">selected object</param>
		public void ExecuteRemoveReagentInformation(object param)
		{
			if (param is ReagentSeries) this.ReagentSeries.Remove((ReagentSeries)param);
			else if (param is ReagentItem)
			{
				var reagentItem = (ReagentItem)param;
				var reagentSeries = this.ReagentSeries.FirstOrDefault(o => o.Name == reagentItem.ParentName);
				reagentSeries.ReagentItems.Remove(reagentItem);
			}
		}

		/// <summary>
		/// add new ReagentItem
		/// </summary>
		/// <param name="param">parent object of the ReagentItem</param>
		private void ExecuteAddReagentItem(object param)
		{
			var reagentSeries = (ReagentSeries)param;
			reagentSeries.ReagentItems.Add(new ReagentItem(reagentSeries.Name, this.NewReagentItemName, false));
			this.NewReagentItemName = string.Empty;
		}

		private void ExecuteAddReagentSeries()
		{
			this.ReagentSeries.Add(new ReagentSeries(this.NewReagentSerierName, new ObservableCollection<ReagentItem>()));
			this.NewReagentSerierName = string.Empty;
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
