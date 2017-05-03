﻿using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace RDS.ViewModels
{
	public class InitializeSuppliesViewModel : ViewModel
	{
		public string[] Messages { get; } = new string[4];

		private string[] noReagentMessages = new string[4]
		{
			string.Empty,
			General.FindResource(Properties.Resources.InitializeSuppliesView_Message7_1),
			General.FindResource(Properties.Resources.InitializeSuppliesView_Message8_1),
			General.FindResource(Properties.Resources.InitializeSuppliesView_Message9_1)
		};

		private string[] reagentMessages = new string[4]
		{
			General.FindResource(Properties.Resources.InitializeSuppliesView_Message6),
			General.FindResource(Properties.Resources.InitializeSuppliesView_Message7),
			General.FindResource(Properties.Resources.InitializeSuppliesView_Message8),
			General.FindResource(Properties.Resources.InitializeSuppliesView_Message9)
		};

		public enum BeadPlace
		{
			Neither = 0,
			Left = 1,
			Right = 2,
			Both = 3
		}

		private BeadPlace beadPlace = BeadPlace.Both;

		public bool IsLeftSelected
		{
			get { return this.GetLeftBeadSelectionState(); }
			set
			{
				if (value)
				{
					if (this.IsRightSelected) this.beadPlace = BeadPlace.Both;
					else this.beadPlace = BeadPlace.Left;
				}
				else
				{
					if (this.IsRightSelected) this.beadPlace = BeadPlace.Right;
					else this.beadPlace = BeadPlace.Neither;
				}
				this.RaisePropertyChanged(nameof(IsLeftSelected));
				this.OnViewChanged(new Tuple<string, object>("BeadSelectionState", this.beadPlace));
			}
		}

		public bool IsRightSelected
		{
			get { return this.GetRightBeadSelectionState(); }
			set
			{
				if (value)
				{
					if (this.IsLeftSelected) this.beadPlace = BeadPlace.Both;
					else this.beadPlace = BeadPlace.Right;
				}
				else
				{
					if (this.IsLeftSelected) this.beadPlace = BeadPlace.Left;
					else this.beadPlace = BeadPlace.Neither;
				}
				this.RaisePropertyChanged(nameof(IsRightSelected));
				this.OnViewChanged(new Tuple<string, object>("BeadSelectionState", this.beadPlace));
			}
		}

		private bool GetLeftBeadSelectionState()
		{
			if (this.beadPlace == BeadPlace.Left || this.beadPlace == BeadPlace.Both) return true;
			else return false;
		}

		private bool GetRightBeadSelectionState()
		{
			if (this.beadPlace == BeadPlace.Right || this.beadPlace == BeadPlace.Both) return true;
			else return false;
		}

		private readonly int WizardSize = 13;

		private int wizardIndex;
		public int WizardIndex
		{
			get { return wizardIndex; }
			set
			{
				if (value < 0) value = 0;
				else if (value > this.WizardSize) value = this.WizardSize;
				wizardIndex = value;
				this.RaisePropertyChanged(nameof(WizardIndex));
			}
		}

		public bool Wizard1 { get { return this.wizardIndex == 0 ? true : false; } }
		public bool Wizard2 { get { return this.wizardIndex == 1 ? true : false; } }
		public bool Wizard3 { get { return this.wizardIndex == 2 ? true : false; } }
		public bool Wizard4 { get { return this.wizardIndex == 3 ? true : false; } }
		public bool Wizard5 { get { return this.wizardIndex == 4 ? true : false; } }
		public bool Wizard6 { get { return this.wizardIndex == 5 ? true : false; } }
		public bool Wizard7 { get { return this.wizardIndex == 6 ? true : false; } }
		public bool Wizard8 { get { return this.wizardIndex == 7 ? true : false; } }
		public bool Wizard9 { get { return this.wizardIndex == 8 ? true : false; } }
		public bool Wizard10 { get { return this.wizardIndex == 9 ? true : false; } }
		public bool Wizard11 { get { return this.wizardIndex == 10 ? true : false; } }
		public bool Wizard12 { get { return this.wizardIndex == 11 ? true : false; } }
		public bool Wizard13 { get { return this.wizardIndex == 12 ? true : false; } }
		public bool Wizard14 { get { return this.wizardIndex == 13 ? true : false; } }
		public bool Wizard15 { get { return this.wizardIndex == 14 ? true : false; } }

		public RelayCommand TurnNextView { get; private set; }
		public RelayCommand TurnPreviousView { get; private set; }

		public InitializeSuppliesViewModel()
		{
			this.TurnNextView = new RelayCommand(this.ExecuteTurnNextView);
			this.TurnPreviousView = new RelayCommand(() => { this.WizardIndex--; this.RaiseWizards(); });
			this.RaiseWizards();

			this.Messages[0] = string.Format(General.FindResource(Properties.Resources.InitializeSuppliesView_Message6), General.UsedReagents[0]);
			for (int i = 0; i < 4; i++)
			{
				if (i > General.UsedReagents.Count - 1) this.Messages[i] = this.noReagentMessages[i];
				else this.Messages[i] = string.Format(this.reagentMessages[i], General.UsedReagents[i]);
			}
		}

		private void ExecuteTurnNextView()
		{
			if (this.WizardIndex++ == this.WizardSize) { this.OnViewChanged(new Tuple<string, object>(string.Empty, null)); };
			this.RaiseWizards();
		}

		private void RaiseWizards()
		{
			this.RaisePropertyChanged(nameof(this.Wizard1));
			this.RaisePropertyChanged(nameof(this.Wizard2));
			this.RaisePropertyChanged(nameof(this.Wizard3));
			this.RaisePropertyChanged(nameof(this.Wizard4));
			this.RaisePropertyChanged(nameof(this.Wizard5));
			this.RaisePropertyChanged(nameof(this.Wizard6));
			this.RaisePropertyChanged(nameof(this.Wizard7));
			this.RaisePropertyChanged(nameof(this.Wizard8));
			this.RaisePropertyChanged(nameof(this.Wizard9));
			this.RaisePropertyChanged(nameof(this.Wizard10));
			this.RaisePropertyChanged(nameof(this.Wizard11));
			this.RaisePropertyChanged(nameof(this.Wizard12));
			this.RaisePropertyChanged(nameof(this.Wizard13));
			this.RaisePropertyChanged(nameof(this.Wizard14));
			this.RaisePropertyChanged(nameof(this.Wizard15));
		}
	}
}
