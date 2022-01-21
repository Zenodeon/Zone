using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone
{
    public class ZoneMetadata
    {
        public string testString = string.Empty;
        public int testInt;

        public List<string> testListString = new List<string>();
        public List<int> testListInt = new List<int>();

        public void dummyFill()
        {
            testString = "test text";
            testInt = 10312;

            testListString.Add("test 1");
            testListString.Add("test 2");
            testListString.Add("test 3");
            testListString.Add("test 4");

            testListInt.Add(1);
            testListInt.Add(2);
            testListInt.Add(3);
            testListInt.Add(4);
        }
    }
}
