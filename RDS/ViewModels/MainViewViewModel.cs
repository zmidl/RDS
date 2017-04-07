﻿using RDS.ViewModels.Common;

namespace RDS.ViewModels
{
	public class MainViewViewModel : ViewModel
	{
		private bool isTask;
		public bool IsTask
		{
			get { return isTask; }
			set
			{
				if (isTask == false)
				{
					isTask = value;
					this.RaisePropertyChanged(nameof(IsTask));
					this.isHistroy = false;
					this.isHelp = false;
					this.RaisePropertyChanged(nameof(this.IsHistroy));
					this.RaisePropertyChanged(nameof(this.IsHelp));
					this.ShowTaskView();
				}
			}
		}

		private bool isHistroy;
		public bool IsHistroy
		{
			get { return isHistroy; }
			set
			{
				if (isHistroy == false)
				{
					isHistroy = value;
					this.RaisePropertyChanged(nameof(IsHistroy));
					this.isTask = false;
					this.isHelp = false;
					this.RaisePropertyChanged(nameof(this.IsTask));
					this.RaisePropertyChanged(nameof(this.IsHelp));
					this.ShowHistroyView();
				}
			}
		}

		private bool isHelp;
		public bool IsHelp
		{
			get { return isHelp; }
			set
			{
				if (isHelp == false)
				{
					isHelp = value;
					this.RaisePropertyChanged(nameof(IsHelp));
					this.isTask = false;
					this.isHistroy = false;
					this.RaisePropertyChanged(nameof(this.IsTask));
					this.RaisePropertyChanged(nameof(this.IsHistroy));
					this.ShowHelpView();
				}
			}
		}

		public RelayCommand ExitApp { get; private set; }
		//public RelayCommand ShowHistroyView { get; private set; }
		//public RelayCommand ShowHelpView { get; private set; }

		public MainViewViewModel()
		{
			this.ExitApp = new RelayCommand(this.ExecuteExitApp);
			//this.ShowHistroyView = new RelayCommand(this.ExecuteShowHistroyView);
			//this.ShowHelpView = new RelayCommand(this.ExecuteShowHelpView);
		}

		private void ShowTaskView()
		{
			this.OnViewChanged(ViewChange.TaskView);
		}

		private void ShowHistroyView()
		{
			this.OnViewChanged(ViewChange.HistroyView);
		}

		private void ShowHelpView()
		{
			this.OnViewChanged(ViewChange.HelpView);
		}

		private void ExecuteExitApp()
		{
			this.OnViewChanged(ViewChange.ExitApp);
		}

		public enum ViewChange
		{
			TaskView = 0,
			HistroyView = 1,
			HelpView = 2,
			AdminView=3,
			ExitApp=4
		}
	}
}
