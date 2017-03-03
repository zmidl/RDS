using System.Collections.Generic;
using System.Collections.ObjectModel;
using RDS.Models;
using System.Linq;
using RDS.ViewModels.Common;

namespace RDS.ViewModels
{
    public class SampleViewModel : ViewModel
    {
        private ObservableCollection<SampleInformatin> sampleInformations;
        public ObservableCollection<SampleInformatin> SampleInformations
        {
            get { return sampleInformations; }
            set { sampleInformations = value; }
        }

		private ObservableCollection<Sampling> samplingStates = new ObservableCollection<Sampling>();
		public ObservableCollection<Sampling> SamplingStates
		{
			get { return samplingStates; }
			set { samplingStates = value; this.RaisePropertyChanged(nameof(SamplingStates)); }
		}

		private ObservableCollection<bool> sampleSelectionStates=new ObservableCollection<bool>();
		public ObservableCollection<bool> SampleSelectionStates
		{
			get { return sampleSelectionStates; }
			set { sampleSelectionStates = value; }
		}

        public SampleViewModel()
        {
            this.TestData();
         
            for (int i = 0; i < 80; i++)
            {
				this.sampleSelectionStates.Add(false);
				this.samplingStates.Add(Sampling.NoSample);
            }
        }

		public void SetAllSampleEmergency()
		{
			for (int i = 0; i < 80; i++)
			{
				this.SamplingStates[i] = Sampling.EmergencySampling;
			}
		}

        private void SetColumnHole(int startHole, int endHole)
        {
            var hasSelected = false;

            var resultRange = this.SampleSelectionStates?.Skip(startHole).Take(endHole-startHole+1).ToList();

            var selectedCount = resultRange.Where(o => o == true).Count();

            if (selectedCount > 0) hasSelected = true;

            for (int i = startHole; i <= endHole; i++) this.SampleSelectionStates[i] = !hasSelected;

        }

        public void SetAHole()
        {
            this.SetColumnHole(0, 19);
        }

        public void SetBHole()
        {
            this.SetColumnHole(20, 39);
        }

        public void SetCHole()
        {
            this.SetColumnHole(40, 59);
        }

        public void SetDHole()
        {
            this.SetColumnHole(60, 79);
        }

        private void TestData()
        {
            this.SampleInformations = new ObservableCollection<SampleInformatin>();
            SampleInformatin si = new SampleInformatin
            {
                Age = "20",
                DateTime = System.DateTime.Now.ToString(),
                HoleSite = "A1",
                Name = "王二妞",
                Reagent = "CC",
                Barcode = "12345671",
                SampleId = "AE123",
                Sex = "女",
                Type = "不明"
            };
            this.SampleInformations.Add(si);
            si = new SampleInformatin
            {
                Age = "20",
                DateTime = System.DateTime.Now.ToString(),
                HoleSite = "A2",
                Name = "赵四姐",
                Reagent = "BB",
                Barcode = "12345672",
                SampleId = "AE1234",
                Sex = "女",
                Type = "血样"
            };
            this.SampleInformations.Add(si);
            si = new SampleInformatin
            {
                Age = "30",
                DateTime = System.DateTime.Now.ToString(),
                HoleSite = "A3",
                Name = "张惠妹",
                Reagent = "BA",
                Barcode = "12345673",
                SampleId = "AE1235",
                Sex = "女",
                Type = "血样"
            };
            this.SampleInformations.Add(si);
            si = new SampleInformatin
            {
                Age = "32",
                DateTime = System.DateTime.Now.ToString(),
                HoleSite = "A4",
                Name = "裘千仞",
                Reagent = "BT",
                Barcode = "12345674",
                SampleId = "AE1236",
                Sex = "男",
                Type = "尿样"
            };
            this.SampleInformations.Add(si);
            si = new SampleInformatin
            {
                Age = "33",
                DateTime = System.DateTime.Now.ToString(),
                HoleSite = "A5",
                Name = "刘叶",
                Reagent = "BT",
                Barcode = "12345675",
                SampleId = "AE1237",
                Sex = "男",
                Type = "尿样"
            };
            this.SampleInformations.Add(si);
        }
    }
}
