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

		private bool? isLoaded;
		public bool? IsLoaded
		{
			get { return isLoaded; }
			set
			{
				isLoaded = value;
				this.RaisePropertyChanged(nameof(IsLoaded));
			}
		}

		private bool isMoving;
		public bool IsMoving
		{
			get { return isMoving; }
			set
			{
				isMoving = value;
				this.RaisePropertyChanged(nameof(IsMoving));
			}
		}


		private const int STRIP_SIZE = 6;

		public Strip(int number, bool isLoaded)
		{
			this.Number = number;

			this.IsLoaded = isLoaded;

			for (int i = 0; i < Strip.STRIP_SIZE; i++) { this.Cells.Add(new Cell(false)); }
		}

		public Strip()
		{
			this.Number = 0;
			this.IsLoaded = false;
			this.IsMoving = false;
			for (int i = 0; i < Strip.STRIP_SIZE; i++) { this.Cells.Add(new Cell(false)); }
		}
	}
}
