using System.Security.Cryptography;

namespace infoManagerAPI.Utils
{
    public class Cryptography
    {
        static HashAlgorithm hash = SHA256.Create();

        public static string EncryptPassword(string password)
        {
            var encodedValue = System.Text.Encoding.UTF8.GetBytes(password);
            var encryptedPassword = hash.ComputeHash(encodedValue);
            return Convert.ToBase64String(encryptedPassword);
        }
    }
}
