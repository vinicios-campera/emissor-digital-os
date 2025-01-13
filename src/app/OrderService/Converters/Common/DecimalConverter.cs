using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace OrderService.Converters.Common
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = decimal.Parse(value.ToString()).ToString("C");
            var currenctSymbol = new RegionInfo(culture.Name).CurrencySymbol;
            return result.Replace($"{currenctSymbol} ", "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                return 0m;

            if (valueLong <= 0)
                return 0m;

            return valueLong / 100m;
        }
    }
}