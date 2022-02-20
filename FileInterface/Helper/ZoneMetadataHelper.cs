using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Zone.FileInterface.Helper
{
    internal static class ZoneMetadataHelper
    {
        public const int defaultSampleLength = 128;

        public const string header = "[ZMD|";
        public const string footer = "|ZMD]";

        public static byte[] GetEmbedData(ZoneMetadata metadata, bool incluedHeader = true)
        {
            return Encoding.UTF8.GetBytes(GetEmbedDataString(metadata, incluedHeader));
        }

        public static string GetEmbedDataString(ZoneMetadata metadata, bool incluedHeader = true)
        {
            string json = JsonConvert.SerializeObject(metadata);
            string rawData = UUtility.ToBase64String(json);

            return incluedHeader? $"{header}{rawData}{footer}" : rawData;
        }

        public static ZoneMetadata GenerateMetadata(string filePath)
        {
            string md5 = string.Empty;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                long streamLength = fs.Length;
                int sampleLength = streamLength >= defaultSampleLength? defaultSampleLength : (int)streamLength;

                byte[] buffer = new byte[sampleLength];
                fs.Read(buffer, 0, sampleLength);

                md5 = UUtility.GetMD5(buffer);
            }
            return new ZoneMetadata(md5);
        }
    }
}
