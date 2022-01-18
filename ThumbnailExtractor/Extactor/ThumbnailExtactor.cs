using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DebugLogger.Wpf;

public class ThumbnailExtactor 
{
    private int width = 256;
    private int height = 256;
    private int duration = 3;
    private int fps = 30;

    public async void GetFrames(string path, Action<BitmapImage> callback)
    {
        BitmapImage gif = new BitmapImage();
        await Task.Run(() =>
        {
            gif = FFMPEGProccess.ConvertToGIF(path, duration, fps, width, height);
            DLog.Log("ffmpeg done");
        });
        callback.Invoke(gif);      
    }

    //IEnumerator ApplyFrames(List<GIFFrame> frames, Action<List<GIFFrame>> callback)
    //{
    //    //yield return null;

    //    //foreach (GIFFrame frame in frames)
    //    //{
    //    //    frame.ApplyTexture();
    //    //    yield return new WaitForSeconds(0.001f);
    //    //}

    //    //callback.Invoke(frames);
    //    //Debug.Log("Apply done");
    //}
}
