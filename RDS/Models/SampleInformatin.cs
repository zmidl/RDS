using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Models
{
   public class SampleInformation
	{
		public SampleInformation() { }

        public string SampleId { get; set; }//strSampleID="20141225CEH001"

		public string Barcode { get; set; }//strBarcode="2009161629"

		public string Name { get; set; }//strName="郑志营"

		public string Age { get; set; }//strAge="34"

		public string Sex { get; set; }// strSex="男"

		public string Type { get; set; }//strSampleType="尿液"

		public string HoleSite { get; set; }

		public string Birthday { get; set; }// strBirthday="19801126"

		public string Reagent { get; set; }//strItem="UU"

		public string DateTime { get; set; }// strDateTime="20141225092145"
	}
}
