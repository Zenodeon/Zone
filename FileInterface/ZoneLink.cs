using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DebugLogger.Wpf;
using Quazistax;
using Zone.FileInterface.Helper;

namespace Zone.FileInterface
{
    internal class ZoneLink
    {
        private int defaultAllocationSize = 1024;

        string uriPath = @"D:\TestSite\TestSubjects\New folder (2)\";
        string media = "unknown-3.png";
        string media1 = "tenor.gif";
        string media2 = "fumo_Cirno_city.mp4";
        string media3 = "1614912655857.webm";
        string media4 = "Apex Legends 2021.01.27 - 22.25.30.02.DVR.mp4";

        string text = "testFile.txt";

        public void ApplyMetadata()
        {
            test(uriPath + text);
            //apply(uriPath + media4);
        }

        public void RemoveMetadata()
        {
            //remove(uriPath + media4);
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
        }

        public void test(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                //ZoneMetadataReader reader = new ZoneMetadataReader();

                //if(reader.LocateMetadata(fs))
                //{
                //    DLog.Log("Located Header");
                //    if (reader.TryExtractMetadata())
                //        DLog.Log("Extraction Success");
                //    else
                //        DLog.Log("Extraction Failed");
                //}

                ZoneMetadata zoneMetadata = new ZoneMetadata();
                zoneMetadata.dummyFill();

                ZoneMetadataWriter.EmbedMetadata(fs, zoneMetadata);
            }
        }
    }
}
