using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Models
{
	public class ReagentItem
	{
		public string Name { get; set; } = string.Empty;

		public bool IsUsed { get; set; } = false;

		public ReagentItem(string name,bool isUsed)
		{
			this.Name = name;

			this.IsUsed = isUsed;
		}
	}
}
