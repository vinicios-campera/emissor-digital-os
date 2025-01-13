using OrderService.Extensions;
using System.Globalization;
using Xamarin.Forms;

namespace OrderService.Converters.Client
{
    public class DocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var document = (string)value;

            if (document.OnlyNumbers().Length <= 11)
                return document.OnlyNumbers().ToMask("###.###.###-##");
            else
                return document.OnlyNumbers().ToMask("##.###.###/####-##");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var document = (string)value;

            if (document.OnlyNumbers().Length <= 11)
                return document.OnlyNumbers().ToMask("###.###.###-##");
            else
                return document.OnlyNumbers().ToMask("##.###.###/####-##");
        }
    }
}