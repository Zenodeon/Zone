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
        public const int defaultSampleLength = 256;

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

            return incluedHeader ? $"{header}{rawData}{footer}" : rawData;
        }

        public static ZoneMetadata GenerateMetadata(string filePath)
        {
            string md5 = string.Empty;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                long streamLength = fs.Length;
                int totalSampleLength = streamLength >= defaultSampleLength ? defaultSampleLength : (int)streamLength;

                totalSampleLength /= 2;
                if (totalSampleLength % 2 != 0)
                    totalSampleLength -= 1;

                int sampleLength = totalSampleLength / 2;

                byte[] mainbuffer = new byte[totalSampleLength];
                byte[] endBuffer = new byte[sampleLength];

                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(mainbuffer, 0, sampleLength);

                fs.Seek(-sampleLength, SeekOrigin.End);
                fs.Read(endBuffer, 0, sampleLength);

                for (int i = 0; i < sampleLength; i++)
                    mainbuffer[sampleLength + i] = endBuffer[i];

                md5 = UUtility.GetMD5(mainbuffer);
            }
            return new ZoneMetadata(md5);
        }
    }
}
