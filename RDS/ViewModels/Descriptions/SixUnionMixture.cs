using RDS.ViewModels.Common;

namespace RDS.ViewModels.Descriptions
{
	public class SixUnionMixture
	{
		public MixtureState MixtureState { get; set; }

		public void SetMixtureState(MixtureState mixture)
		{
			this.MixtureState = mixture;
		}
	}
}
