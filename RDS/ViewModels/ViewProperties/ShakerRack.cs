using RDS.ViewModels.Common;
using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class ShakerRack:ViewModel
	{
		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		private bool isShark;
		public bool IsShark
		{
			get { return isShark; }
			set
			{
				isShark = value;
				this.RaisePropertyChanged(nameof(IsShark));
			}
		}

		public ShakerRack()
		{
			for (int i = 0; i < 3; i++)
			{
				this.Strips.Add(new Strip(0, false));
			}
		}
	}
}
