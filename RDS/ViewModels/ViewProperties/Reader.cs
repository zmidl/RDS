using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class Reader
	{
		private const int NUMBER_OFSET = 33;

		private const int STRIPS_COUNT = 5;

		private const int ENZYMES_COUNT = 6;

		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		public ObservableCollection<Enzyme> Enzymes { get; set; } = new ObservableCollection<Enzyme>();

		public Reader()
		{
			for (int i = 0; i < Reader.STRIPS_COUNT; i++) this.Strips.Add(new Strip(Reader.NUMBER_OFSET + i, RDSCL.StripState.Existence));

			for (int i = 0; i < Reader.ENZYMES_COUNT; i++) this.Enzymes.Add(new Enzyme(75));
		}
	}
}
