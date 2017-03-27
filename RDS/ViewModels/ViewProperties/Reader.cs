using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.ViewModels.ViewProperties
{
	public class Reader
	{
		private const int NUMBER_OFSET = 33;

		private const int STRIPS_COUNT = 5;

		public ObservableCollection<Strip> Strips { get; set; } = new ObservableCollection<Strip>();

		public Reader()
		{
			for (int i = 0; i < Reader.STRIPS_COUNT; i++)
			{
				this.Strips.Add(new Strip(Reader.NUMBER_OFSET + i, RDSCL.StripState.Existence));
			}
		}
	}
}
