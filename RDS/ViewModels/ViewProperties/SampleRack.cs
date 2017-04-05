using RDS.ViewModels.Common;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System;
using RDSCL;

namespace RDS.ViewModels.ViewProperties
{
	public class SampleRack:ViewModel
	{
		private SampleRackState PreviousState { get; set; }

		private SampleRackState sampleRackState;
		public SampleRackState SampleRackState
		{
			get { return sampleRackState; }
			set
			{
				if (value != SampleRackState.PrepareSample && sampleRackState!=SampleRackState.PrepareSample)
				{
					this.PreviousState = sampleRackState;
					this.RaisePropertyChanged(nameof(PreviousState));
				}
				sampleRackState = value;
				this.RaisePropertyChanged(nameof(SampleRackState));
			}
		}

		public ObservableCollection<SampleTube> Samples { get; set; } = new ObservableCollection<SampleTube>();

		public ICollection<SampleTubeState> SamplesState { get { return this.Samples.Select(o=> o.SampleState).ToList(); } }
		
		public SampleRack(int columnIndex)
		{
			this.InitializeSampleHoles(columnIndex);
			this.SampleRackState = SampleRackState.NotSample;
		}
		
		private void InitializeSampleHoles(int columnIndex)
		{
			columnIndex *= 20;
			
			for (int i = 1; i <= 20; i++)
			{
				var sample = new SampleTube(this.GetHoleNameByNumber(columnIndex + i));
				sample.NotifyRaiseProperty = new Action(() => { this.RaisePropertyChanged(nameof(SamplesState)); });
				this.Samples.Add(sample);
			}
		}

		private string GetHoleNameByNumber(int number)
		{
			var result = string.Empty;
			var quotient = number / 20;
			var remainder = number % 20;

			if (remainder == 0)
			{
				remainder = 20;
				quotient -= 1;
			}
			switch (quotient)
			{
				case 0: { result = $"A{remainder}"; break; }
				case 1: { result = $"B{remainder}"; break; }
				case 2: { result = $"C{remainder}"; break; }
				case 3: { result = $"D{remainder}"; break; }
				default: break;
			}
			return result;
		}

		public void RollbackState()
		{
			this.SampleRackState = this.PreviousState;
		}
	}
}
