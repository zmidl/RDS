using RDS.ViewModels.Common;
using Sias.Core.Attributes;
using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class TipRack:ViewModel
	{
		private const int TIPRACK_SIZE = 96;

		public ObservableCollection<Tip> Tips { get; set; } = new ObservableCollection<Tip>();

		private TipType tipType;

		[SStreamableAttribute]
		public TipType TipType
		{
			get { return tipType; }
			set
			{
				tipType = value;
				this.RaisePropertyChanged(nameof(TipType));
			}
		}

		public TipRack(TipType tipType)
		{
			this.TipType = tipType;

			for (int i = 0; i < TipRack.TIPRACK_SIZE; i++)
			{
				this.Tips.Add(new Tip(TipState.NoExist));
			}
		}
	}
}
