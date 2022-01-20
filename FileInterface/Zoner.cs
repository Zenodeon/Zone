using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone
{
    public class Zoner
    {
        string uriPath = @"D:\TestSite\TestSubjects\New folder (2)\";
        string media = "unknown-3.png";
        string media1 = "noted.gif";
        string media2 = "fumo_Cirno_city.mp4";
        string media3 = "1614912655857.webm";

        public void ApplyZoneMetadata()
        {
            File.AppendAllText(uriPath + media3, "||Test||");
        }
    }
}
