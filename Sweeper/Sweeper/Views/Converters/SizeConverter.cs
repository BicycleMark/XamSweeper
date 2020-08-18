using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Sweeper.Views.Converters
{
    public class SizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length <3 || values[2] == null || values[1] == null || values[0] == null)
                return 0;
            int numItems = (int)values[2];
            int separatorSize = (int)values[1]; 
            double frameSize = System.Convert.ToDouble(values[0]) ;
            
            int totalSeparatorSize = (numItems - 1) * separatorSize;
            int remainingArea = System.Convert.ToInt32(frameSize) - totalSeparatorSize;
            return remainingArea / numItems;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
