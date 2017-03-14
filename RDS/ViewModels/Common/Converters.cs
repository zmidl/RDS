using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace RDS.ViewModels.Common
{
	class SamplingToSolidColorBrush : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush resultColor = default(SolidColorBrush);
			Sampling inputted = (Sampling)value;
			switch (inputted)
			{
				case Sampling.NoSample: { resultColor = new SolidColorBrush(Colors.WhiteSmoke); break; }
				case Sampling.NormalSampling: { resultColor = new SolidColorBrush(Colors.Yellow); break; }
				case Sampling.EmergencySampling: { resultColor = new SolidColorBrush(Colors.Green); break; }
				case Sampling.Sampled: { resultColor = new SolidColorBrush(Colors.Gray); break; }
				default: break;
			}
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


