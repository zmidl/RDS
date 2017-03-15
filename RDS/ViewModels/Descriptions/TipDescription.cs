using RDS.ViewModels.Common;

namespace RDS.ViewModels.Descriptions
{
	public class TipDescription:ViewModel
	{
		private TipVolume tipVolume;
		public TipVolume TipVolume
		{
			get { return tipVolume; }
			set
			{
				tipVolume = value;
				this.RaisePropertyChanged(nameof(TipVolume));
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
			}
		}


		public TipDescription()
		{

		}
	}
}
