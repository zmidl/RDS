using RDS.ViewModels.Common;
using System;
using System.Windows.Media;

namespace RDS.ViewModels.Descriptions
{
	public class Sample : ViewModel
	{
		public Action NotifyRaiseProperty;

		public string HoleName { get; set; } = string.Empty;

		private string barcode = string.Empty;
		public string Barcode
		{
			get { return barcode; }
			set
			{
				barcode = value;
				this.RaisePropertyChanged(nameof(Barcode));
			}
		}

		public SolidColorBrush Test { get; set; }

		public SampleState SampleState { get; set; }

		public Sample() { }

		public void SetSampleState(SampleState sampleState)
		{
			this.SampleState = sampleState;
			this.NotifyRaiseProperty();
		}

		public void SetTest(Color c)
		{
			this.Test = new SolidColorBrush(c);
			this.NotifyRaiseProperty();
		}

		public Sample(string holeName)
		{
			this.HoleName = holeName;
			this.SampleState = SampleState.NoSample;
		}

		public Sample(string holeName,SampleState sampleState)
		{
			this.HoleName = holeName;
			this.SampleState = sampleState;
		}
	}
}
