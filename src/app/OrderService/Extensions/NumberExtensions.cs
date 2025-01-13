using System.Globalization;

namespace OrderService.Extensions
{
    public static class NumberExtensions
    {
        public static decimal ToDecimal(this string number) => number.ToDecimal(-1);

        public static decimal ToDecimal(this string number, int decimalPlaces) => number.ToDecimal(decimalPlaces, "pt-BR");

        public static decimal ToDecimal(this string number, int decimalPlaces, string culture = "pt-BR")
        {
            if (string.IsNullOrEmpty(number)) return 0;

            var converted = decimal.Parse(number.RemoveSpecialCharacters(), CultureInfo.GetCultureInfo(culture));
            if (number.Contains("-"))
            {
                converted *= -1;
            }

            if (decimalPlaces >= 0)
            {
                converted = Math.Round(converted, decimalPlaces);
            }

            return converted;
        }

        public static decimal ToDecimal(this double number) => Convert.ToDecimal(number);

        public static double ToDouble(this string number)
        {
            if (string.IsNullOrEmpty(number)) return 0;
            var converted = double.Parse(number.RemoveSpecialCharacters(), CultureInfo.GetCultureInfo("pt-BR"));
            if (number.Contains("-"))
            {
                converted *= -1;
            }

            return converted;
        }

        public static int ToInteger(this decimal number) => Convert.ToInt32(number);

        public static int ToInteger(this string number)
        {
            if (string.IsNullOrEmpty(number)) return 0;
            var converted = int.Parse(number.RemoveSpecialCharacters());
            if (number.Contains("-"))
            {
                converted *= -1;
            }

            return converted;
        }

        public static int ToInteger(this double number) => Convert.ToInt32(number);

        public static long ToLong(this string number)
        {
            if (string.IsNullOrEmpty(number)) return 0;
            var converted = long.Parse(number.RemoveSpecialCharacters());
            if (number.Contains("-"))
            {
                converted *= -1;
            }

            return converted;
        }
    }
}