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

		private bool isExist;
		public bool IsExist
		{
			get { return isExist; }
			set
			{
				isExist = value;
				this.RaisePropertyChanged(nameof(IsExist));
			}
		}


		private const int STRIP_SIZE = 6;

		public Strip(int number, bool isExist)
		{
			this.Number = number;

			this.IsExist = isExist;

			for (int i = 0; i < Strip.STRIP_SIZE; i++) { this.Cells.Add(new Cell(false)); }
		}
	}
}
