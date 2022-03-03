using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zone;

namespace Zone.FileInterface.Helper
{
    internal class ZoneMetadataReader
    {
        private const int DefaultBufferSize = 4096;
        private const int testBufferSize = 20;

        Encoding encoding = Encoding.UTF8;

        public long metadataIndex = -1;
        public long metadataContentIndex = -1;
        public long metadataTotalLength = -1;
        public long metadataLength = -1;

        private string rawData = string.Empty;

        /// <summary>
        /// If Metadata Header is found, index is saved in metadataIndex.
        /// </summary>
        /// <returns>Returns true if Metadata Header is found</returns>
        public bool LocateMetadata(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                return LocateMetadata(fs);
        }

        /// <summary>
        /// If Metadata Header is found, index is saved in metadataIndex.
        /// </summary>
        /// <returns>Returns true if Metadata Header is found</returns>
        public bool LocateMetadata(FileStream fileStream)
        {
            long streamLength = fileStream.Length;
            int bufferSize = DefaultBufferSize;

            long pos = streamLength;

            byte[] headerBytes = encoding.GetBytes(ZoneMetadataHelper.header);

            int bufferOffset = headerBytes.Length * 3;
            int bufferIntervel = bufferSize - bufferOffset;
            if (bufferIntervel < 0)
                bufferIntervel = 0;

            if (streamLength < bufferSize)
                bufferSize = (int)streamLength;

            byte[] fileBuffer = new byte[bufferSize];

            pos -= bufferSize;
            fileStream.Position = pos;


            //Looking for Metadata Header Index
            long patternIndex = -1;
            while (pos >= 0)
            {
                fileStream.Read(fileBuffer, 0, bufferSize);

                patternIndex = fileBuffer.LastIndexOfPattern(headerBytes);

                if (patternIndex != -1)
                {
                    patternIndex += pos;
                    break;
                }

                if (pos == 0)
                    break;

                pos -= bufferIntervel;
                if (pos < 0)
                    pos = 0;
                fileStream.Position = pos;
            }

            if (patternIndex == -1)
                return false;

            //Finding Footer if the header is found
            metadataIndex = patternIndex;

            pos = metadataIndex;
            fileStream.Position = pos;

            if ((pos + bufferSize) > streamLength)
                bufferSize = (int)(streamLength - pos);

            byte[] dataBuffer = new byte[bufferSize];

            fileStream.Read(dataBuffer, 0, bufferSize);

            string tempData = encoding.GetString(dataBuffer);
            int footerIndex = tempData.IndexOf(ZoneMetadataHelper.footer);

            if (footerIndex == -1)
                return false;

            string data = tempData.Substring(0, footerIndex);
            rawData = tempData = data.Remove(0, ZoneMetadataHelper.header.Length);

            metadataContentIndex = metadataIndex + ZoneMetadataHelper.header.Length;
            metadataTotalLength = footerIndex + ZoneMetadataHelper.footer.Length;
            metadataLength = tempData.Length;

            return true;
        }

        public bool TryExtractMetadata(out ZoneMetadata metadata)
        {
            if (rawData != string.Empty)
                try
                {
                    string data = string.Empty;
                    data = UUtility.DecodeBase64String(rawData);
                    metadata = JsonConvert.DeserializeObject<ZoneMetadata>(data);

                    return true;
                }
                catch { }

            metadata = null;
            return false;
        }
    }
}
