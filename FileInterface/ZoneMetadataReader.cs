using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.FileInterface
{
    public class ZoneMetadataReader
    {
        private const int DefaultBufferSize = 4096;

        public FileStream baseStream;

        public ZoneMetadataReader(FileStream fileStream)
        {
            baseStream = fileStream;
        }

        public void FindMetadata()
        {
            Encoding encoding = Encoding.UTF8;
            long position = baseStream.Length;
            int bufferSize = DefaultBufferSize;

            // Allow up to two bytes for data from the start of the previous
            // read which didn't quite make it as full characters
            byte[] buffer = new byte[bufferSize + 2];
            char[] charBuffer = new char[encoding.GetMaxCharCount(buffer.Length)];

            while (position > 0)
            {
                int bytesToRead = Math.Min(position > int.MaxValue ? bufferSize : (int)position, bufferSize);

                position -= bytesToRead;
                baseStream.Position = position;
                baseStream.Read(buffer, 0, bytesToRead);

                //StreamUtil.ReadExactly(baseStream, buffer, bytesToRead);
            }

            DLog.Log("Buffer : " + buffer.Length);
        }

        // StreamUtil.cs:
        public static class StreamUtil
        {
            public static void ReadExactly(Stream input, byte[] buffer, int bytesToRead)
            {
                int index = 0;
                while (index < bytesToRead)
                {
                    int read = input.Read(buffer, index, bytesToRead - index);
                    if (read == 0)
                    {
                        throw new EndOfStreamException
                            (String.Format("End of stream reached with {0} byte{1} left to read.",
                                           bytesToRead - index,
                                           bytesToRead - index == 1 ? "s" : ""));
                    }
                    index += read;
                }
            }
        }
    }
}
