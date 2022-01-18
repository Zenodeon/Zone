using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public static class FFMPEGProccess
{
    public static string ffmpegPath = @"C:\Users\Admin\Desktop\ffmpeg\bin\ffmpeg.exe";
    public static string ffprobePath = @"C:\Users\Admin\Desktop\ffmpeg\bin\ffprobe.exe";

    public static BitmapImage ConvertToGIF(string path, int duration, int fps, int width, int height)
    {
        ProcessStartInfo ffmpegProcessInfo = new ProcessStartInfo()
        {
            FileName = ffmpegPath,
            Arguments = $"-t {duration} -i \"{path}\" -vf \"fps={fps},scale='if(gt(iw,ih), -1, {width})':'if(gt(ih,iw), -1, {height})',crop={width}:{height},split[s0][s1];[s0]palettegen[p];[s1][p]paletteuse\" -loop 0 pipe:.gif",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        Process ffmpegProcess = new Process();
        ffmpegProcess.StartInfo = ffmpegProcessInfo;
        ffmpegProcess.Start();

        BitmapImage ffmpegImage = new BitmapImage();

        using Stream ffmpegOutput = ffmpegProcess.StandardOutput.BaseStream;
        MemoryStream ffmpegMemory = new MemoryStream();

        ffmpegOutput.CopyTo(ffmpegMemory);

        ffmpegImage.BeginInit();
        ffmpegImage.StreamSource = ffmpegMemory;
        ffmpegImage.CacheOption = BitmapCacheOption.OnLoad;
        ffmpegImage.EndInit();
        ffmpegImage.Freeze();

        return ffmpegImage;
    }
}
