using RDS.ViewModels.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace RDS.ViewModels
{
	public class MonitorViewModel : ViewModel
	{
		private ObservableCollection<bool> tubStates=new ObservableCollection<bool>(new List<bool>() { false,false,false,false,false,false});
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

		public SampleViewModel SampleViewModel { get; set; } = new SampleViewModel();

		public RelayCommand Emergency { get; private set; }

		public MonitorViewModel()
		{

			this.Emergency = new RelayCommand(() => this.OnViewChanged(1));
		}

	}
}
//private List<Sampling> samplings=new List<Sampling>();
//public Sampling A1
//{ 
//	get { return samplings[0]; }
//	set { samplings[0] = value;this.RaisePropertyChanged(nameof(A1)); }
//}
//public Sampling A2
//{
//	get { return samplings[1]; }
//	set { samplings[1] = value; this.RaisePropertyChanged(nameof(A2)); }
//}
//public Sampling A3
//{
//	get { return samplings[2]; }
//	set { samplings[2] = value; this.RaisePropertyChanged(nameof(A3)); }
//}
//public Sampling A4
//{
//	get { return samplings[3]; }
//	set { samplings[3] = value; this.RaisePropertyChanged(nameof(A4)); }
//}
//public Sampling A5
//{
//	get { return samplings[4]; }
//	set { samplings[4] = value; this.RaisePropertyChanged(nameof(A5)); }
//}
//public Sampling A6
//{
//	get { return samplings[5]; }
//	set { samplings[5] = value; this.RaisePropertyChanged(nameof(A6)); }
//}
//public Sampling A7
//{
//	get { return samplings[6]; }
//	set { samplings[6] = value; this.RaisePropertyChanged(nameof(A7)); }
//}
//public Sampling A8
//{
//	get { return samplings[7]; }
//	set { samplings[7] = value; this.RaisePropertyChanged(nameof(A8)); }
//}
//public Sampling A9
//{
//	get { return samplings[8]; }
//	set { samplings[8] = value; this.RaisePropertyChanged(nameof(A9)); }
//}
//public Sampling A10
//{
//	get { return samplings[9]; }
//	set { samplings[9] = value; this.RaisePropertyChanged(nameof(A10)); }
//}