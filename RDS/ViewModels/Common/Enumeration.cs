namespace RDS.ViewModels.Common
{
	public enum SampleTubeState
	{
		NoSampleTube = 0,
		Normal = 1,
		Emergency = 2,
		Sampling = 3,
		Sampled = 4
	}

	public enum HoleState
	{
		None = 0,
		Empty = 1,
		Full = 2
	}

	public enum SixUnionState
	{
		Inexistence = 0,
		Existence = 1,
		Leaving = 2
	}

	public enum TipType
	{
		_300uL = 300,
		_1000uL = 1000,
	}

	public enum TipState
	{
		NoExist = 0,
		Exist = 1
	}



	public enum ShowView
	{
		ShowSampleView = 0,
		ShowReagentView = 1,
		ShowReportView = 2
	}

}
