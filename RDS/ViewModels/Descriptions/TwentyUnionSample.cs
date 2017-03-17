using RDS.ViewModels.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;
using System;

namespace RDS.ViewModels.Descriptions
{
	public class TwentyUnionSample:ViewModel
	{
		private Visibility visibility;
		public Visibility Visibility
		{
			get { return visibility; }
			set
			{
				visibility = value;
				this.RaisePropertyChanged(nameof(Visibility));
			}
		}

		public ObservableCollection<Sample> Samples { get; set; } = new ObservableCollection<Sample>();

		public ICollection<SampleState> SamplesState { get { return this.Samples.Select(o=> o.SampleState).ToList(); } }
		
		public TwentyUnionSample(int columnIndex)
		{
			this.InitializeSampleHoles(columnIndex);
		}
		
		private void InitializeSampleHoles(int columnIndex)
		{
			columnIndex *= 20;
			
			for (int i = 1; i <= 20; i++)
			{
				var sample = new Sample(this.GetHoleNameByNumber(columnIndex + i));
				sample.NotifyRaiseProperty = new Action(() => { this.RaisePropertyChanged(nameof(SamplesState)); });
				this.Samples.Add(sample);
			}
		}

		private string GetHoleNameByNumber(int number)
		{
			var result = string.Empty;
			var quotient = number / 20;
			var remainder = number % 20;

			if (remainder == 0)
			{
				remainder = 20;
				quotient -= 1;
			}
			switch (quotient)
			{
				case 0: { result = $"A{remainder}"; break; }
				case 1: { result = $"B{remainder}"; break; }
				case 2: { result = $"C{remainder}"; break; }
				case 3: { result = $"D{remainder}"; break; }
				default: break;
			}
			return result;
		}
	}
}
