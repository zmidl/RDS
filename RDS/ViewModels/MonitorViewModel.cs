using RDS.ViewModels.Common;
using System.Collections.ObjectModel;
using RDS.ViewModels.ViewProperties;

namespace RDS.ViewModels
{
	public class MonitorViewModel : ViewModel
	{
		private const int CUPRACKS_COUNT = 3;

		private const int TIPRACKS_COUNT = 9;

		public SampleViewModel SampleViewModel { get; set; } = new SampleViewModel();

		public ObservableCollection<CupRack> CupRacks { get; set; } = new ObservableCollection<CupRack>();

		public ObservableCollection<TipRack> TipRacks { get; set; } = new ObservableCollection<TipRack>();

		private Heating heating = new Heating();
		public Heating Heating
		{
			get { return heating; }
			set
			{
				heating = value;
				this.RaisePropertyChanged(nameof(Heating));
			}
		}

		private ShakerRack shakerRack = new ShakerRack();
		public ShakerRack ShakerRack
		{
			get { return shakerRack; }
			set
			{
				shakerRack = value;
				this.RaisePropertyChanged(nameof(ShakerRack));
			}
		}

		private Mag mag = new Mag();
		public Mag Mag
		{
			get { return mag; }
			set
			{
				mag = value;
				this.RaisePropertyChanged(nameof(Mag));
			}
		}

		private Reader reader = new Reader();
		public Reader Reader
		{
			get { return reader; }
			set
			{
				reader = value;
				this.RaisePropertyChanged(nameof(Reader));
			}
		}

		public RelayCommand Emergency { get; private set; }

		public void ShowSampleView()
		{
			this.OnViewChanged(ShowView.ShowSampleView);
		}

		public MonitorViewModel()
		{
			this.InitializeCupRacks(MonitorViewModel.CUPRACKS_COUNT);

			this.InitializeTipRacks(MonitorViewModel.TIPRACKS_COUNT);

			this.Emergency = new RelayCommand(() => {; }/*this.ShowSampleView*/);
		}

		private void InitializeTipRacks(int tipRacksCount)
		{
			for (int i = 0; i < tipRacksCount; i++)
			{
				this.TipRacks.Add(new TipRack(TipType._1000uL));
			}
		}

		private void InitializeCupRacks(int cupRacksCount)
		{
			for (int i = 0; i < cupRacksCount; i++)
			{
				this.CupRacks.Add(new CupRack(i));
			}
		}

		public void SetSampleTubeState(int twentyUnionSampleIndex, int sampleIndex, SampleTubeState sampleState)
		{
			this.SampleViewModel.TwentyUnionSampleHoles[twentyUnionSampleIndex].Samples[sampleIndex].SampleState = sampleState;
		}

		public void SetCupRackCellState(int cupRacksIndex, int stripsIndex, int cellsIndex, CellState cellState)
		{
			this.CupRacks[cupRacksIndex].Strips[stripsIndex].Cells[cellsIndex].CellState = cellState;
		}

		public void SetShakerRackCellState(int stripsIndex, int cellsIndex, CellState cellState)
		{
			this.ShakerRack.Strips[stripsIndex].Cells[cellsIndex].CellState = cellState;
		}

		public void SetTipState(int tipRacksIndex, int tipsIndex, TipState tipState)
		{
			this.TipRacks[tipRacksIndex].Tips[tipsIndex].TipState = tipState;
		}
	}
}