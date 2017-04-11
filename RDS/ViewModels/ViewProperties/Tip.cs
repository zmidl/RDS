using RDS.ViewModels.Common;
using Sias.Core.Attributes;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class Tip:ViewModel
	{
		private TipState tipState;
		public TipState TipState
		{
			get { return tipState; }
			set
			{
				tipState = value;
				this.RaisePropertyChanged(nameof(TipState));
				if (value == TipState.NoExist) this.TipContentColor = new SolidColorBrush(Colors.WhiteSmoke);
				else this.TipContentColor = new SolidColorBrush(Colors.Gray);
				this.RaisePropertyChanged(nameof(this.TipContentColor));
			}
		}

		public SolidColorBrush TipContentColor { get; set; }

		public Tip(TipState tipState)
		{
			this.TipState = tipState;
		}
	}
}
