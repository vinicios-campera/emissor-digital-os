using System;
using System.Globalization;
using Xamarin.Forms;

namespace OrderService.Converters.Common
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((DateTime)value).ToString("dd/MM/yy");

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}