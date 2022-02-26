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
using Zone.Database;

namespace Zone.FileInterface
{
    internal static class ZoneLink
    {
        private static int defaultAllocationSize = 1024;

        static string uriPath = @"D:\TestSite\TestSubjects\New folder (2)\";
        static string media = "unknown-3.png";
        static string media1 = "tenor.gif";
        static string media2 = "fumo_Cirno_city.mp4";
        static string media3 = "1614912655857.webm";
        static string media4 = "Apex Legends 2021.01.27 - 22.25.30.02.DVR.mp4";

        public static ZoneMetadata Link(string filePath)
        {
            ZoneMetadataReader reader = new ZoneMetadataReader();
            if (reader.LocateMetadata(filePath))
            {
                if (reader.TryExtractMetadata(out ZoneMetadata extractedMetadata))
                    return extractedMetadata;
            }

            ZoneMetadata metadata = ZoneMetadataHelper.GenerateMetadata(filePath);
            DatabaseHandler.activeDatabase.AddMetadata(metadata);
            return metadata;
        }
    }
}
