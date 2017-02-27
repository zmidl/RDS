using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RDS.Models;

namespace RDS.ViewModels
{
   public class SampleViewModel:ViewModel
    {
        private ObservableCollection<SampleInformatin>  sampleInformations;
        public ObservableCollection<SampleInformatin> SampleInformations
        {
            get { return  sampleInformations; }
            set { sampleInformations = value; }
        }

        private ObservableCollection<bool> isTubeSelected;
        public ObservableCollection<bool> IsTubeSelected
        {
            get { return isTubeSelected; }
            set { isTubeSelected = value; }
        }

        private ObservableCollection<bool> _isTubeSelected;
        public ObservableCollection<bool> _IsTubeSelected
        {
            get { return _isTubeSelected; }
            set 
            { 
                _isTubeSelected = value; 
            }
        }

        public bool A1
        {
            get { return this._isTubeSelected[0]; }
            set { this._isTubeSelected[0] = value; this.RaisePropertyChanged("A1"); }
        }

        public bool A2
        {
            get { return this._isTubeSelected[1]; }
            set { this._isTubeSelected[1] = value; this.RaisePropertyChanged("A2"); }
        }

        public bool A3
        {
            get { return this._isTubeSelected[2]; }
            set { this._isTubeSelected[2] = value; this.RaisePropertyChanged("A3"); }
        }

        public bool A4
        {
            get { return this._isTubeSelected[3]; }
            set { this._isTubeSelected[3] = value; this.RaisePropertyChanged("A4"); }
        }

        public bool A5
        {
            get { return this._isTubeSelected[4]; }
            set { this._isTubeSelected[4] = value; this.RaisePropertyChanged("A5"); }
        }

        public bool A6
        {
            get { return this._isTubeSelected[5]; }
            set { this._isTubeSelected[5] = value; this.RaisePropertyChanged("A6"); }
        }

        public bool A7
        {
            get { return this._isTubeSelected[6]; }
            set { this._isTubeSelected[5] = value; this.RaisePropertyChanged("A7"); }
        }
        public bool A8
        {
            get { return this._isTubeSelected[7]; }
            set { this._isTubeSelected[5] = value; this.RaisePropertyChanged("A8"); }
        }
        public bool A9
        {
            get { return this._isTubeSelected[8]; }
            set { this._isTubeSelected[5] = value; this.RaisePropertyChanged("A9"); }
        }
        public bool A10
        {
            get { return this._isTubeSelected[9]; }
            set { this._isTubeSelected[5] = value; this.RaisePropertyChanged("A10"); }
        }
        public SampleViewModel()
        {
            this.TestData();
            this._IsTubeSelected = new ObservableCollection<bool>();
            for (int i = 0; i < 20; i++)
            {
                this._IsTubeSelected.Add(false);
            }
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
