using System.Collections.ObjectModel;

namespace RDS.ViewModels.ViewProperties
{
	public class ReagentRack
	{
		private readonly int ReagentBoxCount = 8;
		private readonly int MBBottleCount = 2;
		private readonly int AMPBollteCount = 4;
		private readonly int PNBottleCount = 8;
		private readonly int ISBottleCount = 4;

		public ObservableCollection<Reagent> ReagentBoxs { get; set; } = new ObservableCollection<Reagent>();
		public ObservableCollection<Reagent> MBBottles { get; set; } = new ObservableCollection<Reagent>();
		public ObservableCollection<Reagent> AMPBottles { get; set; } = new ObservableCollection<Reagent>();
		public ObservableCollection<Reagent> PNBottles { get; set; } = new ObservableCollection<Reagent>();
		public ObservableCollection<Reagent> ISBottles { get; set; } = new ObservableCollection<Reagent>();

		public ReagentRack()
		{
			this.InitializeReagents();
		}

		private void InitializeReagents()
		{
			for (int i = 0; i < this.ReagentBoxCount; i++) this.ReagentBoxs.Add(new Reagent(10,5));
			for (int i = 0; i < this.MBBottleCount; i++) this.MBBottles.Add(new Reagent(10, 5));
			for (int i = 0; i < this.AMPBollteCount; i++) this.AMPBottles.Add(new Reagent(10, 5));
			for (int i = 0; i < this.PNBottleCount; i++) this.PNBottles.Add(new Reagent(10, 5));
			for (int i = 0; i < this.ISBottleCount; i++) this.ISBottles.Add(new Reagent(10, 5));
		}
	}
}
