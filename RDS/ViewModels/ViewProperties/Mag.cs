using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.ViewModels.ViewProperties
{
	public class Mag
	{
		private const int MAG_SIZE = 4;
		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		public Mag()
		{
			for (int i = 0; i < Mag.MAG_SIZE; i++)
			{
				this.Strips.Add(new Strip(0, false));
			}
		}
	}
}
