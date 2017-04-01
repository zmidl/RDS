using RDS.ViewModels.Common;
using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class Strip : ViewModel
	{
		private int number;
		public int Number
		{
			get { return number; }
			set
			{
				number = value;
				this.RaisePropertyChanged(nameof(Number));
			}
		}

		public ObservableCollection<Cell> Cells { get; set; } = new ObservableCollection<Cell>();

		private RDSCL.StripState state;
		public RDSCL.StripState State
		{
			get { return state; }
			set
			{
				state = value;
				this.RaisePropertyChanged(nameof(State));
			}
		}

		private const int STRIP_SIZE = 6;

		public Strip(int number, RDSCL.StripState stripState)
		{
			this.Number = number;

			this.State = stripState;

			for (int i = 0; i < Strip.STRIP_SIZE; i++) { this.Cells.Add(new Cell(HoleState.None)); }
		}
	}
}
