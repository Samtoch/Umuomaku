using System.Security.Cryptography;
using System.Text;

namespace Umuomaku.Helpers
{
    public static class PasswordHasher
    {
        public static string ComputeStringToSha256Hash(string plainText)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
