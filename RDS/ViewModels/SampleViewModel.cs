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

		private int sampleTabIndex;
		public int SampleTabIndex
		{
			get { return sampleTabIndex; }
			set
			{
				if (value < 0) value = 0;
				else if (value > 1) value = 1;
				sampleTabIndex = value;
				this.RaisePropertyChanged(nameof(SampleTabIndex));
			}
		}

		public bool IsFirstTabIndex
		{
			get { return this.sampleTabIndex == 0 ? true : false; }
			set
			{
				if (this.sampleTabIndex == 1 && value) this.SampleTabIndex = 0;
				this.RaisePropertyChanged(nameof(IsFirstTabIndex));
				this.RaisePropertyChanged(nameof(IsSecondTabIndex));
			}
		}

		public bool IsSecondTabIndex
		{
			get { return this.sampleTabIndex == 1 ? true : false; }
			set
			{
				if (this.sampleTabIndex == 0 && value) this.SampleTabIndex = 1;
				this.RaisePropertyChanged(nameof(IsFirstTabIndex));
				this.RaisePropertyChanged(nameof(IsSecondTabIndex));
			}
		}

		public void ResetSampleSelection()
		{
			for (int i = 0; i < this.SampleDescritions.Count; i++)
			{
				if (this.SampleDescritions[i].IsSelected == false) this.SampleDescritions[i].SelectionState = false;
			}
		}

		public void SynchronizeSampleSelectionState()
		{
			for (int i = 0; i < this.SampleDescritions.Count; i++)
			{
				this.SampleDescritions[i].IsSelected = this.SampleDescritions[i].SelectionState;
			}
		}

		public void MultipeSetSampleStateToEmergency()
		{
			for (int i = 40; i < 60; i++)
			{
				this.SampleDescritions[i].State = Sampling.EmergencySampling;
			}
		}

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

		private void MultipleSetSampleSelectionState(List<SampleDescription> sampleDescriptions)
		{
			var hasSelected = false;

			var selectedCount = sampleDescriptions.Where(o => o.SelectionState == true).Count();

			if (selectedCount > 0) hasSelected = true;

			for (int i = 0; i < sampleDescriptions.Count; i++) sampleDescriptions[i].SelectionState = !hasSelected;

			this.SynchronizeSampleSelectionState();
		}

		private List<SampleDescription> GetSampleDescriptionsByRange(int startIndex, int endIndex)
		{
			return this.SampleDescritions?.Skip(startIndex).Take(endIndex - startIndex + 1).ToList();
		}

		private List<SampleDescription> GetSampleDescriptionsByRowNumber(int rowIndex)
		{
			
			var resultRange = new List<SampleDescription>(4);
			for (int i = 0; i < 4; i++)
			{
				resultRange.Add(this.SampleDescritions[rowIndex + (i * 20)]);
			}
			return resultRange;
		}

		public void MultipleSetSampleSelectionState(MultipeSelection multipeSelection)
		{
			switch (multipeSelection)
			{
				case MultipeSelection.ColumnA: { this.MultipleSetSampleSelectionState(this.GetSampleDescriptionsByRange(0, 19)); break; }
				case MultipeSelection.ColumnB: { this.MultipleSetSampleSelectionState(this.GetSampleDescriptionsByRange(20, 39)); break; }
				case MultipeSelection.ColumnC: { this.MultipleSetSampleSelectionState(this.GetSampleDescriptionsByRange(40, 59)); break; }
				case MultipeSelection.ColumnD: { this.MultipleSetSampleSelectionState(this.GetSampleDescriptionsByRange(60, 79)); break; }
				default: { this.MultipleSetSampleSelectionState(this.GetSampleDescriptionsByRowNumber((int)multipeSelection)); break; }
			}
		}
	}
}
