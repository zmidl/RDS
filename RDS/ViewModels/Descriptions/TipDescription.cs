using RDS.ViewModels.Common;
using Sias.Core.Attributes;

namespace RDS.ViewModels.Descriptions
{
	public class TipDescription:ViewModel
	{
		private TipType tipVolume;

		[SStreamableAttribute]
		public TipType TipVolume
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
