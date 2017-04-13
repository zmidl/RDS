﻿using System;
using System.Windows;
using System.Windows.Controls;
using RDS.ViewModels.Common;
using RDS.ViewModels;

namespace RDS.Views.Monitor
{
	/// <summary>
	/// SampleView.xaml 的交互逻辑
	/// </summary>
	public partial class SampleView : UserControl, IExitView
	{
		Action IExitView.ExitView { get; set; }

		public SampleViewModel ViewModel { get { return this.DataContext as SampleViewModel; } }

		public SampleView()
		{
			InitializeComponent();
		}

		private void Button_Exit_Click(object sender, RoutedEventArgs e)
		{
			((IExitView)this).ExitView();
		}

		//private void Button_Import_Click(object sender, RoutedEventArgs e)
		//{
			
		//	System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
		//	ofd.Filter = "Xm;文件(*.xml;)|*.xml;|所有文件|*.*";
		//	ofd.ValidateNames = true;
		//	ofd.CheckPathExists = true;
		//	ofd.CheckFileExists = true;
		//	if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
		//	{
		//		this.ViewModel.Test.Execute(null);
		//	}
		//	var table = XmlOperation.ReadXmlFile(ofd.FileName).Tables[2];
		//	var path = System.IO.Directory.GetCurrentDirectory();
		//	//this.ViewModel.DatatableToEntity(XmlOperation.ReadXmlFile(ofd.FileName).Tables[2]);
		//}

		//private void Button_On_Click(object sender, RoutedEventArgs e)
		//{
		//	Tip tipDescription = new Tip(TipState.NoExist);
		//	var a = this.ViewModel.EntityToXmlString(tipDescription);
		//	var c = this.ViewModel.XmlStringToEntity(a);
		//	var t1 = c.GetType();
		//	var d = this.ViewModel.XmlStringToEntity2(tipDescription, a);
		//	var t2 = tipDescription.GetType();


		//	this.ViewModel.Test.Execute(null);
		//	var a = this.FindResource("Uid").ToString();
		//	MessageBox.Show(a);
		//	this.ViewModel.RaiseSampleViewChanged(new SampleViewChangedArgs(SampleViewChangedName.MultiSelectMouseDown, 1));
		//}
	}
}
