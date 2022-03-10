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

        public string lastDevicID { get; set; }
        public int sharedCount { get; set; }

        public List<string> tags { get; set; }

        public ZoneMetadata(string md5)
        {
            addedDT = DateTime.UtcNow;
            fileMD5 = md5;

            fileID = UUtility.GetMD5(fileMD5 + addedDT.ToString("dd:M:yyyy:H:m:s:ffff"));

            sharedCount = 0;
            tags = new List<string>();
        }

        public void CheckIfNewDevice()
        {
            if (lastDevicID != UUtility.deviceID)
                sharedCount++;
        }
    }
}