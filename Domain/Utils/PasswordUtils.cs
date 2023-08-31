using System.Security.Cryptography;

namespace Domain.Utils;

public static class PasswordUtils
{
    private const int keySize = 64;
    private const int iterations = 350000;
    private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public static (string Hash, string Salt) HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[16];
        rng.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            iterations,
            hashAlgorithm);
        byte[] hash = pbkdf2.GetBytes(keySize);
        return (Convert.ToHexString(hash), Convert.ToHexString(salt));
    }

    public static bool VerifyPassword(string password, string hashedPassword, string salt)
    {
        byte[] saltBytes = Convert.FromHexString(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            iterations,
            hashAlgorithm);
        byte[] hashToCompare = pbkdf2.GetBytes(keySize);
        string computedHash = Convert.ToHexString(hashToCompare);
        return hashedPassword == computedHash;
    }
}
