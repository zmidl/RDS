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

		public ObservableCollection<ReagentBox> ReagentBoxs { get; set; } = new ObservableCollection<ReagentBox>();
		public ObservableCollection<MBBottle> MBBottles { get; set; } = new ObservableCollection<MBBottle>();
		public ObservableCollection<AMPBottle> AMPBottles { get; set; } = new ObservableCollection<AMPBottle>();
		public ObservableCollection<PNBottle> PNBottles { get; set; } = new ObservableCollection<PNBottle>();
		public ObservableCollection<ISBottle> ISBottles { get; set; } = new ObservableCollection<ISBottle>();

		public ReagentRack()
		{
			this.Initialize();
		}

		private void Initialize()
		{
			for (int i = 0; i < this.ReagentBoxCount; i++) this.ReagentBoxs.Add(new ReagentBox(Common.ReagentState.Empty));
			for (int i = 0; i < this.MBBottleCount; i++) this.MBBottles.Add(new MBBottle(Common.ReagentState.Empty));
			for (int i = 0; i < this.AMPBollteCount; i++) this.AMPBottles.Add(new AMPBottle(Common.ReagentState.Empty));
			for (int i = 0; i < this.PNBottleCount; i++) this.PNBottles.Add(new PNBottle(Common.ReagentState.Empty));
			for (int i = 0; i < this.ISBottleCount; i++) this.ISBottles.Add(new ISBottle(Common.ReagentState.Empty));
		}
	}
}
