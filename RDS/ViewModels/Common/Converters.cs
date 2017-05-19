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


