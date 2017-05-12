using RDS.ViewModels.Common;
using System.Collections.ObjectModel;
using RDS.ViewModels.ViewProperties;
using System;

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

		private readonly int yearUnit = 1;
		private readonly int monthUnit = 1;
		private readonly int dateUnit = 1;
		private int hourUnit = 0;
		private int minuteUnit = 0;
		private int secondUnit = 5;

		private DateTime remainingTime;

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
				if (value)
				{
					if (this.SamplingResult == true)
					{
						this.StartRemainingTimer();
						isStartTask = value;
						this.RaisePropertyChanged(nameof(IsStartTask));
					}
					else General.ShowMessage("开始前请确保上样完成。");
				}

				else
				{
					this.StopRemainingTimer();
					isStartTask = value;
					this.RaisePropertyChanged(nameof(IsStartTask));
				}
			}
		}

		public bool? SamplingResult { get; private set; } = false;


		public enum ViewChangedOption
		{
			ShowSampleView = 0,
			TaskStop = 1,
			NotifySamplingResult = 2
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

		public MonitorViewModel()
		{
			this.InitializeCupRacks(MonitorViewModel.CUPRACKS_COUNT);

			this.InitializeTipRacks(MonitorViewModel.TIPRACKS_COUNT);

			this.Emergency = new RelayCommand(this.ShowSampleView);

			this.InitializeRemainingTimer();

			this.remainingTime = new DateTime(yearUnit, monthUnit, dateUnit, hourUnit, minuteUnit, secondUnit);

			
			//this.Heating.Strips[0].IsExist = false;
			//this.Heating.Strips[1].IsExist = false;
			//this.Heating.Strips[2].IsExist = false;
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

		public void ShowSampleView()
		{
			this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.ShowSampleView, null));
		}

		public void SetSamplingResult(bool samplingResult)
		{
			this.SamplingResult = samplingResult;
			this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.NotifySamplingResult, samplingResult));
		}

		public void SetSampleTubeState(int twentyUnionSampleIndex, int sampleIndex, SampleTubeState sampleState)
		{
			this.SampleViewModel.FourSampleRackDescriptions[twentyUnionSampleIndex].Samples[sampleIndex].SampleState = sampleState;
		}

		public void SetCupRackCellState(int cupRacksIndex, int stripsIndex, int cellsIndex, bool isLoaded)
		{
			this.CupRacks[cupRacksIndex].Strips[stripsIndex].Cells[cellsIndex].IsLoaded = isLoaded;
		}

		public void SetShakerRackCellState(int stripsIndex, int cellsIndex, bool isLoaded)
		{
			this.ShakerRack.Strips[stripsIndex].Cells[cellsIndex].IsLoaded = isLoaded;
		}

		public void SetTipState(int tipRacksIndex, int tipsIndex, TipState tipState)
		{
			this.TipRacks[tipRacksIndex].Tips[tipsIndex].TipState = tipState;
		}

		public void SetReaderCellState(int stripsIndex, int cellsIndex, bool isLoaded)
		{
			this.Reader.Strips[stripsIndex].Cells[cellsIndex].IsLoaded = isLoaded;
		}

		public void SetReaderEnzymeBottlesValue(int enzymeIndex, int value)
		{
			this.Reader.EnzymeBottles[enzymeIndex].Volume += value;
		}



		public void SetSampleRackState(int sampleRackIndex, RDSCL.SampleRackState sampleRackState)
		{
			this.SampleViewModel.FourSampleRackDescriptions[sampleRackIndex].SampleRackState = sampleRackState;
			//(RDSCL.SampleRackState)Enum.Parse(typeof(RDSCL.SampleRackState), stateDescription);
		}

		public void SetReagentBoxVolume(int reagentBoxIndex, int volume)
		{
			this.ReagentRack.ReagentBoxs[reagentBoxIndex].Volume = volume;
		}


		public void SetMBBottleVolume(int mBBottleIndex, int volume)
		{
			this.ReagentRack.MBBottles[mBBottleIndex].Volume = volume;
		}

		public void InitializeRemainingTimer()
		{


			this.remainingTimer = new System.Timers.Timer(1000);

			this.remainingTimer.Elapsed += Timer_Elapsed;

			this.remainingTimer.AutoReset = true;
		}

		public void StartRemainingTimer()
		{
			if (remainingTime.ToString(Properties.Resources.RemainingTimeFormat) == Convert.ToDateTime(Properties.Resources.TimeOut).ToString(Properties.Resources.RemainingTimeFormat)) this.remainingTime = new DateTime(yearUnit, monthUnit, dateUnit, hourUnit, minuteUnit, secondUnit);
			this.remainingTimer.Enabled = true;
			this.remainingTimer.Start();
		}

		public void StopRemainingTimer()
		{
			this.remainingTimer.Stop();
			this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.TaskStop, null));
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (remainingTime.ToString(Properties.Resources.RemainingTimeFormat) != Convert.ToDateTime(Properties.Resources.TimeOut).ToString(Properties.Resources.RemainingTimeFormat))
			{
				remainingTime = remainingTime.AddSeconds(-1);
				this.RaisePropertyChanged(nameof(RemainingTime));
			}
			else
			{
				this.IsStartTask = false;
			}
		}
	}
}