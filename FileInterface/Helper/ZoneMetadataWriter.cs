using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quazistax;

namespace Zone.FileInterface.Helper
{
    internal class ZoneMetadataWriter
    {
        public static void EmbedMetadata(FileStream fs, ZoneMetadata metadata, bool replaceOldMetadata = true)
        {
            ZoneMetadataReader reader = new ZoneMetadataReader();

            bool replaceMetadata = false;
            if (replaceOldMetadata)
                replaceMetadata = reader.LocateMetadata(fs);

            if (replaceMetadata)
            {
                byte[] embedData = ZoneMetadataHelper.CreateEmbedData(metadata, incluedHeader: false);
                QSFile.ReplaceFilePart(fs, reader.metadataContentIndex, reader.metadataLength, embedData);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string embedData = ZoneMetadataHelper.CreateEmbedDataString(metadata);

                    fs.Seek(0, SeekOrigin.End);
                    sw.WriteLine(Environment.NewLine + embedData);
                }
            }
        }

        public static void RemoveEmbedData(FileStream fs)
        {
            ZoneMetadataReader reader = new ZoneMetadataReader();
            if (reader.LocateMetadata(fs))
                QSFile.DeleteFilePart(fs, reader.metadataContentIndex, reader.metadataLength);
        }
    }
}
