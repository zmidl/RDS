using System.Collections.Generic;
using System.Collections.ObjectModel;
using RDS.Models;
using System.Linq;
using RDS.ViewModels.Common;
using RDS.ViewModels.Descriptions;

namespace RDS.ViewModels
{
	public class SampleViewModel : ViewModel
	{
		public ObservableCollection<SampleDescription> SampleDescritions { get; set; } = new ObservableCollection<SampleDescription>();

		public ObservableCollection<SampleInformatin> SampleInformations { get; set; } = new ObservableCollection<SampleInformatin>();

		public SampleViewModel()
		{

			for (int i = 0; i < 80; i++)
			{
				SampleDescritions.Add(new SampleDescription(true, Sampling.EmergencySampling, new SampleInformatin
				{
					Age = "20",
					DateTime = System.DateTime.Now.ToString(),
					HoleSite = $"A{i}",
					Name = "王二妞",
					Reagent = "CC",
					Barcode = "12345671",
					SampleId = "AE123",
					Sex = "女",
					Type = "不明"
				}));
			}
		}

		public void SetAllSampleEmergency()
		{
			//for (int i = 0; i < 80; i++)
			//{
			//	this.SamplingStates[i] = Sampling.EmergencySampling;
			//}
		}

		private void SetColumnHole(int startHole, int endHole)
		{
			var hasSelected = false;

			var resultRange = this.SampleDescritions?.Skip(startHole).Take(endHole - startHole + 1).ToList();

			var selectedCount = resultRange.Where(o => o.IsSelected == true).Count();

			if (selectedCount > 0) hasSelected = true;

			for (int i = startHole; i <= endHole; i++) this.SampleDescritions[i].IsSelected = !hasSelected;
		}

		public void SetAHole()
		{
			this.SetColumnHole(0, 19);
		}

		public void SetBHole()
		{
			this.SetColumnHole(20, 39);
		}

		public void SetCHole()
		{
			this.SetColumnHole(40, 59);
		}

		public void SetDHole()
		{
			this.SetColumnHole(60, 79);
		}
	}
}
