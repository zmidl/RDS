using RDS.ViewModels.Common;
using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class Reader : ViewModel
	{
		private const int STRIPS_COUNT = 5;

		private readonly int EnzymeBottlesCount = 6;

		private int temperature = 0;
		public int Temperature
		{
			get { return temperature; }
			set
			{
				temperature = value;
				this.RaisePropertyChanged(nameof(Temperature));

				if (value >= 50) this.IsWarmAlarm = true;
				else this.IsWarmAlarm = false;
				this.RaisePropertyChanged(nameof(this.IsWarmAlarm));
			}
		}

		public bool? IsWarmAlarm { get; set; } = false;

		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		public ObservableCollection<Reagent> EnzymeBottles { get; set; } = new ObservableCollection<Reagent>();

		public Reader()
		{
			for (int i = 0; i < Reader.STRIPS_COUNT; i++) this.Strips.Add(new Strip(0, false));

			for (int i = 0; i < this.EnzymeBottlesCount; i++) this.EnzymeBottles.Add(new Reagent(ReagentType.Normal, 0,5));
		}
	}
}
