using OrderService.Api.Client;
using OrderService.Extensions;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace OrderService.Converters.Item
{
    public class MeasureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().Equals(typeof(Measure)))
            {
                var measure = (Measure)value;
                switch (measure)
                {
                    case Measure.Unit:
                        return Models.Item.FilterItemType.Unit.Description();

                    case Measure.Meters:
                        return Models.Item.FilterItemType.Meters.Description();

                    case Measure.Centimeters:
                        return Models.Item.FilterItemType.Centimeters.Description();

                    case Measure.Box:
                        return Models.Item.FilterItemType.Box.Description();
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}