using RDS.Models;
using RDS.ViewModels.Common;

namespace RDS.ViewModels.Descriptions
{
	public class SampleDescription:ViewModel
	{
		public bool IsSelected { get; set; } = false;

		private bool selectionState;
		public bool SelectionState
		{
			get { return selectionState; }
			set { selectionState = value; this.RaisePropertyChanged(nameof(SelectionState)); }
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
			this.SelectionState = isSelected;
			this.State = state;
			this.Information = information;
		}
	}
}
