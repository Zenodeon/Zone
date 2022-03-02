using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zone.FileInterface.Helper;
using Zone.Database;

namespace Zone.FileInterface
{
    public class ZoneMetadata : IDatabaseItem
    {
        public string fileMD5 { get; private set; }
        public int sharedCount = 0;

        public List<string> tags = new List<string>();

        public ZoneMetadata(string md5)
        {
            fileMD5 = md5;
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