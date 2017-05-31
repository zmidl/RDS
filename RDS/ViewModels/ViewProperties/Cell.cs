using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class Cell:ViewModel
	{
		private bool isLoaded;
		public bool IsLoaded
		{
			get { return isLoaded; }
			set
			{
				isLoaded = value;
				this.RaisePropertyChanged(nameof(IsLoaded));
				if (value) this.Color = General.TextForeground3;
				else this.Color = new SolidColorBrush(Colors.White);
				this.RaisePropertyChanged(nameof(this.Color));
			}
		}

		public SolidColorBrush Color { get; set; }

		public Cell(bool isloaded)
		{
			this.IsLoaded = isloaded;
		}
	}
}
