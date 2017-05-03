using RDS.ViewModels.Common;
using System.Windows.Media;

namespace RDS.ViewModels.ViewProperties
{
	public class Reagent : ViewModel
	{
		private int volume = 0;
		public int Volume
		{
			get { return volume; }
			set
			{
				volume = value;
				this.RaisePropertyChanged(nameof(Volume));
			}
		}

		private ReagentState state;
		public ReagentState State
		{
			get { return state; }
			set
			{
				state = value;
				this.RaisePropertyChanged(nameof(State));

				switch (value)
				{
					case ReagentState.Empty: { this.Color = new SolidColorBrush(Colors.Gray); break; }
					case ReagentState.Few: { this.Color = new SolidColorBrush(Colors.Yellow); break; }
					case ReagentState.Normal: { this.Color = new SolidColorBrush(Colors.Brown); break; }
					case ReagentState.Full: { this.Color = new SolidColorBrush(Colors.Green); break; }
					default: break;
				}
				this.RaisePropertyChanged(nameof(this.Color));
			}
		}

		public SolidColorBrush Color { get; set; }

		public Reagent(ReagentState reagentBoxState)
		{
			this.State = reagentBoxState;
		}
	}

	public class ReagentBox : Reagent
	{
		public ReagentBox(ReagentState reagentState) : base(reagentState) { }
	}

	public class MBBottle : Reagent
	{
		public MBBottle(ReagentState reagentState) : base(reagentState) { }
	}

	public class AMPBottle : Reagent
	{
		public AMPBottle(ReagentState reagentState) : base(reagentState) { }
	}

	public class PNBottle : Reagent
	{
		public PNBottle(ReagentState reagentState) : base(reagentState) { }
	}

	public class ISBottle : Reagent
	{
		public ISBottle(ReagentState reagentState) : base(reagentState) { }
	}

	public class OlefinBox : Reagent
	{
		public OlefinBox(ReagentState reagentState) : base(reagentState) { }
	}

	public class EnzymeBottle : Reagent
	{
		public EnzymeBottle(ReagentState reagentState) : base(reagentState) { }
	}

}
