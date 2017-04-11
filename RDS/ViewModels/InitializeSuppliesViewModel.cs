using RDS.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace RDS.ViewModels
{
	public class InitializeSuppliesViewModel : ViewModel
	{
		private bool[] reagentSelections = new bool[5];

		public bool IsUuSelected
		{
			get { return reagentSelections[0]; }
			set
			{
				reagentSelections[0] = value;
				this.RaisePropertyChanged(nameof(IsUuSelected));
				this.TurnNextView.RaiseCanExecuteChanged();
			}
		}

		public bool IsCtSelected
		{
			get { return reagentSelections[1]; }
			set
			{
				reagentSelections[1] = value;
				this.RaisePropertyChanged(nameof(IsCtSelected));
				this.TurnNextView.RaiseCanExecuteChanged();
			}
		}

		public bool IsMgSelected
		{
			get { return reagentSelections[2]; }
			set
			{
				reagentSelections[2] = value;
				this.RaisePropertyChanged(nameof(IsMgSelected));
				this.TurnNextView.RaiseCanExecuteChanged();
			}
		}

		public bool IsNgSelected
		{
			get { return reagentSelections[3]; }
			set
			{
				reagentSelections[3] = value;
				this.RaisePropertyChanged(nameof(IsNgSelected));
				this.TurnNextView.RaiseCanExecuteChanged();
			}
		}

		public bool IsMpSelected
		{
			get { return reagentSelections[4]; }
			set
			{
				reagentSelections[4] = value;
				this.RaisePropertyChanged(nameof(IsMpSelected));
				this.TurnNextView.RaiseCanExecuteChanged();
			}
		}

		private bool isLeft;
		public bool IsLeft
		{
			get { return isLeft; }
			set
			{
				isLeft = value;
				this.RaisePropertyChanged(nameof(this.IsRight));
				this.RaisePropertyChanged(nameof(IsLeft));
				this.RaisePropertyChanged(nameof(LeftColor));
				this.RaisePropertyChanged(nameof(RightColor));
			}
		}

		public bool IsRight
		{
			get { return !this.IsLeft; }
			set
			{
				this.isLeft = !value;
				this.RaisePropertyChanged(nameof(this.IsLeft));
				this.RaisePropertyChanged(nameof(IsRight));
				this.RaisePropertyChanged(nameof(RightColor));
				this.RaisePropertyChanged(nameof(LeftColor));
			}
		}
	
		public SolidColorBrush LeftColor
		{
			get { return this.isLeft?new SolidColorBrush(Colors.Blue):new SolidColorBrush(Colors.Black); }
		}
		public SolidColorBrush RightColor
		{
			get { return !this.isLeft ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.Black); }
		}

		private int wizardIndex;
		public int WizardIndex
		{
			get { return wizardIndex; }
			set
			{
				if (value < 0) value = 0;
				else if (value > 13) value = 13;
				wizardIndex = value;
				this.RaisePropertyChanged(nameof(WizardIndex));
				this.TurnNextView.RaiseCanExecuteChanged();
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

		public RelayCommand TurnNextView { get; private set; }
		public RelayCommand TurnHomeView { get; private set; }

		public InitializeSuppliesViewModel()
		{
			this.TurnNextView = new RelayCommand(this.ExecuteTurnNextView,this.CanExecuteTurnNextView);
			this.TurnHomeView = new RelayCommand(() => { this.WizardIndex = 0;this.RaiseWizards(); });
			this.RaiseWizards();
		}

		private void ExecuteTurnNextView()
		{
			if (this.WizardIndex++ == 13) { this.OnViewChanged(2); };
			this.RaiseWizards();
		}

		private bool CanExecuteTurnNextView()
		{
			bool result = default(bool);
			var count = this.reagentSelections.Where(o => o == true).Count();
			if(this.wizardIndex==4)
			{
				if (count > 0 && count < 5) result = true;
			}
			else
			{
				result = true;
			}
			return result;
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
		}
	}
}
