using System;
using System.Text;
using Utf8Json;

namespace Common.Helpers
{
    public static class HashHelper
    {
        private static readonly string _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>
        /// Getting MD5 Hash.
        /// </summary>
        public static string GetMD5Hash(params object[] input)
        {
            var serializedString = JsonSerializer.ToJsonString(new { input, _env });

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(serializedString);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
