using System.Collections.Generic;
using System.Collections.ObjectModel;
using RDS.Models;
using System.Linq;
using RDS.ViewModels.Common;
using RDS.ViewModels.Descriptions;
using System.Data;

namespace RDS.ViewModels
{
	public class SampleViewModel : ViewModel
	{
		public ObservableCollection<SampleDescription> SampleDescritions { get; set; } = new ObservableCollection<SampleDescription>();

		public ObservableCollection<SampleInformation> SampleInformatins { get; set; } = new ObservableCollection<SampleInformation>();

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
				SampleDescritions.Add(new SampleDescription(false, Sampling.NoSample/*, new SampleInformatin()*/));

				this.SampleInformatins.Add(new SampleInformation()
				{
					Age = "25",
					Barcode = "1234567",
					Birthday = "20010321",
					DateTime = "20170101",
					HoleSite = "",
					Name = "王秀娟",
					Reagent = "UU",
					SampleId = "123",
					Sex = "女",
					Type = "未知"
				});
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

		public void DatatableToEntity(DataTable table)
		{
			//table.Columns[nameof(a)].ToString();

			//strAge="28岁" strDateTime="2016/5/5 9:33:35" strSampleType="阴道分泌物" strSex="女" strName="周晓燕" strSampleID="701" strItem="CT" strBarcode="1"/

			for (int i = 0; i < table.Rows.Count; i++)
			{
				var sampleInformation = new SampleInformation()
				{
					Age = table.Rows[i]["strAge"].ToString(),
					Barcode = table.Rows[i]["strBarcode"].ToString(),
					Birthday = table.Rows[i]["strBirthday"].ToString(),
					DateTime = table.Rows[i]["strDateTime"].ToString(),
					HoleSite = "0",
					Name = table.Rows[i]["strName"].ToString(),
					Reagent = table.Rows[i]["strItem"].ToString(),
					SampleId = table.Rows[i]["strSampleID"].ToString(),
					Sex = table.Rows[i]["strSex"].ToString(),
					Type = table.Rows[i]["strSampleType"].ToString()
				};
				this.SampleInformatins.Add(sampleInformation);
			}
		}
	}
}
