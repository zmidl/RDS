using RDS.ViewModels.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace RDS.ViewModels
{
	public class MonitorViewModel : ViewModel
	{
		private ObservableCollection<bool> tubStates = new ObservableCollection<bool>(new List<bool>() { false, false, false, false, false, false });
		public ObservableCollection<bool> TubStates
		{
			get { return tubStates; }
			set { tubStates = value; }
		}

		public byte TempelateValue { get; set; } = 255;
		public SolidColorBrush TempelateColor
		{
			get
			{
				var resullt = default(SolidColorBrush);
				if (this.TempelateValue < 1) this.TempelateValue = 1;
				else if (this.TempelateValue > 254) this.TempelateValue = 254;
				resullt = new SolidColorBrush(Color.FromRgb(255, this.TempelateValue, 0));
				return resullt;
			}
		}
		public void RaiseTempelateColor()
		{
			this.RaisePropertyChanged(nameof(this.TempelateColor));
		}

		public void ShowSampleView()
		{
			this.OnViewChanged(ShowView.ShowSampleView);
		}

		public SampleViewModel SampleViewModel { get; set; } = new SampleViewModel();

		public RelayCommand Emergency { get; private set; }

		public MonitorViewModel()
		{

			this.Emergency = new RelayCommand(this.ShowSampleView);
		}

	}
}