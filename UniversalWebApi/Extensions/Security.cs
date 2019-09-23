using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace UniversalWebApi.Extensions
{
    public static class GeneratePassword
    {
        public static string GetRandomAlphanumericString(int length)
            => GetRandomString(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");

        private static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", nameof(length));
            if (length > int.MaxValue / 8)
                throw new ArgumentException("length is too big", nameof(length));
            if (characterSet == null)
                throw new ArgumentNullException(nameof(characterSet));
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", nameof(characterSet));

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint) characterArray.Length];
            }

            return new string(result);
        }
    }

    public enum PasswordStrength
    {
        Blank = 0,
        VeryWeak,
        Weak,
        Medium,
        Strong,
        VeryStrong
    }

    public static class PasswordCheck
    {
        public static PasswordStrength GetPasswordStrength(string password)
        {
            int score = 0;
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password.Trim())) return PasswordStrength.Blank;
            if (!HasMinimumLength(password, 5)) return PasswordStrength.VeryWeak;
            if (HasMinimumLength(password, 8)) score++;
            if (HasUpperCaseLetter(password) && HasLowerCaseLetter(password)) score++;
            if (HasDigit(password)) score++;
            if (HasSpecialChar(password)) score++;
            return (PasswordStrength) score;
        }

        public static bool IsStrongPassword(string password)
            => HasMinimumLength(password, 8)
               && HasUpperCaseLetter(password)
               && HasLowerCaseLetter(password)
               && (HasDigit(password) || HasSpecialChar(password));

        public static bool IsValidPassword(
            string password,
            int requiredLength,
            int requiredUniqueChars,
            bool requireNonAlphanumeric,
            bool requireLowercase,
            bool requireUppercase,
            bool requireDigit)
        {
            if (!HasMinimumLength(password, requiredLength)) return false;
            if (!HasMinimumUniqueChars(password, requiredUniqueChars)) return false;
            if (requireNonAlphanumeric && !HasSpecialChar(password)) return false;
            if (requireLowercase && !HasLowerCaseLetter(password)) return false;
            if (requireUppercase && !HasUpperCaseLetter(password)) return false;
            if (requireDigit && !HasDigit(password)) return false;
            return true;
        }

        #region Helper Methods

        private static bool HasMinimumLength(string password, int minLength) => password.Length >= minLength;

        private static bool HasMinimumUniqueChars(string password, int minUniqueChars) =>
            password.Distinct().Count() >= minUniqueChars;

        private static bool HasDigit(string password) => password.Any(char.IsDigit);

        private static bool HasSpecialChar(string password) =>
            password.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) != -1;

        private static bool HasUpperCaseLetter(string password) => password.Any(char.IsUpper);
        private static bool HasLowerCaseLetter(string password) => password.Any(char.IsLower);

        #endregion
    }
}