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

        public ZoneMetadataReader(FileStream fileStream)
        {
            baseStream = fileStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns true if Metadata Header is found</returns>
        public bool LocateMetadata()
        {
            long streamLength = baseStream.Length;
            int bufferSize = testBufferSize;

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

                patternIndex = buffer.IndexOfPattern(headerBytes);
                
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
                //char[] charArray = encoding.GetChars(buffer, 0, buffer.Length);
                DLog.Log("Final Index : " + metadataIndex);
                return true;
            }

        }

        public void TryExtractingData()
        {

        }
    }
}
