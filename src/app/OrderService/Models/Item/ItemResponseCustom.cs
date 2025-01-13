using Xamarin.Forms.ConvertersPack;

namespace OrderService.Models.Item
{
    public class ItemResponseCustom
    {
        public string? Description { get; set; }
        public Guid Id { get; set; }
        public MeasureType Measure { get; set; }
        public double UnitaryValue { get; set; }

        public override string ToString()
        {
            var converter = new CurrencyConverter();
            var result = converter.Convert(UnitaryValue, typeof(double), null, System.Globalization.CultureInfo.CurrentCulture);
            return $"{Description} - {result}";
        }
    }
}