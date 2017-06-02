using RDS.ViewModels.Common;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class Reagent : ViewModel
	{
		private int alarmVolume = 0;

		private int volume = 0;
		public int Volume
		{
			get { return volume; }
			set
			{
				if (value >= 0 && value <= 100)
				{
					volume = value;
					this.RaisePropertyChanged(nameof(Volume));
					if (value <= this.alarmVolume && value > 0) this.IsTwinkle = true;
					else this.IsTwinkle = false;

					if (value > 0)
					{
						switch (this.reagentType)
						{
							case ReagentType.Normal: { this.Color = General.TextForeground4; break; }
							case ReagentType.Olefin:
							case ReagentType.Enzyme: { this.Color = General.GreenColor ; break; }
						}
					}
					else this.Color = new SolidColorBrush(Colors.White);

					this.RaisePropertyChanged(nameof(this.IsTwinkle));
					this.RaisePropertyChanged(nameof(this.Color));
				}
			}
		}

		private ReagentType reagentType;

		public bool IsTwinkle { get; set; }

		public SolidColorBrush Color { get; set; }

		public string Name { get; set; }

		public Reagent(ReagentType reagentType, int volume = 0, int alarmVolume = 0)
		{
			this.reagentType = reagentType;
			this.Volume = volume;
			this.alarmVolume = alarmVolume;
		}
	}

	public enum ReagentType
	{
		Normal = 0,
		Olefin = 1,
		Enzyme = 2
	}
}
