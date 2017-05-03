using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Models
{
	public class ReagentInformation:ViewModel
	{
		public string Name { get; set; }

		private bool isSelected;
		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				isSelected = value;
				this.RaisePropertyChanged(nameof(IsSelected));
				this.OnViewChanged(null);
			}
		}


		public ReagentInformation(string name,bool isSelected)
		{
			this.Name = name;
			this.IsSelected = isSelected;
		}
	}
}
