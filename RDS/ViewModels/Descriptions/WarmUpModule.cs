using System.Collections.ObjectModel;

namespace RDS.ViewModels.Descriptions
{
	public class WarmUpModule
	{
		public ObservableCollection<SixUnionMixture> SixUnionMixtures { get; set; }
		public WarmUpModule()
		{
			for (int i = 0; i < 4; i++)
			{
				this.SixUnionMixtures.Add(new SixUnionMixture());
			}
		}
	}
}
