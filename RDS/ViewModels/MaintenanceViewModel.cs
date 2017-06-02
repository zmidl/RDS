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
		private readonly int WizardSize = 3;

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

		public RelayCommand TurnNextView { get; private set; }
		public RelayCommand TurnPreviousView { get; private set; }

		public MaintenanceViewModel()
		{
			this.TurnNextView = new RelayCommand(this.ExecuteTurnNextView);
			this.TurnPreviousView = new RelayCommand(this.ExecuteTurnPreviousView);
		}

		public enum ViewChangedOption
		{
			ExitMaintenanceView = 0,
			EnterFinalView = 1
		}

		public class MaintenanceViewChangedArgs : EventArgs
		{
			public ViewChangedOption Option { get; set; }
			public object Value { get; set; }

			public MaintenanceViewChangedArgs(ViewChangedOption option, object value)
			{
				this.Option = option;
				this.Value = value;
			}
		}

		private void ExecuteTurnNextView()
		{
			if (this.WizardIndex++ == this.WizardSize) this.OnViewChanged(new MaintenanceViewChangedArgs(ViewChangedOption.EnterFinalView, null));
		}

		private void ExecuteTurnPreviousView()
		{
			if (this.WizardIndex-- == 0) this.OnViewChanged(new MaintenanceViewChangedArgs(ViewChangedOption.ExitMaintenanceView, null));
		}
	}
}
