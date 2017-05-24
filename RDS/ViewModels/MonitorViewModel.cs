using RDS.ViewModels.Common;
using System.Collections.ObjectModel;
using RDS.ViewModels.ViewProperties;
using System;
using System.Collections.Generic;

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

		private ObservableCollection<Strip>[] StripGroups = new ObservableCollection<Strip>[7];

		public SampleViewModel SampleViewModel { get; set; } = new SampleViewModel();

		public ObservableCollection<CupRack> CupRacks { get; set; } = new ObservableCollection<CupRack>();

		public ObservableCollection<TipRack> TipRacks { get; set; } = new ObservableCollection<TipRack>();

		public ReagentRack ReagentRack { get; set; } = new ReagentRack();

		public Heating Heating { get; set; } = new Heating();

		public ShakerRack ShakerRack { get; set; } = new ShakerRack();

		public Mag Mag { get; set; } = new Mag();

		public Reader Reader { get; set; } = new Reader();

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
					else General.PopupWindow(PopupType.ShowMessage, General.FindStringResource(Properties.Resources.PopupWindow_Message2), null);
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

			this.InitializeStripGroups();
		}

		private void InitializeStripGroups()
		{
			this.StripGroups[0] = this.CupRacks[0].Strips;
			this.StripGroups[1] = this.CupRacks[1].Strips;
			this.StripGroups[2] = this.CupRacks[2].Strips;
			this.StripGroups[3] = this.Heating.Strips;
			this.StripGroups[4] = this.ShakerRack.Strips;
			this.StripGroups[5] = this.Mag.Strips;
			this.StripGroups[6] = this.Reader.Strips;
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

		public void FinishTask()
		{
			this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.TaskStop, null));
		}

		public void SetSamplingResult(bool samplingResult)
		{
			this.SamplingResult = samplingResult;
			this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.NotifySamplingResult, samplingResult));
		}

		public void SetSampleState(int twentyUnionSampleIndex, int sampleIndex, bool isLoaded)
		{
			this.SampleViewModel.FourSampleRackDescriptions[twentyUnionSampleIndex].Samples[sampleIndex].IsLoaded = isLoaded;
		}

		public void SetCupRackMixtureState(int cupRacksIndex, int stripsIndex, int mixtureIndex, bool isLoaded)
		{
			this.CupRacks[cupRacksIndex].Strips[stripsIndex].Cells[mixtureIndex].IsLoaded = isLoaded;
		}

		public void SetCupRackStripState(int cupRacksIndex, int stripsIndex, bool isLoaded, int number = 0)
		{
			var strip = this.CupRacks[cupRacksIndex].Strips[stripsIndex];
			strip.IsLoaded = isLoaded;
			strip.Number = number;
		}

		public void SetTipState(int tipRacksIndex, int tipsIndex, bool isLoaded)
		{
			this.TipRacks[tipRacksIndex].Tips[tipsIndex].IsLoaded = isLoaded;
		}

		public void SetReaderEnzymeBottleVolume(int enzymeIndex, int volume)
		{
			this.Reader.EnzymeBottles[enzymeIndex].Volume += volume;
		}

		public void SetReaderTemperature(int temperature)
		{
			this.Reader.Temperature = temperature;
		}

		public void SetSampleRackState(int sampleRackIndex, RDSCL.SampleRackState sampleRackState)
		{
			this.SampleViewModel.FourSampleRackDescriptions[sampleRackIndex].SampleRackState = sampleRackState;
		}

		public void SetOlefinBoxVolume(int volume)
		{
			this.Heating.OlefinBox.Volume = volume;
		}

		public void SetHeatingTemperature(int temperature)
		{
			this.Heating.Temperature = temperature;
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

		public void CarryStrip(int from, int to)
		{
			var pointFrom = this.GetStripLocation(from);
			var pointTo = this.GetStripLocation(to);
			this.StripGroups[pointTo.Item1][pointTo.Item2] = this.StripGroups[pointFrom.Item1][pointFrom.Item2];
			this.StripGroups[pointFrom.Item1][pointFrom.Item2] = new Strip();
		}

		private Tuple<int, int> GetStripLocation(int index)
		{
			if (index < 1) index = 1;
			else if (index > 37) index = 37;
			int tenth = 0;
			int units = 0;
			if (index > 32) { tenth = 6; units = index % 33; }
			else if (index > 28) { tenth = 5; units = index % 29; }
			else if (index > 25) { tenth = 4; units = index % 26; }
			else if (index > 21) { tenth = 3; units = index % 22; }
			else if (index > 14) { tenth = 2; units = index % 15; }
			else if (index > 7) { tenth = 1; units = index % 8; }
			else { tenth = 0; units = index - 1; }
			return new Tuple<int, int>(tenth, units);
		}
	}
}