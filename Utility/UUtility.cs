using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone
{
    public static class UUtility
    {
        public static string ToBase64String(string text)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecodeBase64String(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static byte[] AppendByteArray(this byte[] byteArray1, byte[] byteArray2)
        {
            return byteArray1.Concat(byteArray2).ToArray();
        }

        public static string GetMD5(string hashContent)
            => GetMD5(Encoding.ASCII.GetBytes(hashContent));

        public static string GetMD5(byte[] hashContent)
        {
            // https://stackoverflow.com/a/24031467/16627173
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(hashContent);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("X2"));

                return sb.ToString();
            }
        }
    }
}
