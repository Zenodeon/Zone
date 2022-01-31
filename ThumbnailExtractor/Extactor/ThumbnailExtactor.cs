using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DebugLogger.Wpf;
using System.IO;

internal class ThumbnailExtactor 
{
    private int width = 256;
    private int height = 256;
    private int duration = 3;
    private int fps = 30;

    public async void GetFrame(string path, Action<MemoryStream> callback)
    {
        await Task.Run(() =>
            callback.Invoke(FFMPEGProccess.GetPNG(path, duration, fps, width, height)));
    }

    public async void GetFrames(string path, Action<MemoryStream> callback)
    {
        await Task.Run(() =>
            callback.Invoke(FFMPEGProccess.GetGIF(path, duration, fps, width, height)));
    }
}
