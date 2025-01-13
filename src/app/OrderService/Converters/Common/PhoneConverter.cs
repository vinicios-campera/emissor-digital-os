using OrderService.Extensions;
using System.Globalization;
using Xamarin.Forms;

namespace OrderService.Converters.Common
{
    public class PhoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cellphone = (string)value;
            return cellphone.OnlyNumbers().ToMask("(##)####-####", "(##)#####-####");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cellphone = (string)value;
            return cellphone.OnlyNumbers().ToMask("(##)####-####", "(##)#####-####");
        }
    }
}