using RDS.ViewModels.Common;
using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class Heating:ViewModel
	{
		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		public Reagent OlefinBox { get; set; }

		private int temperature = 0;
		public int Temperature
		{
			get { return temperature; }
			set
			{
				if (value >= 0 && value <= 100)
				{
					temperature = value;
					this.RaisePropertyChanged(nameof(Temperature));

					if (value >= 50) this.IsWarmAlarm = true;
					else this.IsWarmAlarm = false;
					this.RaisePropertyChanged(nameof(this.IsWarmAlarm));
				}
			}
		}

		public bool? IsWarmAlarm { get; set; } = false;

		public Heating()
		{
			for (int i = 0; i < 4; i++)
			{
				this.Strips.Add(new Strip(0,false));
			}

			this.OlefinBox = new Reagent(ReagentType.Normal, 0,5);
		}
	}
}
