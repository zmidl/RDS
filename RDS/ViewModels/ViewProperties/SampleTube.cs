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

		private bool isLoaded;
		public bool IsLoaded
		{
			get { return isLoaded; }
			set
			{
				isLoaded = value;
				this.RaisePropertyChanged(nameof(IsLoaded));
				if (value) this.SampleContentColor = new SolidColorBrush(Colors.WhiteSmoke);
				else this.SampleContentColor = new SolidColorBrush(Colors.Gray);
				this.RaisePropertyChanged(nameof(this.SampleContentColor));
			}
		}

		public SolidColorBrush SampleContentColor { get; set; }

		private bool isSampling = false;
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

		

		public SampleTube(string holeName)
		{
			this.HoleName = holeName;

		}
	}
}
