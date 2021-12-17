using SkyCommerce.Models;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.String;

namespace SkyCommerce.Extensions
{

    public static class StringExtensions
    {
        private static char sensitive = '*';
        private static char at = '@';

        public static GeoCoordinate ToGeoCoordinate(this string geoCoordinate)
        {
            var geops = geoCoordinate.Split("|");
            return new GeoCoordinate(double.Parse(geops[0], new CultureInfo("en")), double.Parse(geops[1], new CultureInfo("en")));
        }
        public static string UrlEncode(this string url)
        {
            return Uri.EscapeDataString(url);
        }
        public static bool NotEqual(this string original, string compareTo)
        {
            return !original.Equals(compareTo);
        }
        public static bool IsEmail(this string field)
        {
            // Return true if strIn is in valid e-mail format.
            return field.IsPresent() && Regex.IsMatch(field, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsMissing(this string value)
        {
            return IsNullOrEmpty(value);
        }

        public static bool IsPresent(this string value)
        {
            return !IsNullOrWhiteSpace(value);
        }

        private static string UrlCombine(string path1, string path2)
        {
            path1 = path1.TrimEnd('/') + "/";
            path2 = path2.TrimStart('/');

            return Path.Combine(path1, path2)
                .Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
        public static string UrlPathCombine(this string path1, params string[] path2)
        {
            path1 = path1.TrimEnd('/') + "/";
            foreach (var s in path2)
            {
                path1 = UrlCombine(path1, s).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }

            return path1;

        }

        public static string AddSpacesToSentence(this string state)
        {
            var text = state.ToCharArray();
            var chars = new char[text.Length + HowManyCapitalizedChars(text) - 1];

            chars[0] = text[0];
            int j = 1;
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    if (text[i - 1] != ' ' && !char.IsUpper(text[i - 1]) ||
                        (char.IsUpper(text[i - 1]) && i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    {
                        chars[j++] = ' ';
                        chars[j++] = text[i];
                        continue;
                    }
                }

                chars[j++] = text[i];
            }

            return new string(chars.AsSpan());
        }
        private static int HowManyCapitalizedChars(char[] state)
        {
            var count = 0;
            for (var i = 0; i < state.Length; i++)
            {
                if (char.IsUpper(state[i]))
                    count++;
            }

            return count;
        }

        public static bool HasTrailingSlash(this string url)
        {
            return url != null && url.EndsWith("/");
        }


        public static string TruncateSensitiveInformation(this string part)
        {
            return part.AsSpan().TruncateSensitiveInformation();
        }

        public static string TruncateSensitiveInformation(this ReadOnlySpan<char> part)
        {
            var firstAndLastLetterBuffer = new char[2];
            var firstAndLastLetter = new Span<char>(firstAndLastLetterBuffer);

            if (part != "")
            {
                part.Slice(0, 1).CopyTo(firstAndLastLetter.Slice(0, 1));
                part.Slice(part.Length - 1).CopyTo(firstAndLastLetter.Slice(1));

                return Create(part.Length, firstAndLastLetterBuffer, (span, s) =>
                {
                    s.AsSpan(0, 1).CopyTo(span);
                    for (var i = 1; i < span.Length - 1; i++)
                    {
                        span[i] = sensitive;
                    }
                    s.AsSpan(s.Length - 1).CopyTo(span.Slice(span.Length - 1));
                });
            }
            else
            {
                return "";
            }

        }

        public static string TruncateEmail(this string email)
        {
            var beforeAt = email.AsSpan(0, email.IndexOf(at)).TruncateSensitiveInformation().AsSpan();
            var afterAt = email.AsSpan(email.IndexOf(at) + 1).TruncateSensitiveInformation().AsSpan();

            var finalSpan = new Span<char>(new char[email.Length]);

            beforeAt.CopyTo(finalSpan);
            finalSpan[beforeAt.Length] = at;
            afterAt.CopyTo(finalSpan.Slice(beforeAt.Length + 1));

            return finalSpan.ToString();
        }

        public static string TruncateCreditCard(this string cardNumber)
        {
            var firstDigits = cardNumber.Substring(0, 6);
            var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);

            var requiredMask = new string('X', cardNumber.Length - firstDigits.Length - lastDigits.Length);

            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
            var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");

            return maskedCardNumberWithSpaces;
        }

        public static string ToSha256(this string value)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
