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

		private SampleState sampleState;
		public SampleState SampleState
		{
			get { return sampleState; }
			set
			{
				sampleState = value;
				this.RaisePropertyChanged(nameof(SampleState));
				switch(value)
				{
					case SampleState.NoSample: { this.SampleContentColor = new SolidColorBrush(Colors.WhiteSmoke); break; }
					case SampleState.Normal: { this.SampleContentColor = new SolidColorBrush(Colors.Brown); break; }
					case SampleState.Emergency: { this.SampleContentColor = new SolidColorBrush(Colors.Blue); break; }
					case SampleState.Sampling: { this.SampleContentColor = new SolidColorBrush(Colors.Green); break; }
					case SampleState.Sampled: { this.SampleContentColor = new SolidColorBrush(Colors.Gray); break; }
					default: break;
				}
				this.RaisePropertyChanged(nameof(this.SampleContentColor));
			}
		}

		public SolidColorBrush SampleContentColor { get; set; }


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
