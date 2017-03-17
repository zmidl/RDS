using System.Collections.Generic;
using System.Collections.ObjectModel;
using RDS.Models;
using System.Linq;
using RDS.ViewModels.Common;
using RDS.ViewModels.Descriptions;
using System.Data;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDS.ViewModels
{
	public class SampleViewModel : ViewModel
	{
		
		public ObservableCollection<TwentyUnionSample> TwentyUnionSampleHoles { get; set; } = new ObservableCollection<TwentyUnionSample>();

		public ObservableCollection<SampleInformation> SampleInformations { get; set; } = new ObservableCollection<SampleInformation>();

		public void MultipeSetSampleStateToEmergency()
		{
		
		}

		public RelayCommand Test { get; private set; }

		public SampleViewModel()
		{
			for (int i = 0; i < 4; i++)
			{
				this.TwentyUnionSampleHoles.Add(new TwentyUnionSample(i));
			}

			this.Test = new RelayCommand
				(() =>
				{
					this.SampleInformations.Add(new SampleInformation()
					{
						Age = "25",
						Barcode = "1234567",
						Birthday = "20010321",
						DateTime = "20170101",
						HoleName = "",
						Name = "王秀娟",
						Reagent = "UU",
						SampleId = "123",
						Sex = "女",
						Type = "未知"
					});
				});
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

		
		public void DatatableToEntity(DataTable table)
		{

			//	for (int i = 0; i < table.Rows.Count; i++)
			//	{
			//		var sampleInformation = new SampleInformation()
			//		{
			//			Age = table.Rows[i]["strAge"].ToString(),
			//			Barcode = table.Rows[i]["strBarcode"].ToString(),
			//			Birthday = table.Rows[i]["strBirthday"].ToString(),
			//			DateTime = table.Rows[i]["strDateTime"].ToString(),
			//			HoleName = "0",
			//			Name = table.Rows[i]["strName"].ToString(),
			//			Reagent = table.Rows[i]["strItem"].ToString(),
			//			SampleId = table.Rows[i]["strSampleID"].ToString(),
			//			Sex = table.Rows[i]["strSex"].ToString(),
			//			Type = table.Rows[i]["strSampleType"].ToString()
			//		};
			//		this.SampleInformations.Add(sampleInformation);
			//	}
			//}



			this.SampleInformations.Add(new SampleInformation()
			{
				Age = "25",
				Barcode = "1234567",
				Birthday = "20010321",
				DateTime = "20170101",
				HoleName = "A9",
				Name = "王秀娟",
				Reagent = "UU",
				SampleId = "123",
				Sex = "女",
				Type = "未知"
			});
		}


		public string EntityToXmlString(object entity, bool isFormat = false)
		{
			return Sias.Core.SXmlConverter.ToXMLString(entity, isFormat);
		}

		public object XmlStringToEntity(string xmlString)
		{
			return Sias.Core.SXmlConverter.CreateFromXMLString(xmlString);
		}

		public bool XmlStringToEntity2(object obj,string xmlString)
		{
			return Sias.Core.SXmlConverter.FromXMLString( obj, xmlString);
		}
	}

}
