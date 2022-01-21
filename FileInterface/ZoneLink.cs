using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DebugLogger.Wpf;

namespace Zone.FileInterface
{
    public class ZoneLink
    {
        string uriPath = @"D:\TestSite\TestSubjects\New folder (2)\";
        string media = "unknown-3.png";
        string media1 = "tenor.gif";
        string media2 = "fumo_Cirno_city.mp4";
        string media3 = "1614912655857.webm";

        public void ApplyMetadata()
        {
            apply(uriPath + media);
        }

        public async void apply(string filePath)
        {
            string fileText = await File.ReadAllTextAsync(filePath, Encoding.UTF8);

            byte[] fileData = await File.ReadAllBytesAsync(filePath);

            ZoneMetadata zoneMetadata = new ZoneMetadata();
            zoneMetadata.dummyFill();

            byte[] embedData = ZoneMetadataHelper.GetEmbedData(zoneMetadata);

            //ZoneMetadataHelper.RemoveEmbeddedData(ref fileData);

            DLog.Log("Before : " + fileData.Length + " || " + embedData.Length);

            fileData = fileData.AppendByteArray(embedData);

            DLog.Log("After : " + fileData.Length);

            await File.WriteAllBytesAsync(filePath, fileData);
        }
    }
}
