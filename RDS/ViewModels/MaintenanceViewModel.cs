using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.ViewModels
{
	public class MaintenanceViewModel : ViewModel
	{
		private readonly int WizardSize = 2;

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

		public RelayCommand TurnNextView { get; private set; }
		public RelayCommand TurnPreviousView { get; private set; }

		public MaintenanceViewModel()
		{
			this.TurnNextView = new RelayCommand(this.ExecuteTurnNextView);
			this.TurnPreviousView = new RelayCommand(this.ExecuteTurnPreviousView);
		}


		private void ExecuteTurnNextView()
		{
			if (this.WizardIndex++ == this.WizardSize) { this.OnViewChanged(null); };
			this.RaiseWizards();
		}

		private void ExecuteTurnPreviousView()
		{
			this.WizardIndex--;
			this.RaiseWizards();
		}

		private void RaiseWizards()
		{
			this.RaisePropertyChanged(nameof(this.Wizard1));
			this.RaisePropertyChanged(nameof(this.Wizard2));
			this.RaisePropertyChanged(nameof(this.Wizard3));

		}
	}
}
