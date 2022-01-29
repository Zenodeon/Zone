using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zone.FileInterface.Helper
{
    public static class ZoneMetadataHelper
    {
        public const string header = "[ZMD|";
        public const string footer = "|ZMD]";

        public static byte[] GetEmbedData(ZoneMetadata metadata)
        {
            return Encoding.UTF8.GetBytes(GetEmbedDataString(metadata));
        }

        public static string GetEmbedDataString(ZoneMetadata metadata)
        {
            string json = JsonConvert.SerializeObject(metadata);

            string rawData = UUtility.ToBase64String(json);

            string embedData = $"{header}{rawData}{footer}";
            return embedData;
        }

        public static bool RemoveEmbeddedData(string decodedData, ref byte[] data)
        {
            int headerIndex = decodedData.LastIndexOf(header);

            if (headerIndex == -1)
                return false;

            int footerIndex = decodedData.IndexOf(footer);

            if(footerIndex == -1)
                return false;

            int footerEndIndex = footerIndex + footer.Length;
            footerEndIndex -= headerIndex;

            decodedData = decodedData.Substring(headerIndex, footerEndIndex);

            byte[] bytesToRemove = Encoding.UTF8.GetBytes(decodedData);

            //int patternIndex = data.LastIndexOfPattern(bytesToRemove);
            int patternIndex = -1;

            DLog.Log("Data Length Before : " + data.Length);

            DLog.Log("Pattern Index : " + patternIndex);


            List<byte> dataByteList = new List<byte>(data);
            dataByteList.RemoveRange(patternIndex, bytesToRemove.Length);

            data = dataByteList.ToArray();

            DLog.Log("Data Length After : " + data.Length);

            return false;
        }

        public static bool dads(ref string data)
        {
            int headerIndex = data.LastIndexOf(header);

            if (headerIndex == -1)
                return true;

            data = data.Remove(0, headerIndex);

            int footerIndex = data.IndexOf(footer);

            data = data.Substring(0, footerIndex);

            DLog.Log(headerIndex + "");
            //DLog.Log(data);

            return false;
        }
    }
}
