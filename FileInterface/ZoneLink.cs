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
    public class ZoneLink
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
            string fileText = await File.ReadAllTextAsync(filePath, Encoding.UTF8);

            byte[] fileData = await File.ReadAllBytesAsync(filePath);

            ZoneMetadataHelper.RemoveEmbeddedData(fileText, ref fileData);

            await File.WriteAllBytesAsync(filePath, fileData);
        }

        public void test(string filePath)
        {
            //ZoneMetadata zoneMetadata = new ZoneMetadata();
            //zoneMetadata.dummyFill();

            //DLog.Log("Json : " + ZoneMetadataHelper.GetEmbedDataString(zoneMetadata).Length);

            //byte[] embedData = ZoneMetadataHelper.GetEmbedData(zoneMetadata);
            //DLog.Log("Byte[] : " + embedData.Length);


            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                ZoneMetadataReader reader = new ZoneMetadataReader(fs);

                if(reader.LocateMetadata())
                {
                    DLog.Log("Located Header");
                    reader.TryExtractingData();
                }
                //int test = -1;

                //fs.Seek(-2, SeekOrigin.End);
                //test = fs.ReadByte();

                //DLog.Log(test + "");

                //using (StreamWriter sw = new StreamWriter(fs))
                //{
                //    ZoneMetadata zoneMetadata = new ZoneMetadata();
                //    zoneMetadata.dummyFill();

                //    fs.Seek(0, SeekOrigin.End);

                //    sw.WriteLine(Environment.NewLine + ZoneMetadataHelper.GetEmbedDataString(zoneMetadata));
                //}

                //DLog.Log(fs.Position + " pos before");

                //ReverseLineReader rr = new ReverseLineReader(fs, Encoding.UTF8);

                //IEnumerator<LineData> enumerator = rr.GetEnumerator();

                //enumerator.MoveNext();

                //LineData cl = enumerator.Current;

                //DLog.Log("Content : " + cl.content + " || " + "StartI : " + cl.startIndex + " || " + " || " + "Length : " + cl.length);

                //if (cl.startIndex != -1)
                //    QSFile.DeleteFilePart(fs, cl.startIndex, cl.length);

                //DLog.Log(fs.Position + " pos after");

                //DLog.Log(fs.Length + "");

                //fs.Seek(fs.Position, SeekOrigin.Begin);

                //using (StreamReader sr = new StreamReader(fs))
                //{
                //    using (StreamWriter sw = new StreamWriter(fs))
                //    {
                //        DLog.Log(fs.Position + " pos before");

                //        DLog.Log(sr.ReadLine());

                //        DLog.Log(fs.Position + " pos after");

                //        sw.WriteLine();
                //    }
                //}
                //sw.WriteLine(Environment.NewLine + ZoneMetadataHelper.GetEmbedDataString(zoneMetadata));
                //{
                //    ZoneMetadata zoneMetadata = new ZoneMetadata();
                //    zoneMetadata.dummyFill();

                //    fs.Seek(0, SeekOrigin.End);

                //    sw.WriteLine(Environment.NewLine + ZoneMetadataHelper.GetEmbedDataString(zoneMetadata));
                //}


            }
        }
    }
}
