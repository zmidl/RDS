using RDS.Models;
using RDS.ViewModels.Common;

namespace RDS.ViewModels.Descriptions
{
	public class SampleDescription:ViewModel
	{
		private bool isSelected;
		public bool IsSelected
		{
			get { return isSelected; }
			set { isSelected = value; this.RaisePropertyChanged(nameof(IsSelected)); }
		}

		private Sampling state;
		public Sampling State
		{
			get { return state; }
			set { state = value; this.RaisePropertyChanged(nameof(State)); }
		}

		public SampleInformatin Information { get; set; }

		public SampleDescription(bool isSelected,Sampling state,SampleInformatin information)
		{
			this.IsSelected = isSelected;
			this.State = state;
			this.Information = information;
		}
	}
}
