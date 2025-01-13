using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OrderService.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool Compare(this DateTime date1, DateTime date2) => DateTime.Compare(date1, date2) < 0;

        public static bool CompareAmmountDays(this DateTime date1, DateTime date2, int ammountDays)
        {
            TimeSpan value = date2 - date1;
            var result = value.Days;

            if (result < 0)
                result *= -1;

            return result > ammountDays;
        }

        public static int GetWeekNumber(this DateTime date)
            => DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

        public static string TimeSpanAsStringFromHHMMSSMM(this TimeSpan timespan, string delimiter = "")
            => string.Format("{0:00}{1}{2:00}{3}{4:00}{5}{6:00}", timespan.Hours, delimiter, timespan.Minutes, delimiter, timespan.Seconds, delimiter, timespan.Milliseconds);

        public static int ToDateAndTimeInteger(this DateTime datetime, string targetFormat = "dd/MM/yy") => long.Parse(datetime.ToString(targetFormat)).ToString().ToInteger();

        public static string ToDateAndTimeString(this DateTime datetime)
            => string.Format("[{0:00}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}]", datetime.Day, datetime.Month, datetime.Year, datetime.Hour, datetime.Minute, datetime.Second);

        public static DateTime ToDateTime(this string date, string sourceFormat = "dd/MM/yy")
            => DateTime.ParseExact(date.PadLeft(sourceFormat.Length, '0'), sourceFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

        public static DateTime ToDateTime(this long date, string sourceFormat = "dd/MM/yy")
            => DateTime.ParseExact(date.ToString().PadLeft(sourceFormat.Length, '0'), sourceFormat, null, DateTimeStyles.None);

        public static DateTime ToDateTime(this int date, string sourceFormat = "dd/MM/yy")
            => DateTime.ParseExact(date.ToString().PadLeft(sourceFormat.Length, '0'), sourceFormat, null, DateTimeStyles.None);

        public static DateTime ToDateTime(this int? date, string sourceFormat = "dd/MM/yy") => ToDateTime(date!.Value, sourceFormat);

        public static DateTime ToDateTime(this decimal date, string sourceFormat = "dd/MM/yy")
            => DateTime.ParseExact(date.ToString().PadLeft(sourceFormat.Length, '0'), sourceFormat, null, DateTimeStyles.None);

        public static DateTime ToDateTime(this decimal? date, string sourceFormat = "dd/MM/yy") => ToDateTime(date!.Value, sourceFormat);

        public static DateTimeOffset ToDateTimeOffset(this string date, string sourceFormat = "dd/MM/yy")
            => DateTimeOffset.ParseExact(date.PadLeft(sourceFormat.Length, '0'), sourceFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

        public static DateTime? ToNullableDateTime(this int date, string sourceFormat = "dd/MM/yy")
        {
            if (date == 0) return null;
            return ToDateTime(date, sourceFormat);
        }

        public static DateTime? ToNullableDateTime(this int? date, string sourceFormat = "dd/MM/yy")
        {
            if (date == 0) return null;
            return ToDateTime(date, sourceFormat);
        }

        public static DateTime? ToNullableDateTime(this decimal date, string sourceFormat = "dd/MM/yy")
        {
            if (date == 0) return null;
            return ToDateTime(date, sourceFormat);
        }

        public static DateTime? ToNullableDateTime(this decimal? date, string sourceFormat = "dd/MM/yy")
        {
            if (date == null || date == 0) return null;
            return ToDateTime(date, sourceFormat);
        }

        public static DateTime? ToNullableDateTime(this string date, string sourceFormat = "dd/MM/yy")
        {
            if (string.IsNullOrEmpty(date)) return null;
            return ToDateTime(date, sourceFormat);
        }

        public static DateTimeOffset? ToNullableDateTimeOffset(this string date, string sourceFormat = "dd/MM/yy")
        {
            if (string.IsNullOrEmpty(date)) return null;
            return DateTimeOffset.ParseExact(date.PadLeft(sourceFormat.Length, '0'), sourceFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static string ToTimeDescription(this decimal value)
        {
            if (value < 0) throw new ArgumentException("Value should be >= 0");
            var hours = value.ToString(CultureInfo.GetCultureInfo("en-US")).Split('.')[0].ToInteger();
            var minutes = ((value - hours) * 60).ToInteger();
            var description = new StringBuilder();

            if (hours > 0)
                description.Append($"{hours}h");

            if (minutes > 0)
                description.Append($"{minutes}min");

            if (hours == 0 && minutes == 0)
                description.Append($"0h");

            return description.ToString();
        }

        public static TimeSpan ToTimeSpan(this decimal time, string sourceFormat = "hhmm")
                    => TimeSpan.ParseExact(time.ToString().PadLeft(sourceFormat.Length, '0'), sourceFormat, CultureInfo.InvariantCulture);

        public static TimeSpan ToTimeSpanFromHHMMSSDD(this uint value)
        {
            //https://stackoverflow.com/questions/12568408/how-to-convert-an-integer-time-to-hhmmss00-in-sql-server-2008
            var hour = (value / 1000000) % 100;
            var minute = (value / 10000) % 100;
            var second = (value / 100) % 100;
            var millisecond = (value % 100) * 10;

            var time =
                TimeSpan.FromHours(hour).Add(
                TimeSpan.FromMinutes(minute)).Add(
                TimeSpan.FromSeconds(second)).Add(
                TimeSpan.FromMilliseconds(millisecond));

            return time;
        }
    }
}