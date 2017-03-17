using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using RDS.ViewModels.Common;
using RDS.ViewModels;
using RDSCL;
using RDS.ViewModels.Descriptions;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// SampleView.xaml 的交互逻辑
	/// </summary>
	public partial class SampleView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }

		public SampleViewModel ViewModel = new SampleViewModel();

		public SampleView()
		{
			InitializeComponent();
			this.DataContext = new SampleViewModel();

		}

		private void Button_Exit_Click(object sender, RoutedEventArgs e)
		{
			((IExitView)this).ExitView();
		}

		private void Button_Import_Click(object sender, RoutedEventArgs e)
		{
			
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
			ofd.Filter = "Xm;文件(*.xml;)|*.xml;|所有文件|*.*";
			ofd.ValidateNames = true;
			ofd.CheckPathExists = true;
			ofd.CheckFileExists = true;
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.ViewModel.Test.Execute(null);

			}

			this.ViewModel.DatatableToEntity(ViewModels.Common.XmlOperation.ReadXmlFile(ofd.FileName).Tables[2]);
		}

		private void Button_Layout_Click(object sender, RoutedEventArgs e)
		{
			RDS.Views.LayoutSetting LS = new LayoutSetting();
			LS.ShowDialog();
		}

		private void Button_On_Click(object sender, RoutedEventArgs e)
		{
			TipDescription tipDescription = new TipDescription() { IsLoaded = true, TipVolume = TipType._1000uL };
			var a = this.ViewModel.EntityToXmlString(tipDescription);

			var b=$"<TipDescription Type='RDS.ViewModels.Descriptions.TipDescription'><TipVolume>ThreeHundredMicroliter</TipVolume></TipDescription>";
			var c = this.ViewModel.XmlStringToEntity(b);
			var t1 = c.GetType();
			var d = this.ViewModel.XmlStringToEntity2(tipDescription, b);
			var t2 = tipDescription.GetType();
			
			//this.ViewModel.Test.Execute(null);
			//var a=this.FindResource("Uid").ToString();
			//MessageBox.Show(a);
			//this.ViewModel.RaiseSampleViewChanged(new SampleViewChangedArgs(SampleViewChangedName.MultiSelectMouseDown, 1));
		}


	}
}
