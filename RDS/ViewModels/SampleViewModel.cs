
using System.Collections.ObjectModel;
using RDS.Models;
using RDS.ViewModels.Common;
using RDS.ViewModels.ViewProperties;
using System.Data;
using System;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Text;

namespace RDS.ViewModels
{

	public class SampleViewModel : ViewModel
	{
		public SampleRackIndex CurrentSampleRackIndex { get; set; } = 0;

		private readonly string[] columnNames = new string[4] { Properties.Resources.A, Properties.Resources.B, Properties.Resources.C, Properties.Resources.D };

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
			if (this.CurrentSampleInformations != null && this.CurrentSampleInformations.Count > 0)
			{
				var targetValue = false;
				if (this.CurrentSampleInformations.Where(o => o.IsEmergency == true).Count() == 0) targetValue = true;
				for (int i = 0; i < 20; i++) this.CurrentSampleInformations[i].IsEmergency = targetValue;
			}
		}

		private string GetHoleNameByNumber(int number)
		{
			var result = new StringBuilder();
			var quotient = number / 20;
			var remainder = number % 20;
			if (remainder == 0) { remainder = 20; quotient -= 1; }
			result.Append(columnNames[quotient]);
			result.Append(remainder);
			return result.ToString();
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
						Age = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_Age].ToString(),
						Barcode = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_Barcode].ToString(),
						Birthday = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_Birthday].ToString(),
						DateTime = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_DateTime].ToString(),
						HoleName = this.GetHoleNameByNumber(i + 1 + (20 * (int)sampleColumn)),
						Name = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_Name].ToString(),
						Reagent = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_Item].ToString(),
						SampleId = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_SampleID].ToString(),
						Sex = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_Sex].ToString(),
						Type = this.lisInformationTable.Rows[i][Properties.Resources.LisInfo_SampleType].ToString(),
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

		private string FileName = string.Empty;

		public void GetLisTableFromFile()
		{
			try
			{
				string dateTime = string.Empty;

#if DEBUG
				dateTime = "20170504";
#else
				dateTime=DateTime.Now.ToString(Properties.Resources.LisFileNameFormat);
#endif
				var lisFilesPath = string.Format
				(
					ConfigurationManager.AppSettings[Properties.Resources.LisFilesPath].ToString(),
					/*Directory.GetCurrentDirectory()*/
					Environment.CurrentDirectory,
					dateTime
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
				General.PopupWindow(PopupType.ShowMessage,General.FindStringResource(Properties.Resources.PopupWindow_Message1),null);
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
