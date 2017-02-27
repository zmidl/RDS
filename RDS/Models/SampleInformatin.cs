using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Models
{
   public class SampleInformatin
    {
        public SampleInformatin() { }

        public string SampleId { get; set; }

        public string Barcode { get; set; }

        public string Name { get; set; }

        public string Age { get; set; }

        public string Sex { get; set; }

        public string Type { get; set; }

        public string HoleSite { get; set; }

        public string Reagent { get; set; }

        public string DateTime { get; set; }
    }
}
