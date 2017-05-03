using System;
using System.Windows;
using System.Globalization;

namespace RDS.Apps
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
			//Application currApp = Application.Current;
			this.StartupUri = new Uri(RDS.Properties.Resources.StartupUri, UriKind.Relative);
            //this.LoadLanguage();
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
