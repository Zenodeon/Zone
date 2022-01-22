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
        string media4 = "Apex Legends 2021.01.27 - 22.25.30.02.DVR.mp4";

        public void ApplyMetadata()
        {
            apply(uriPath + media4);
        }

        public void RemoveMetadata()
        {
            remove(uriPath + media4);
        }

        public async void apply(string filePath)
        {
            byte[] fileData = await File.ReadAllBytesAsync(filePath);

            ZoneMetadata zoneMetadata = new ZoneMetadata();
            zoneMetadata.dummyFill();

            byte[] embedData = ZoneMetadataHelper.GetEmbedData(zoneMetadata);

            fileData = fileData.AppendByteArray(embedData);

            await File.WriteAllBytesAsync(filePath, fileData);
        }

        public async void remove(string filePath)
        {
            string fileText = await File.ReadAllTextAsync(filePath, Encoding.UTF8);

            byte[] fileData = await File.ReadAllBytesAsync(filePath);

            ZoneMetadataHelper.RemoveEmbeddedData(fileText, ref fileData);

            await File.WriteAllBytesAsync(filePath, fileData);
        }
    }
}
