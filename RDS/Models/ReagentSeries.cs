using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Models
{
	public class ReagentSeries:ViewModel
	{
		public string Name { get; set; }

		public ObservableCollection<ReagentItem> ReagentItems { get; set; } = new ObservableCollection<ReagentItem>();

		public ReagentSeries(string name, ObservableCollection<ReagentItem>reagentItems)
		{
			this.Name = name;

			this.ReagentItems = reagentItems;
		}
	}
}
