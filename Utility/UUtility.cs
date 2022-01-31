using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone
{
    internal static class UUtility
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
    }
}
