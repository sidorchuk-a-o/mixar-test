using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AD.ToolsCollection
{
    public static class StringExtensions
    {
        public static bool IsValid(this string value)
        {
            return value.IsNullOrEmpty() == false;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string ToUpperFirst(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var a = value.ToCharArray();

            a[0] = char.ToUpper(a[0]);

            return new(a);
        }

        /// <summary>
        /// Разделить слова текста в стиле [ Pascal | Camel ] Case пробелами
        /// </summary>
        /// <param name="value">Текст в формате [ Pascal | Camel ] Case</param>
        public static string SplitByUpper(this string value)
        {
            return !value.IsNullOrEmpty()
                ? string.Join(" ", Regex.Split(value, "(?=\\p{Lu}[a-z])")).TrimStart()
                : string.Empty;
        }

        /// <summary>
        /// Разделение текста на несколько строк
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="max">Максимум символов в одной строке</param>
        public static string Wrap(this string text, int max)
        {
            var charCount = 0;

            var lines = text
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(word => CharCount(word, max, ref charCount))
                .Select(group => string.Join(" ", group.ToArray()))
                .ToArray();

            return string.Join("\n", lines);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CharCount(string word, int max, ref int charCount)
        {
            var checkMax = (charCount % max) + word.Length + 1 >= max;

            var lenght = checkMax
                ? max - (charCount % max)
                : 0;

            charCount += lenght + word.Length + 1;

            return charCount / max;
        }

        public static string ToHexString(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return null;
            }

            var sb = new StringBuilder();
            var bytes = Encoding.UTF8.GetBytes(str);

            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string FromHexString(this string hexString)
        {
            if (hexString.IsNullOrEmpty())
            {
                return null;
            }

            var bytes = new byte[hexString.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}