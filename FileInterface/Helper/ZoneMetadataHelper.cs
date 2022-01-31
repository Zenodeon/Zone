using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zone.FileInterface.Helper
{
    internal static class ZoneMetadataHelper
    {
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
    }
}
