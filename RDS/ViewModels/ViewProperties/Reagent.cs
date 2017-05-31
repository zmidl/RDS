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

					if (value > 0) this.Color = General.TextForeground3;
					else this.Color = new SolidColorBrush(Colors.White);

					this.RaisePropertyChanged(nameof(this.IsTwinkle));
					this.RaisePropertyChanged(nameof(this.Color));
				}
			}
		}

		public bool IsTwinkle { get; set; }

		public SolidColorBrush Color { get; set; }

		public string Name { get; set; }

		public Reagent(int volume = 0, int alarmVolume = 0)
		{
			this.Volume = volume;
			this.alarmVolume = alarmVolume;
		}
	}
}
