using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Collections.ObjectModel;
using RDS.ViewModels.ViewProperties;

namespace RDS.ViewModels.Common
{
	class SampleStateToSolidColorBrush : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var inputted = new ObservableCollection<SampleTube>((ICollection<SampleTube>)value);
			ObservableCollection<SolidColorBrush> resultColor = new ObservableCollection<SolidColorBrush>();
			for (int i = 0; i < inputted.Count; i++)
			{
				SolidColorBrush color = default(SolidColorBrush);
				switch(inputted[i].SampleState)
				{
					case SampleTubeState.NoSampleTube: { color = new SolidColorBrush(Colors.WhiteSmoke); break; }
					case SampleTubeState.Normal: { color = new SolidColorBrush(Colors.Brown); break; }
					case SampleTubeState.Emergency: { color = new SolidColorBrush(Colors.Blue); break; }
					case SampleTubeState.Sampling: { color = new SolidColorBrush(Colors.Green); break; }
					case SampleTubeState.Sampled: { color = new SolidColorBrush(Colors.Gray); break; }
					default: break;
				}
				resultColor.Add(color);
			}
			//ObservableCollection<SampleState> inputted = (ObservableCollection<SampleState>)value;
			//for (int i = 0; i < inputted.Count; i++)
			//{
			//	var color = default(SolidColorBrush); 
			//	switch (inputted[i])
			//	{
		
			//	}
			//	resultColor.Add(color);
			//}
			//return resultColor;
			return resultColor;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}

	class SampleSelectionStateToSolidColorBrush : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var inputtedValue = (bool)value;
			var resutColor = default(SolidColorBrush);
			if (inputtedValue) resutColor = new SolidColorBrush(Colors.Black);
			else resutColor = new SolidColorBrush(Colors.White);
			return resutColor;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var inputtedValue = (SolidColorBrush)value;
			var result = true;
			if (inputtedValue.Color == Colors.White) result = false;
			return result;
		}
	}
}


