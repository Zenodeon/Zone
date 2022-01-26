using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone
{
    public class ZoneMetadata
    {
        public long sharedCount;

        public List<string> tags = new List<string>();

        public void dummyFill()
        {
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
