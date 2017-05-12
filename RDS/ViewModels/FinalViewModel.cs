using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.ViewModels
{
	public class FinalViewModel:ViewModel
	{
		private int wizardIndex;
		public int WizardIndex
		{
			get { return wizardIndex; }
			set
			{
				wizardIndex = value;
				this.RaisePropertyChanged(nameof(WizardIndex));
			}
		}


		public RelayCommand CloseHatch { get; private set; }

		public FinalViewModel()
		{
			this.CloseHatch = new RelayCommand(this.ExecuteCloseHatch);
		}

		private void ExecuteCloseHatch()
		{
			switch(this.WizardIndex)
			{
				case 0:
				{
					General.ShowMessage(General.FindStringResource(Properties.Resources.FinalView_Message3));
					this.WizardIndex++;
					break;
				}
				case 1:
				{
					General.ShutDown();
					break;
				}
				default:break;
			}
		}
	}
}
