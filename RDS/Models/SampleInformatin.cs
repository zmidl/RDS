namespace RDS.Models
{
	public class SampleInformation : ViewModels.Common.ViewModel
	{
		public SampleInformation() { }

		public string SampleId { get; set; }//strSampleID="20141225CEH001"

		public string Barcode { get; set; }//strBarcode="2009161629"

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				this.RaisePropertyChanged(nameof(Name));
			}
		}

		public string Age { get; set; }//strAge="34"

		public string Sex { get; set; }// strSex="男"

		public string Type { get; set; }//strSampleType="尿液"

		private string holeName;
		public string HoleName
		{
			get { return holeName; }
			set
			{
				holeName = value;
				this.RaisePropertyChanged(nameof(HoleName));
			}
		}

		public string Birthday { get; set; }// strBirthday="19801126"

		public string Reagent { get; set; }//strItem="UU"

		public string DateTime { get; set; }// strDateTime="20141225092145"

		private bool isEmergency = false;
		public bool IsEmergency
		{
			get { return isEmergency; }
			set
			{
				isEmergency = value;
				this.RaisePropertyChanged(nameof(IsEmergency));
			}
		}

	}
}
