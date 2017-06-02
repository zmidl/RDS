using RDS.ViewModels.Common;
using Sias.Core.Attributes;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class Tip : ViewModel
	{
		private bool isLoaded;
		public bool IsLoaded
		{
			get { return isLoaded; }
			set
			{
				isLoaded = value;
				this.RaisePropertyChanged(nameof(IsLoaded));
				if (value) this.TipContentColor = General.TextForeground4;
				else this.TipContentColor = new SolidColorBrush(Colors.White);
				this.RaisePropertyChanged(nameof(this.TipContentColor));
			}
		}

		public SolidColorBrush TipContentColor { get; set; }

		public Tip()
		{
			this.IsLoaded = false;
		}
	}
}
