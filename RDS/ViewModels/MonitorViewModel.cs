using RDS.ViewModels.Common;
using System.Collections.ObjectModel;
using RDS.ViewModels.ViewProperties;
using System;
using System.Windows;

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
			this.SampleViewModel.SampleRacks[twentyUnionSampleIndex].Samples[sampleIndex].SampleState = sampleState;
		}

		public void SetCupRackCellState(int cupRacksIndex, int stripsIndex, int cellsIndex, HoleState cellState)
		{
			this.CupRacks[cupRacksIndex].Strips[stripsIndex].Cells[cellsIndex].CellState = cellState;
		}

		public void SetShakerRackCellState(int stripsIndex, int cellsIndex, HoleState cellState)
		{
			this.ShakerRack.Strips[stripsIndex].Cells[cellsIndex].CellState = cellState;
		}

		public void SetTipState(int tipRacksIndex, int tipsIndex, TipState tipState)
		{
			this.TipRacks[tipRacksIndex].Tips[tipsIndex].TipState = tipState;
		}

		public void SetReaderCellState(int stripsIndex, int cellsIndex, HoleState cellState)
		{
			this.Reader.Strips[stripsIndex].Cells[cellsIndex].CellState = cellState;
		}

		public void SetReaderEnzymeValue(int enzymeIndex, int value)
		{
			this.Reader.Enzymes[enzymeIndex].Value += value;
		}

		public void SetSampleRackVisibility(int sampleRackIndex,Visibility visibility)
		{
			//this.SampleViewModel.SampleRacks[sampleRackIndex].Visibility = visibility;
		}

		public string TimeCount
		{
			get { return $"{fiveM.Hour.ToString("00")}:{ fiveM.Minute.ToString("00") }:{ fiveM.Second.ToString("00")}"; }
		}

		private DateTime fiveM = DateTime.Parse("00:05:00");
		private System.Timers.Timer timer;

		public void A()
		{
			this.timer = new System.Timers.Timer(1000);

			this.timer.Elapsed += Timer_Elapsed;

			this.timer.AutoReset = true;
		}

		public void B()
		{
			this.timer.Enabled = true;
			this.timer.Start();
		}

		public void C()
		{
			this.timer.Stop();
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (fiveM != Convert.ToDateTime("00:00:00"))
			{
				fiveM = fiveM.AddSeconds(-1);
				this.RaisePropertyChanged(nameof(TimeCount));
			}
		}
	}
}