using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zone.FileInterface.Helper;
using Zone.Database;

namespace Zone.FileInterface
{
    public class ZoneMetadata
    {
        public string ver = "0.1";

        public string fileID { get; set; }

        public DateTime addedDT { get; set; }
        public string fileMD5 { get; set; }

        public int sharedCount { get; set; }
        public List<string> tags { get; set; }

        public ZoneMetadata(string md5)
        {
            addedDT = DateTime.UtcNow;
            fileMD5 = md5;

            GenerateFileID();

            sharedCount = 0;
            tags = new List<string>();
        }

        private void GenerateFileID()
        {
            //DLog.Log(creationDateTime.ToString());
        }

        public void dummyFill()
        {
            fileMD5 = "asdubasd89asd9ah7b7893";
            sharedCount = 432;

            tags.Clear();
            tags.Add("1234567890123456");
            tags.Add("test 1");
            tags.Add("test 2");
            tags.Add("test 3");
            tags.Add("test 4");
        }

        public void dummyFill2()
        {
            fileMD5 = "sjdbf674gt9eubgf8734hs45";
            sharedCount = 26;

            tags.Clear();
            tags.Add("test 0");
            tags.Add("test 5");
            tags.Add("test 6");
            tags.Add("test 7");
            tags.Add("test 8");
        }
    }
}