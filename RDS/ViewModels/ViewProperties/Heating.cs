using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class Heating
	{
		private const int NUMBER_OFSET = 22;

		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		public OlefinBox OlefinBox { get; set; }

		public Heating()
		{
			for (int i = 0; i < 4; i++)
			{
				this.Strips.Add(new Strip(Heating.NUMBER_OFSET + i,RDSCL.StripState.Existence));
			}

			this.OlefinBox = new OlefinBox(Common.ReagentState.Empty);
		}
	}
}
