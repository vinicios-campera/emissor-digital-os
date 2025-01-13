using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace OrderService.Converters.Common
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Transparent;
            
            if (value != null)
                if ((bool)value)
                    color = (Color)Application.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"];
                else
                    color = (Color)Application.Current.Resources.MergedDictionaries.FirstOrDefault()["Red1"];

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}