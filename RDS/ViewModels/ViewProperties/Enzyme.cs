using RDS.ViewModels.Common;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class Enzyme:ViewModel
	{
		private int value;
		public int Value
		{
			get { return value; }
			set
			{
				this.value = value;
				this.RaisePropertyChanged(nameof(Value));
			}
		}

		public SolidColorBrush EnzymeContentColor { get; set; }

		public Enzyme(int value)
		{
			this.Value = value;
		}
	}
}
