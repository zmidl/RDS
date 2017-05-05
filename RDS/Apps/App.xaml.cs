using System;
using System.Windows;
using System.Globalization;
using System.Runtime.InteropServices;

namespace RDS.Apps
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
    {

		System.Threading.Mutex mutex;
		protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
			//Application currApp = Application.Current;
			this.StartupUri = new Uri(RDS.Properties.Resources.StartupUri, UriKind.Relative);
			//this.LoadLanguage();

			bool isArrowMore;
			mutex = new System.Threading.Mutex(true, "ElectronicNeedleTherapySystem", out isArrowMore);

			if (isArrowMore == false) Environment.Exit(0);
			
		}

	
		private void LoadLanguage()
		{
			//CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
			//ResourceDictionary languageFile = default(ResourceDictionary);
			//try
			//{
			//	languageFile =
			//		Application.LoadComponent(
			//				 new Uri(@"/RDS;component/Apps/Languages/" + currentCultureInfo.Name + ".xaml", UriKind.Relative))
			//		as ResourceDictionary;
			//}
			//catch
			//{
			//}

			//if (languageFile != null)
			//{
			//	if (this.Resources.MergedDictionaries.Count > 0)
			//	{
			//		this.Resources.MergedDictionaries.Clear();
			//	}
			//	this.Resources.MergedDictionaries.Add(languageFile);
			//}
		}
	}
}
