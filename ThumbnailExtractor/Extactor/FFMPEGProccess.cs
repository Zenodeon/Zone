using System.IO;
using System.Diagnostics;

public static class FFMPEGProccess
{
    public static string ffmpegPath = @"C:\Users\Admin\Desktop\ffmpeg\bin\ffmpeg.exe";

    public static MemoryStream GetGIF(string path, int duration, int fps, int width, int height)
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

        using Stream ffmpegOutput = ffmpegProcess.StandardOutput.BaseStream;
        MemoryStream ffmpegMemory = new MemoryStream();

        ffmpegOutput.CopyTo(ffmpegMemory);
        return ffmpegMemory;
    }

    public static MemoryStream GetPNG(string path, int width, int height)
    {
        ProcessStartInfo ffmpegProcessInfo = new ProcessStartInfo()
        {
            FileName = ffmpegPath,
            Arguments = $"-i \"{path}\" -vf \"scale='if(gt(iw,ih), -1, {width})':'if(gt(ih,iw), -1, {height})',crop={width}:{height}\" -vframes 1 pipe:.png",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        Process ffmpegProcess = new Process();
        ffmpegProcess.StartInfo = ffmpegProcessInfo;
        ffmpegProcess.Start();

        using Stream ffmpegOutput = ffmpegProcess.StandardOutput.BaseStream;
        MemoryStream ffmpegMemory = new MemoryStream();

        ffmpegOutput.CopyTo(ffmpegMemory);
        return ffmpegMemory;
    }
}
