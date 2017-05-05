using RDS.ViewModels.Common;
using System;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class SampleTube : ViewModel
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

		private SampleTubeState sampleState;
		public SampleTubeState SampleState
		{
			get { return sampleState; }
			set
			{
				sampleState = value;
				this.RaisePropertyChanged(nameof(SampleState));
				switch(value)
				{
					case SampleTubeState.NoSampleTube: { this.SampleContentColor = new SolidColorBrush(Colors.WhiteSmoke); break; }
					case SampleTubeState.Normal: { this.SampleContentColor = new SolidColorBrush(Colors.Brown); break; }
					case SampleTubeState.Emergency: { this.SampleContentColor = new SolidColorBrush(Colors.Blue); break; }
					case SampleTubeState.Sampling: { this.SampleContentColor = new SolidColorBrush(Colors.Green); break; }
					case SampleTubeState.Sampled: { this.SampleContentColor = new SolidColorBrush(Colors.Gray); break; }
					default: break;
				}
				this.RaisePropertyChanged(nameof(this.SampleContentColor));
			}
		}

		public SolidColorBrush SampleContentColor { get; set; }

		private bool isSampling;
		public bool IsSampling
		{
			get { return isSampling; }
			set
			{
				isSampling = value;
				this.RaisePropertyChanged(nameof(IsSampling));
			}
		}

		public SampleTube() { }

		public void SetSampleState(SampleTubeState sampleState)
		{
			this.SampleState = sampleState;
			this.NotifyRaiseProperty();
		}

		public SampleTube(string holeName)
		{
			this.HoleName = holeName;
			this.SampleState = SampleTubeState.NoSampleTube;
		}

		public SampleTube(string holeName,SampleTubeState sampleState)
		{
			this.HoleName = holeName;
			this.SampleState = sampleState;
			this.IsSampling = false;
		}
	}
}
