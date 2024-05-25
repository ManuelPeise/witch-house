using System.Text;

namespace Logic.Shared
{
    public static class Helpers
    {
        public static string GetEncodedSecret(string secret, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(secret).ToList();
            bytes.AddRange(Encoding.UTF8.GetBytes(salt));

            return Convert.ToBase64String(bytes.ToArray());
        }
    }
}
