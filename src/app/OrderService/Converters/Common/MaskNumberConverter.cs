using OrderService.Extensions;
using System.Globalization;
using Xamarin.Forms;

namespace OrderService.Converters.Common
{
    public class MaskNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value != null ? (string)value : string.Empty;

            return str.OnlyNumbers().ToMask((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}