using System.Collections.ObjectModel;

namespace RDS.ViewModels.Descriptions
{
	public class SixUnionMixtureModule
	{
		public ObservableCollection<SixUnionMixture> SixUnionMixtures { get; set; }

		public SixUnionMixtureModule()
		{
			this.SixUnionMixtures = new ObservableCollection<SixUnionMixture>();
			for (int i = 0; i < 7; i++)
			{
				this.SixUnionMixtures.Add(new SixUnionMixture());
			}
		}
	}
}
