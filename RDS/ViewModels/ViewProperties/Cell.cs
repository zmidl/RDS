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
		private HoleState cellState;
		public HoleState CellState
		{
			get { return cellState; }
			set
			{
				cellState = value;
				this.RaisePropertyChanged(nameof(CellState));

				switch (value)
				{
					case HoleState.None: { this.CellContentColor = new SolidColorBrush(Colors.WhiteSmoke); break; }
					case HoleState.Empty: { this.CellContentColor = new SolidColorBrush(Colors.Yellow); break; }
					case HoleState.Full: { this.CellContentColor = new SolidColorBrush(Colors.Brown); break; }
					default: break;
				}
				this.RaisePropertyChanged(nameof(this.CellContentColor));
			}
		}

		public SolidColorBrush CellContentColor { get; set; }

		public Cell(HoleState cellState)
		{
			this.CellState = cellState;
		}
	}
}
