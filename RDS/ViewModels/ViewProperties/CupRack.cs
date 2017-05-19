using RDS.ViewModels.Common;
using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class CupRack:ViewModel
	{
		public int ColumnNumber { get; set; }

		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		private bool isTwinkle;
		public bool IsTwinkle
		{
			get { return isTwinkle; }
			set
			{
				isTwinkle = value;
				this.RaisePropertyChanged(nameof(IsTwinkle));
			}
		}

		private const int CUP_RACK_SIZE = 7;

		public CupRack(int columnNumber)
		{
			this.ColumnNumber = columnNumber;

			for (int i = 1; i <= CupRack.CUP_RACK_SIZE; i++)
			{
				this.Strips.Add(new Strip(this.ColumnNumber * CUP_RACK_SIZE + i,false));
			}
		}
	}
}
