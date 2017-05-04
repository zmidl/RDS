using System.Collections.ObjectModel;
using RDS.Models;
using RDS.ViewModels.Common;
using RDS.ViewModels.ViewProperties;
using System.Data;
using System;
using System.Linq;
using System.Configuration;
using System.IO;

namespace RDS.ViewModels
{
	public class SampleViewModel : ViewModel
	{
		public SampleRackIndex CurrentSampleRackIndex { get; set; } = 0;

		private DataTable lisInformationTable;

		public ObservableCollection<SampleRackDescription> FourSampleRackDescriptions { get; set; } = new ObservableCollection<SampleRackDescription>();

		public ObservableCollection<SampleInformation> CurrentSampleInformations { get; set; } = new ObservableCollection<SampleInformation>();

		private ObservableCollection<SampleInformation>[] FourSampleInformations = new ObservableCollection<SampleInformation>[4];

		public RelayCommand ExitSampleView { get; private set; }

		public RelayCommand SetMultipleEmergency { get; private set; }

		public SampleViewModel()
		{
			for (int i = 0; i < 4; i++)
			{
				this.FourSampleRackDescriptions.Add(new SampleRackDescription(i));
			}

			this.GetLisTableFromFile();
			this.SetMultipleEmergency = new RelayCommand(this.ExecuteSettingMultipleEmergency);
			this.ExitSampleView = new RelayCommand(() => { this.OnViewChanged(null); this.RollBackSampleRacksState(true, this.CurrentSampleRackIndex); });
		}

		private void ExecuteSettingMultipleEmergency()
		{
			var targetValue = false;
			if(this.CurrentSampleInformations.Where(o => o.IsEmergency == true).Count()==0) targetValue = true;
			for (int i = 0; i < 20; i++) this.CurrentSampleInformations[i].IsEmergency = targetValue;
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

		public void DatatableToEntity(SampleRackIndex sampleColumn)
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
						HoleName = this.GetHoleNameByNumber(i + 1 + (20 * (int)sampleColumn)),
						Name = this.lisInformationTable.Rows[i]["strName"].ToString(),
						Reagent = this.lisInformationTable.Rows[i]["strItem"].ToString(),
						SampleId = this.lisInformationTable.Rows[i]["strSampleID"].ToString(),
						Sex = this.lisInformationTable.Rows[i]["strSex"].ToString(),
						Type = this.lisInformationTable.Rows[i]["strSampleType"].ToString(),
						IsEmergency = i % 2 == 0 ? true : false
					};
					sampleInformationsColumns.Add(sampleInformation);
				}
				this.CurrentSampleInformations = sampleInformationsColumns;
				this.FourSampleInformations[(int)sampleColumn] = sampleInformationsColumns;
				this.RaisePropertyChanged(nameof(this.CurrentSampleInformations));
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
			var sampleRackIndex = (int)args.SampleRackIndex;
			this.RollBackSampleRacksState(false, args.SampleRackIndex);
			this.FourSampleRackDescriptions[sampleRackIndex].SampleRackState = args.SampleRackState;
			this.CurrentSampleInformations = this.FourSampleInformations[sampleRackIndex];
			this.RaisePropertyChanged(nameof(this.CurrentSampleInformations));
		}

		private void RollBackSampleRacksState(bool isRollBackCurrentIndex, SampleRackIndex sampleRack)
		{
			var currentIndex = (int)sampleRack;
			if (isRollBackCurrentIndex) this.FourSampleRackDescriptions[currentIndex].RollbackState();
			else
			{
				for (int i = 0; i < this.FourSampleRackDescriptions.Count; i++)
				{
					if (currentIndex != i) this.FourSampleRackDescriptions[i].RollbackState();
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
					/*Directory.GetCurrentDirectory()*/Environment.CurrentDirectory,
					DateTime.Now.ToString(Properties.Resources.LisFileNameFormat)
				);
				if (File.Exists(lisFilesPath))
				{
					this.lisInformationTable = XmlOperation.ReadXmlFile(lisFilesPath).Tables[2];
					if (this.lisInformationTable == null) { /*General.ShowAdministratorsView(); */return; }
				}
				else { /*General.ShowAdministratorsView();*/ return; }
			}
			catch
			{
				General.ShowMessage("没有Lis信息");
			}
		}
	}


	public class SampleRackStateArgs : EventArgs
	{
		public SampleRackIndex SampleRackIndex { get; set; } = 0;
		public RDSCL.SampleRackState SampleRackState { get; set; } = RDSCL.SampleRackState.NotSample;

		public SampleRackStateArgs(SampleRackIndex sampleRackIndex, RDSCL.SampleRackState sampleRackState)
		{
			this.SampleRackIndex = sampleRackIndex;
			this.SampleRackState = sampleRackState;
		}
	}
}
