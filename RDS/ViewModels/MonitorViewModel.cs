using RDS.ViewModels.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Linq;

namespace RDS.ViewModels
{
	public class MonitorViewModel : ViewModel
	{
		public SampleViewModel SampleViewModel { get; set; } = new SampleViewModel();

		public RelayCommand Emergency { get; private set; }

		public ObservableCollection<SolidColorBrush> Mixtures { get; set; } = new ObservableCollection<SolidColorBrush>(new List<SolidColorBrush>()
		{
			new SolidColorBrush(Colors.Gray),
			new SolidColorBrush(Colors.Gray),
			new SolidColorBrush(Colors.Gray),
			new SolidColorBrush(Colors.Gray),
			new SolidColorBrush(Colors.Gray),
			new SolidColorBrush(Colors.Gray)
		});

	



		public void ShowSampleView()
		{
			this.OnViewChanged(ShowView.ShowSampleView);
		}

		
		public MonitorViewModel()
		{
			this.Emergency = new RelayCommand(this.ShowSampleView);
		}

	}
}