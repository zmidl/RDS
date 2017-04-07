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
		private HoleState state;
		public HoleState State
		{
			get { return state; }
			set
			{
				state = value;
				this.RaisePropertyChanged(nameof(State));

				switch (value)
				{
					case HoleState.None: { this.Color = new SolidColorBrush(Colors.WhiteSmoke); break; }
					case HoleState.Empty: { this.Color = new SolidColorBrush(Colors.Yellow); break; }
					case HoleState.Full: { this.Color = new SolidColorBrush(Colors.Brown); break; }
					default: break;
				}
				this.RaisePropertyChanged(nameof(this.Color));
			}
		}

		public SolidColorBrush Color { get; set; }

		public Cell(HoleState cellState)
		{
			this.State = cellState;
		}
	}
}
