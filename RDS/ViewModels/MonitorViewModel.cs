using RDS.ViewModels.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Linq;
using System.Windows;
using RDS.ViewModels.Descriptions;

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


		//public ObservableCollection<SixUnionMixture> SixUnionMixture { get; set; } = new ObservableCollection<SixUnionMixture>();





		public void ShowSampleView()
		{
			this.OnViewChanged(ShowView.ShowSampleView);
		}

		
		public MonitorViewModel()
		{
			this.Emergency = new RelayCommand(this.ShowSampleView);
		}

		public void SetSampleState(int twentyUnionSampleIndex,int sampleIndex,SampleState sampleState)
		{
			this.SampleViewModel.TwentyUnionSampleHoles[twentyUnionSampleIndex].Samples[sampleIndex].SampleState = sampleState;
		}

		public void SetSampleVisibility(int twentyUnionSampleIndex,Visibility visibility)
		{
			this.SampleViewModel.TwentyUnionSampleHoles[twentyUnionSampleIndex].Visibility = visibility;
		}

	}
}