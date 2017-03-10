namespace RDS.ViewModels.Common
{
	public enum Sampling
	{
		NoSample = 0,
		NormalSampling = 1,
		EmergencySampling = 2,
		Sampled = 3
	}

	public enum SixTubeStates
	{
		Inexistence = 0,
		Existence = 1,
		Leaving = 2
	}

	public enum MultipeSelection
	{
		ColumnA = 20,
		ColumnB = 21,
		ColumnC = 22,
		ColumnD = 23,
		Row1 = 0,
		Row2 = 1,
		Row3 = 2,
		Row4 = 3,
		Row5 = 4,
		Row6 = 5,
		Row7 = 6,
		Row8 = 7,
		Row9 = 8,
		Row10 = 9,
		Row11 = 10,
		Row12 = 11,
		Row13 = 12,
		Row14 = 13,
		Row15 = 14,
		Row16 = 15,
		Row17 = 16,
		Row18 = 17,
		Row19 = 18,
		Row20 = 19
	}

	public enum ShowView
	{
		ShowSampleView = 0,
		ShowReagentView = 1,
		ShowReportView = 2
	}
}
