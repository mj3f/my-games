using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MyGames.Core.Utils;

public static class PasswordHasher
{
    private const int IterationCount = 100000;
    private const int SubkeyLength = 256 / 8; // 32
    private const int SaltSize = 128 / 8;
    
    public static (string, string) HashAndSaltPassword(string password)
    {
        // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
        // See: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0
        byte[] salt = new byte[SaltSize];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }
        
        // Produce a version 2 (see comment above) text hash.

        byte[] subkey =
            KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: SubkeyLength);

        var outputBytes = new byte[1 + SaltSize + SubkeyLength];
        outputBytes[0] = 0x00; // format marker
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, SubkeyLength);
        return (Convert.ToBase64String(outputBytes), Convert.ToBase64String(salt));
    }
    
    public static bool VerifyHashedPassword(string hashedPassword, string password, string salt)
    {
        byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
        
        byte[] saltBytes = Convert.FromBase64String(salt);

        byte[] expectedSubkey = new byte[SubkeyLength];
        Buffer.BlockCopy(decodedHashedPassword, 1 + saltBytes.Length, expectedSubkey, 0, expectedSubkey.Length);

        // Hash the incoming password and verify it
        byte[] actualSubkey = KeyDerivation.Pbkdf2(password, saltBytes, KeyDerivationPrf.HMACSHA256, IterationCount, SubkeyLength);

        return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
    }
}