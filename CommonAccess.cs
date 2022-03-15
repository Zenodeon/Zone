using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceId;

namespace Zone
{
    public static class CommonAccess
    {
        public static string ffmpegDirectory = @"C:\ffmpeg\";
        public static string ffmpegPath => $@"{ffmpegDirectory}\ffmpeg.exe";

        private static string _deviceMD5 = string.Empty;
        public static string deviceMD5
        {
            get
            {
                if (_deviceMD5 == string.Empty)
                    _deviceMD5 = GetDeviceMD5();
                return _deviceMD5;
            }
        }

        public static string GetDeviceMD5()
        {
            string deviceID = new DeviceIdBuilder().AddMachineName().AddMacAddress().ToString();
            return UUtility.GetMD5(deviceID);
        }
    }
}
