using RDS.ViewModels.Common;

namespace RDS.ViewModels
{
	public class InitializeSuppliesViewModel : ViewModel
	{
		private int wizardIndex;
		public int WizardIndex
		{
			get { return wizardIndex; }
			set
			{
				if (value < 0) value = 0;
				else if (value > 7) value = 7;
				wizardIndex = value;
				this.RaisePropertyChanged(nameof(WizardIndex));
			}
		}

		public RelayCommand TurnNextView { get; private set; }
		public RelayCommand TurnHomeView { get; private set; }

		public InitializeSuppliesViewModel()
		{
			this.TurnNextView = new RelayCommand(() => this.WizardIndex++);
			this.TurnHomeView = new RelayCommand(() => this.WizardIndex = 0);
		}
	}
}
