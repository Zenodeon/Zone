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

        public static string uriPath = @"D:\TestSite\TestSubjects\New folder (2)\";
        public static string media = "unknown-3.png";
        public static string media1 = "tenor.gif";
        public static string media2 = "fumo_Cirno_city.mp4";
        public static string media3 = "1614912655857.webm";
        public static string media4 = "Apex Legends 2021.01.27 - 22.25.30.02.DVR.mp4";
        public static string media5 = "20210715_022756.jpg";

        public static ZoneMetadata Link(string filePath)
        {
            ZoneMetadata metadata = null;

            ZoneMetadataReader reader = new ZoneMetadataReader();
            if (reader.LocateMetadata(filePath))
                reader.TryExtractMetadata(out metadata);

            DatabaseHandler dbhandler = DatabaseHandler.activeDatabase;

            if (metadata == null)
            {
                metadata = ZoneMetadataHelper.GenerateMetadata(filePath, embedMetadata: true);
                dbhandler.AddMetadata(metadata);
            }
            else
            {
                //Checking for any Dupicate Files and given new date for unique fileID 
                if (!dbhandler.MetadataExists(metadata))
                {
                    if(dbhandler.MetadataExists(metadata))
                        metadata.UpdateAddedDate();
                    dbhandler.AddMetadata(metadata);
                }
            }

            return metadata;
        }
    }
}
