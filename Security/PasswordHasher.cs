using System.Security.Cryptography;

namespace RestfulApiVisualCode.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;
        private const char Delimiter = '$';
        private const string Prefix = "PBKDF2";

        public static string Hash(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize);

            return string.Join(
                Delimiter,
                Prefix,
                Iterations,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(key));
        }

        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
            {
                return false;
            }

            if (!TryParseHash(storedHash, out int iterations, out byte[] salt, out byte[] expectedKey))
            {
                return false;
            }

            byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedKey.Length);

            return CryptographicOperations.FixedTimeEquals(key, expectedKey);
        }

        public static bool IsHashFormat(string value)
        {
            return TryParseHash(value, out _, out _, out _);
        }

        private static bool TryParseHash(string storedHash, out int iterations, out byte[] salt, out byte[] expectedKey)
        {
            iterations = 0;
            salt = Array.Empty<byte>();
            expectedKey = Array.Empty<byte>();

            string[] parts = storedHash.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4 || !string.Equals(parts[0], Prefix, StringComparison.Ordinal))
            {
                return false;
            }

            try
            {
                if (!int.TryParse(parts[1], out iterations) || iterations <= 0)
                {
                    return false;
                }

                salt = Convert.FromBase64String(parts[2]);
                expectedKey = Convert.FromBase64String(parts[3]);
                return salt.Length > 0 && expectedKey.Length > 0;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
