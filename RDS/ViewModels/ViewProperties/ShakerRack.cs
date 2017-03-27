using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class ShakerRack
	{
		private const int NUMBER_OFSET = 26;

		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		public ShakerRack()
		{
			for (int i = 0; i < 3; i++)
			{
				this.Strips.Add(new Strip(ShakerRack.NUMBER_OFSET + i, RDSCL.StripState.Existence));
			}
		}
	}
}
