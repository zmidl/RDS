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

		public string RemainingTime
		{
			get { return remainingTime.ToString(Properties.Resources.RemainingTimeFormat); }
		}

		private DateTime remainingTime = DateTime.Parse("00:00:05");

		private System.Timers.Timer remainingTimer;

		public SampleViewModel SampleViewModel { get; set; } = new SampleViewModel();

		public ObservableCollection<CupRack> CupRacks { get; set; } = new ObservableCollection<CupRack>();

		public ObservableCollection<TipRack> TipRacks { get; set; } = new ObservableCollection<TipRack>();

		public ReagentRack ReagentRack { get; set; } = new ReagentRack();

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

		public ShakerRack ShakerRack { get; set; } = new ShakerRack();

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

		private bool isStartTask;
		public bool IsStartTask
		{
			get { return isStartTask; }
			set
			{
				isStartTask = value;
				this.RaisePropertyChanged(nameof(IsStartTask));
				if (value) this.StartRemainingTimer();
				else this.StopRemainingTimer();
			}
		}

		public enum ViewChangedOption
		{
			ShowSampleView = 0,
			TaskStop = 1
		}

		public class MonitorViewChangedArgs : EventArgs
		{
			public ViewChangedOption Option { get; set; }
			public object Value { get; set; }

			public MonitorViewChangedArgs(ViewChangedOption option, object value)
			{
				this.Option = option;
				this.Value = value;
			}
		}

		public RelayCommand Emergency { get; private set; }

		public void ShowSampleView()
		{
			this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.ShowSampleView,null));
		}

		public MonitorViewModel()
		{
			this.InitializeCupRacks(MonitorViewModel.CUPRACKS_COUNT);

			this.InitializeTipRacks(MonitorViewModel.TIPRACKS_COUNT);

			this.Emergency = new RelayCommand(this.ShowSampleView);

			this.InitializeRemainingTimer();
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
			this.SampleViewModel.FourSampleRackDescriptions[twentyUnionSampleIndex].Samples[sampleIndex].SampleState = sampleState;
		}

		public void SetCupRackCellState(int cupRacksIndex, int stripsIndex, int cellsIndex, HoleState cellState)
		{
			this.CupRacks[cupRacksIndex].Strips[stripsIndex].Cells[cellsIndex].State = cellState;
		}

		public void SetShakerRackCellState(int stripsIndex, int cellsIndex, HoleState cellState)
		{
			this.ShakerRack.Strips[stripsIndex].Cells[cellsIndex].State = cellState;
		}

		public void SetTipState(int tipRacksIndex, int tipsIndex, TipState tipState)
		{
			this.TipRacks[tipRacksIndex].Tips[tipsIndex].TipState = tipState;
		}

		public void SetReaderCellState(int stripsIndex, int cellsIndex, HoleState cellState)
		{
			this.Reader.Strips[stripsIndex].Cells[cellsIndex].State = cellState;
		}

		public void SetReaderEnzymeBottlesValue(int enzymeIndex, int value)
		{
			this.Reader.EnzymeBottles[enzymeIndex].Volume += value;
		}

		public void SetReaderEnzymeBottlesState(int enzymeIndex, HoleState holeSstate)
		{
			this.Reader.EnzymeBottles[enzymeIndex].State = ReagentState.Full;
		}

		public void SetSampleRackState(int sampleRackIndex, RDSCL.SampleRackState sampleRackState)
		{
			this.SampleViewModel.FourSampleRackDescriptions[sampleRackIndex].SampleRackState = sampleRackState;
			//(RDSCL.SampleRackState)Enum.Parse(typeof(RDSCL.SampleRackState), stateDescription);
		}

		public void SetReagentBoxState(int reagentBoxIndex, ReagentState reagentState)
		{
			this.ReagentRack.ReagentBoxs[reagentBoxIndex].State = reagentState;
		}

		public void SetReagentBoxVolume(int reagentBoxIndex, int volume)
		{
			this.ReagentRack.ReagentBoxs[reagentBoxIndex].Volume = volume;
		}

		public void SetMBBottleState(int mBBottleIndex, ReagentState reagentState)
		{
			this.ReagentRack.MBBottles[mBBottleIndex].State = reagentState;
		}

		public void SetMBBottleVolume(int mBBottleIndex, int volume)
		{
			this.ReagentRack.MBBottles[mBBottleIndex].Volume = volume;
		}

		public void SetAMPBottleState(int aMPBottleIndex, ReagentState reagentState)
		{
			this.ReagentRack.AMPBottles[aMPBottleIndex].State = reagentState;
		}

		public void SetNPBottleState(int nPBottleIndex, ReagentState reagentState)
		{
			this.ReagentRack.PNBottles[nPBottleIndex].State = reagentState;
		}

		public void SetISBottleState(int iSBottleIndex, ReagentState reagentState)
		{
			this.ReagentRack.ISBottles[iSBottleIndex].State = reagentState;
		}

		public void InitializeRemainingTimer()
		{
			this.remainingTimer = new System.Timers.Timer(1000);

			this.remainingTimer.Elapsed += Timer_Elapsed;

			this.remainingTimer.AutoReset = true;
		}

		public void StartRemainingTimer()
		{
			this.remainingTimer.Enabled = true;
			this.remainingTimer.Start();
		}

		public void StopRemainingTimer()
		{
			this.remainingTimer.Stop();
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (remainingTime != Convert.ToDateTime(Properties.Resources.TimeOut))
			{
				remainingTime = remainingTime.AddSeconds(-1);
				this.RaisePropertyChanged(nameof(RemainingTime));
			}
			else
			{
				this.StopRemainingTimer();
				this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.TaskStop, null));
			}
		}
	}
}