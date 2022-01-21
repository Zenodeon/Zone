using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zone.FileInterface
{
    public static class ZoneMetadataHelper
    {
        private const string header = "[_ZMD-=<[";

        private const string footer1 = "]=[";
        private const string footer2 = "]>=-ZMD_]";

        public static byte[] GetEmbedData(ZoneMetadata metadata)
        {
            string json = JsonConvert.SerializeObject(metadata);

            string rawData = UUtility.ToBase64String(json);
            int dataLength = rawData.Length;

            string embedData = $"{header}{rawData}{footer1}{dataLength}{footer2}";
            return Encoding.UTF8.GetBytes(embedData);
        }

        public static bool RemoveEmbeddedData(ref string data)
        {
            DLog.Log(data.LastIndexOf(footer2) + "");

            return false;
        }
    }
}
