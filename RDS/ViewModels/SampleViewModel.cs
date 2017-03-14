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
		public ObservableCollection<SampleDescription> SampleDescriptions { get; set; } = new ObservableCollection<SampleDescription>();

		public ObservableCollection<SampleInformation> SampleInformations { get; set; } = new ObservableCollection<SampleInformation>();

		public void ResetSampleSelection()
		{
			for (int i = 0; i < this.SampleDescriptions.Count; i++)
			{
				if (this.SampleDescriptions[i].IsSelected == false) this.SampleDescriptions[i].SelectionState = false;
			}
		}

		public void SynchronizeSampleSelectionState()
		{
			for (int i = 0; i < this.SampleDescriptions.Count; i++)
			{
				this.SampleDescriptions[i].IsSelected = this.SampleDescriptions[i].SelectionState;
			}
		}

		public void MultipeSetSampleStateToEmergency()
		{
			for (int i = 40; i < 60; i++)
			{
				this.SampleDescriptions[i].State = Sampling.EmergencySampling;
			}
		}

		public SampleViewModel()
		{
			for (int i = 1; i <= 80; i++)
			{
				//var holeName = string.Empty;
				//if (i % 2 == 0) 
				var holeName = this.GetHoleNameByNumber(i);
				SampleDescriptions.Add(new SampleDescription(this.GetHoleNameByNumber(i),false, Sampling.NormalSampling/*, new SampleInformatin()*/));

				this.SampleInformations.Add(new SampleInformation()
				{
					Age = "25",
					Barcode = "1234567",
					Birthday = "20010321",
					DateTime = "20170101",
					HoleName = holeName,
					Name = "王秀娟",
					Reagent = "UU",
					SampleId = "123",
					Sex = "女",
					Type = "未知"
				});
			}
			this.SampleDescriptions[0].State = Sampling.NormalSampling;
			this.SampleDescriptions[1].State = Sampling.EmergencySampling;
			this.SampleDescriptions[2].State = Sampling.Sampled;

			this.SampleInformations[0].HoleName = string.Empty;
			this.SampleInformations[1].HoleName = string.Empty;
			this.SampleInformations[2].HoleName = string.Empty;
			this.SampleInformations[3].HoleName = string.Empty;
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
			return this.SampleDescriptions?.Skip(startIndex).Take(endIndex - startIndex + 1).ToList();
		}

		private List<SampleDescription> GetSampleDescriptionsByRowNumber(int rowIndex)
		{

			var resultRange = new List<SampleDescription>(4);
			for (int i = 0; i < 4; i++)
			{
				resultRange.Add(this.SampleDescriptions[rowIndex + (i * 20)]);
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
					HoleName = "0",
					Name = table.Rows[i]["strName"].ToString(),
					Reagent = table.Rows[i]["strItem"].ToString(),
					SampleId = table.Rows[i]["strSampleID"].ToString(),
					Sex = table.Rows[i]["strSex"].ToString(),
					Type = table.Rows[i]["strSampleType"].ToString()
				};
				this.SampleInformations.Add(sampleInformation);
			}
		}
	}
}
