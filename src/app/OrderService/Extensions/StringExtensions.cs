using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderService.Extensions
{
    public static class StringExtensions
    {
        public static string AbbreviateName(this string name, int length = 32)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            var nomeAbreviado = name;

            if (name.Length > length)
            {
                var narr = name.Split(' ');
                var novo = new List<string>();

                for (int i = 0; i < narr.Length; i++)
                {
                    if (i == 0 || i == narr.Length - 1)
                    {
                        novo.Add(narr[i]);
                    }
                    else
                    {
                        if (narr[i].Length > 3)
                        {
                            novo.Add(string.Format("{0}.", narr[i][0].ToString().ToUpper()));
                        }
                    }
                }
                nomeAbreviado = string.Join(" ", novo.ToArray());
            }

            if (nomeAbreviado.Length > length)
            {
                var narr = nomeAbreviado.Split(' ');
                var novo = new List<string>
                    {
                        narr.First(),
                        narr.Last()
                    };
                nomeAbreviado = string.Join(" ", novo.ToArray());
            }

            return nomeAbreviado.ToTitleCase();
        }

        public static string Between(this string str, string start, string end)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.Split(start)[1].Split(end)[0];
        }

        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.ToUpper().First() + string.Join("", str.Skip(1)).ToLower();
        }

        public static string FirstCharToUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            str = str.Trim();

            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str;

            return char.ToUpper(str[0]) + str[1..].ToLower();
        }

        public static string FormatEx(this string str, params object[] parameters)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return string.Format(str, parameters);
        }

        public static string GetFirstName(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split(' ').ToList();
            words.Remove("Da");
            words.Remove("Do");
            words.Remove("De");
            words.Remove("Dos");
            words.Remove("Das");
            if (words.Count() == 0)
                return input;
            else if (words.Count() > 3)
                return $"{words[0]} {words[1]}";
            else
                return $"{words[0]}";
        }

        public static bool IsLegalDocument(this string str)
            => new Regex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$").IsMatch(str);

        public static bool IsPhysicalDocument(this string str)
            => new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").IsMatch(str);

        public static string LoremIpsum(this string value, int words)
        {
            var wordsDict = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            wordsDict.Concat(value.Split(" "));

            var rand = new Random();
            int numWords = rand.Next(words - 1) + 1;

            var result = new StringBuilder();
            for (int w = 0; w < numWords; w++)
            {
                if (w > 0) { result.Append(" "); }
                result.Append(wordsDict[rand.Next(wordsDict.Length)]);
            }

            return $"Lorem {result}";
        }

        public static string OnlyLetters(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return new string(str.Where(char.IsLetter).ToArray());
        }

        public static string OnlyNumbers(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            StringBuilder sb = new();
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string OnlyNumbersWithoutLeftZeros(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.OnlyNumbers().ToInteger().ToString();
        }

        public static string RemoveAccents(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            StringBuilder sbReturn = new();
            var arrayText = str.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);

            return sbReturn.ToString();
        }

        public static string RemoveExtraSpaces(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"\s+", " ");
        }

        public static string RemoveLineBreaks(this string str) => str.Replace("\r", " ").Replace("\n", " ");

        public static string RemoveLineBreaksWithRegex(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"\t|\n|\r", "");
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            StringBuilder sb = new();
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' || c == '.' || c == '_' || c == ',')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string ReplaceFirst(this string str, string search, string replace)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            int pos = str.IndexOf(search);

            if (pos < 0)
                return str;

            string before = str[..pos];
            string after = str[(pos + search.Length)..];

            return before + replace + after;
        }

        public static string ReplaceIgnoreCase(this string str, string search, string replace)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, search, replace, RegexOptions.IgnoreCase);
        }

        public static string[] Split(this string str, string separator)
            => str.Split(new string[] { separator }, StringSplitOptions.None);

        public static IEnumerable<string> SplitOnChunk(this string str, int chunkSize)
        {
            if (str.Length < chunkSize)
                chunkSize = str.Length;

            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public static IEnumerable<string> SplitOnLength(this string input, int length)
        {
            int index = 0;
            while (index < input.Length)
            {
                if (index + length < input.Length)
                    yield return input.Substring(index, length);
                else
                    yield return input[index..];

                index += length;
            }
        }

        public static bool StringAsBool(this string value, string valueToCompare = "X")
            => value.Trim().ToLower().Equals(valueToCompare.Trim().ToLower());

        public static IEnumerable<int> StringCellphoneNumberAsSplitInt(this string str)
        {
            var list = new List<int>();

            if (str.Length != 11 && str.Length != 12 && !str.StartsWith("0"))
                list.AddRange(new List<int> { 0, 0, 0 });

            if (str.Length == 12)
            {
                list.Add(str[..3].ToInteger());
                list.Add(str.Substring(3, 5).ToInteger());
                list.Add(str.Substring(8, 4).ToInteger());
            }

            if (str.Length == 11)
            {
                list.Add(str[..3].ToInteger());
                list.Add(str.Substring(3, 4).ToInteger());
                list.Add(str.Substring(7, 4).ToInteger());
            }

            return list;
        }

        public static string ToBase64String(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static byte[] ToByteArray(this string str) => Convert.FromBase64String(str);

        public static char ToChar(this string value) => Convert.ToChar(value);

        public static string ToDateFromDateTime(this DateTime? date, string targetFormat = "dd/MM/yyyy", string stringResultIfInvalid = "")
        {
            if (!date.HasValue)
                return stringResultIfInvalid;

            return date.Value.ToString(targetFormat);
        }

        public static Guid ToGuid(this string str)
        {
            var result = Guid.TryParse(str, out Guid guid);
            if (!result)
                throw new Exception($"Value not possible converted {str} to Guid");
            return guid;
        }

        public static string ToGuidLinesString(this string str, string guid)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            string separator = string.Format("\r\n{0} ", guid);

            List<string> value = str.Split("\r\n").ToList();
            int i = value.FindIndex(w => !string.IsNullOrEmpty(w));
            value.RemoveRange(0, i);

            string result = string.Format("{0} {1}", guid, string.Join(separator, value));
            return result;
        }

        public static string ToMask(this string value, params string[] masks)
        {
            var maskedOutput = "";
            var maskedCounter = 0;
            var mask = masks.ToList().FirstOrDefault(x =>
                 (x.Split("#").Count() - 1) == value.Length)
                 ?? masks[0];

            for (int i = 0; i < mask.Length; i++)
            {
                if (maskedCounter >= value.Length)
                {
                    return maskedOutput;
                }

                var maskChar = mask.Substring(i, 1);

                if (maskChar == "#")
                {
                    maskedOutput += value.Substring(maskedCounter, 1);
                    maskedCounter++;
                }
                else
                {
                    maskedOutput += maskChar;
                }
            }

            return maskedOutput;
        }

        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            str = textInfo.ToTitleCase(str.ToLower());
            return str;
        }

        public static string ToUppercaseFirst(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return char.ToUpper(str[0]) + str[1..];
        }

        public static string? TrimDefault(this string str, string? def = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return string.IsNullOrEmpty(str) ? def : str.Trim();
        }

        public static string TruncateLongString(this string str, int maxLength)
        {
            var newStr = string.IsNullOrEmpty(str) ? str : str[..Math.Min(str.Length, maxLength)];

            if (str.Length > maxLength)
                return $"{newStr}...";

            return newStr;
        }
    }
}