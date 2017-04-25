using RDS.ViewModels.Common;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class Enzyme:ViewModel
	{
		private int volume;
		public int Volume
		{
			get { return volume; }
			set
			{
				this.volume = value;
				this.RaisePropertyChanged(nameof(Volume));
			}
		}

		public SolidColorBrush EnzymeContentColor { get; set; }

		public Enzyme(int value)
		{
			this.Volume = value;
		}
	}
}
