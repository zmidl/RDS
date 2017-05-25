using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Models
{
	public class ReagentItem
	{
		public string ParentName { get; set; } = string.Empty;

		public int Level { get; set; } = 1;

		public string Name { get; set; } = string.Empty;

		public bool IsUsed { get; set; } = false;

		public ReagentItem(string parentName,string name,bool isUsed)
		{
			this.ParentName = parentName;

			this.Name = name;

			this.IsUsed = isUsed;
		}
	}
}
