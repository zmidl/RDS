using System.Collections.ObjectModel;
using RDS.Models;
using RDS.ViewModels.Common;
using RDS.ViewModels.ViewProperties;
using System.Data;
using System;
using System.Configuration;
using System.IO;

namespace RDS.ViewModels
{
	public class SampleViewModel : ViewModel
	{
		public int CurrentColumnIndex { get; set; } = 0;

		public enum SampleColumn
		{
			ColumnA = 0,
			ColumnB = 1,
			ColumnC = 2,
			ColumnD = 3
		}

		private DataTable lisInformationTable;

		public ObservableCollection<SampleRack> SampleRacks { get; set; } = new ObservableCollection<SampleRack>();

		public ObservableCollection<SampleInformation> SampleInformations { get; set; } = new ObservableCollection<SampleInformation>();

		private ObservableCollection<SampleInformation>[] SampleInformationsColumns = new ObservableCollection<SampleInformation>[4];

		public void MultipeSetSampleStateToEmergency()
		{

		}

		public RelayCommand ExitSampleView { get; private set; }

		public SampleViewModel()
		{
			for (int i = 0; i < 4; i++)
			{
				this.SampleRacks.Add(new SampleRack(i));
			}

			this.GetLisTableFromFile();
			this.ExitSampleView = new RelayCommand(() => { this.OnViewChanged(null); this.RollBackSampleRacksState(true, this.CurrentColumnIndex); });
			//	(() =>
			//	{
			//		this.SampleInformations.Add(new SampleInformation()
			//		{
			//			Age = "25",
			//			Barcode = "1234567",
			//			Birthday = "20010321",
			//			DateTime = "20170101",
			//			HoleName = "",
			//			Name = "王秀娟",
			//			Reagent = "UU",
			//			SampleId = "123",
			//			Sex = "女",
			//			Type = "未知"
			//		});
			//	});
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

		public void DatatableToEntity(SampleColumn sampleColumn)
		{
			var sampleInformationsColumns = new ObservableCollection<SampleInformation>();
			if (this.lisInformationTable != null)
			{
				for (int i = 0; i < this.lisInformationTable.Rows.Count; i++)
				{
					var sampleInformation = new SampleInformation()
					{
						Age = this.lisInformationTable.Rows[i]["strAge"].ToString(),
						Barcode = this.lisInformationTable.Rows[i]["strBarcode"].ToString(),
						Birthday = this.lisInformationTable.Rows[i]["strBirthday"].ToString(),
						DateTime = this.lisInformationTable.Rows[i]["strDateTime"].ToString(),
						HoleName = this.GetHoleNameByNumber(i + 1+(20*(int)sampleColumn)),
						Name = this.lisInformationTable.Rows[i]["strName"].ToString(),
						Reagent = this.lisInformationTable.Rows[i]["strItem"].ToString(),
						SampleId = this.lisInformationTable.Rows[i]["strSampleID"].ToString(),
						Sex = this.lisInformationTable.Rows[i]["strSex"].ToString(),
						Type = this.lisInformationTable.Rows[i]["strSampleType"].ToString(),
						IsEmergency = i % 2 == 0 ? true : false
					};
					sampleInformationsColumns.Add(sampleInformation);
				}
				this.SampleInformations= this.SampleInformationsColumns[(int)sampleColumn] = sampleInformationsColumns;
				this.RaisePropertyChanged(nameof(this.SampleInformations));
			}
			else
			{
				
			}
		}

		public string EntityToXmlString(object entity, bool isFormat = false)
		{
			return Sias.Core.SXmlConverter.ToXMLString(entity, isFormat);
		}

		public object XmlStringToEntity(string xmlString)
		{
			return Sias.Core.SXmlConverter.CreateFromXMLString(xmlString);
		}

		public bool XmlStringToEntity2(object obj, string xmlString)
		{
			return Sias.Core.SXmlConverter.FromXMLString(obj, xmlString);
		}

		public void SetSampleRackState(SampleRackStateArgs args)
		{
			this.RollBackSampleRacksState(false,args.SampleRackIndex);
			this.SampleRacks[args.SampleRackIndex].SampleRackState = args.SampleRackState;
			this.SampleInformations = this.SampleInformationsColumns[args.SampleRackIndex];
			this.RaisePropertyChanged(nameof(this.SampleInformations));
		}

		private void RollBackSampleRacksState(bool isRollBackCurrentIndex,int currentIndex)
		{
			if (isRollBackCurrentIndex) this.SampleRacks[currentIndex].RollbackState();
			else
			{
				for (int i = 0; i < this.SampleRacks.Count; i++)
				{
					if (currentIndex != i) this.SampleRacks[i].RollbackState();
				}
			}
		}

		public void GetLisTableFromFile()
		{
			try
			{
				var lisFilesPath = string.Format
				(
					ConfigurationManager.AppSettings[Properties.Resources.LisFilesPath].ToString(),
					System.IO.Directory.GetCurrentDirectory(),
					DateTime.Now.ToString(Properties.Resources.LisFileNameFormat)
				);
				if (File.Exists(lisFilesPath))
				{
					this.lisInformationTable = XmlOperation.ReadXmlFile(lisFilesPath).Tables[2];
					if (this.lisInformationTable == null) { /*General.ShowAdministratorsView(); */return; }
				}
				else { /*General.ShowAdministratorsView();*/ return; }
			}
			catch (Exception ex)
			{
				//General.ShowAdministratorsView();
			}
		}
	}


	public class SampleRackStateArgs : EventArgs
	{
		public int SampleRackIndex { get; set; } = 0;
		public RDSCL.SampleRackState SampleRackState { get; set; } = RDSCL.SampleRackState.NotSample;

		public SampleRackStateArgs(int sampleRackIndex, RDSCL.SampleRackState sampleRackState)
		{
			this.SampleRackIndex = sampleRackIndex;
			this.SampleRackState = sampleRackState;
		}
	}
}
