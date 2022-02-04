using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zone.FileInterface.Helper;

namespace Zone.FileInterface
{
    internal class ZoneMetadata
    {
        public string uniqueID = string.Empty;
        public long sharedCount;

        public List<string> tags = new List<string>();

        public void dummyFill()
        {
            uniqueID = "asdubasd89asd9ah7b7893";
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
            uniqueID = "sjdbf674gt9eubgf8734hs45";
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