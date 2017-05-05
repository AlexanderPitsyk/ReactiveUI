using System;
using System.Globalization;
using System.Windows.Data;
using ReactiveUIApplication.Common;

namespace ReactiveUIApplication.Models
{
    public class PriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return Extentions.GetEnumByIndex<PriorityOption>((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
