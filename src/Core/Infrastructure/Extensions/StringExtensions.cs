using System;
using System.Security.Cryptography;
using System.Text;

namespace ByteCarrot.Masslog.Core.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string Args(this string s, params object[] args)
        {
            return String.Format(s, args);
        }

        public static bool Contains(this string s, string value, StringComparison comparison)
        {
            return s.IndexOf(value, comparison) > -1;
        }

        public static bool Empty(this string s)
        {
            return s == null || s.Trim() == String.Empty;
        }

        public static bool NotEmpty(this string s)
        {
            return !s.Empty();
        }

        public static TValue? ToEnum<TValue>(this string s) where TValue : struct
        {
            if (s.Empty())
            {
                return null;
            }

            return (TValue?) Enum.Parse(typeof (TValue), s.Trim());
        }

        public static string Md5Hash(this string input)
        {
            if (input == null)
            {
                throw new NullReferenceException();
            }

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
