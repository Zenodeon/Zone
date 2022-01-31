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
        public static void EmbedMetadata(FileStream fs, ZoneMetadata metadata)
        {
            ZoneMetadataReader reader = new ZoneMetadataReader();

            if (reader.LocateMetadata(fs))
            {
                byte[] embedData = ZoneMetadataHelper.GetEmbedData(metadata, incluedHeader: false);
                QSFile.ReplaceFilePart(fs, reader.metadataContentIndex, reader.metadataLength, embedData);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string embedData = ZoneMetadataHelper.GetEmbedDataString(metadata);

                    fs.Seek(0, SeekOrigin.End);
                    sw.WriteLine(Environment.NewLine + embedData);
                }
            }
        }

        public void RemoveEmbedData(FileStream fs)
        {

        }
    }
}
