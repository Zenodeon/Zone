using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DebugLogger.Wpf;
using System.IO;

namespace Zone.ThumbnailExtractor.Extactor
{
    public class ThumbnailExtactor
    {
        private int width = 256;
        private int height = 256;
        private int duration = 4;
        private int fps = 30;

        public void GetFrame(string path, Action<MemoryStream> callback)
        {
            Task.Run(() =>
                callback.Invoke(FFMPEGProccess.GetPNGStream(path, width, height)));
        }

        public void GetFrame(string path, Action<BitmapImage> callback)
        {
            try
            {
                Task.Run(() =>
                    callback.Invoke(FFMPEGProccess.GetPNG(path, width, height)));
            }
            catch
            {
                DLog.Log(path);
            }
        }

        public void GetFrames(string path, Action<MemoryStream> callback)
        {
            Task.Run(() =>
                callback.Invoke(FFMPEGProccess.GetGIF(path, duration, fps, width, height)));
        }
    }
}