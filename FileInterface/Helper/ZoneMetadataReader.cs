using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.FileInterface.Helper
{
    public class ZoneMetadataReader
    {
        private const int DefaultBufferSize = 4096;
        private const int testBufferSize = 20;

        public FileStream baseStream;

        public long metadataIndex = -1;

        Encoding encoding = Encoding.UTF8;

        public ZoneMetadata metadata;

        public ZoneMetadataReader(FileStream fileStream)
        {
            baseStream = fileStream;
        }

        /// <summary>
        /// If Metadata Header is found, index is saved in metadataIndex.
        /// </summary>
        /// <returns>Returns true if Metadata Header is found</returns>
        public bool LocateMetadata()
        {
            long streamLength = baseStream.Length;
            int bufferSize = DefaultBufferSize;

            long pos = streamLength;

            byte[] headerBytes = encoding.GetBytes(ZoneMetadataHelper.header);

            int bufferOffset = headerBytes.Length * 3;
            int bufferIntervel = bufferSize - bufferOffset;
            if(bufferIntervel < 0)
                bufferIntervel = 0;

            if (streamLength < bufferSize)
                bufferSize = (int)streamLength;

            byte[] buffer = new byte[bufferSize];

            pos -= bufferSize;
            baseStream.Position = pos;

            long patternIndex = -1;
            while (pos >= 0)
            {
                baseStream.Read(buffer, 0, bufferSize);

                patternIndex = buffer.LastIndexOfPattern(headerBytes);
                
                if (patternIndex != -1)
                {
                    patternIndex += pos;
                    break;
                }

                if (pos == 0)
                    break;

                pos -= bufferIntervel;
                if(pos < 0)
                    pos = 0;
                baseStream.Position = pos;
            }

            if (patternIndex == -1)
                return false;
            else
            {
                metadataIndex = patternIndex;
                return true;
            }
        }

        public bool TryExtractingData()
        {
            long streamLength = baseStream.Length;

            long pos = metadataIndex;
            baseStream.Position = pos;

            int bufferSize = DefaultBufferSize;
            if ((pos + bufferSize) > streamLength)
                bufferSize = (int)(streamLength - pos);

            byte[] buffer = new byte[bufferSize];

            baseStream.Read(buffer, 0, bufferSize);

            string rawData = encoding.GetString(buffer);
            int footerIndex = rawData.IndexOf(ZoneMetadataHelper.footer);

            if (footerIndex == -1)
                return false;

            string data = rawData.Substring(0, footerIndex);
            data = data.Remove(0, ZoneMetadataHelper.header.Length);
            DLog.Log("Text : " + data);

            return true;
        }
    }
}
