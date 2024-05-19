using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ZenithEngine.Output
{
    public class FFMpeg : IDisposable
    {
        Task writeTask = null;
        Task diagnostic = null;

        Process process;

        int frameSize;
        byte[] bufferFrame;

        public FFMpeg(int width, int height, int fps, string extraArgs, string output)
        {
            string mainArgs = $"-f rawvideo -s {width}x{height} -pix_fmt rgb32 -r {fps} -i -";
            string args = $"{mainArgs} {extraArgs} \"{output}\" -y";
            process = new Process();
            process.StartInfo = new ProcessStartInfo("ffmpeg", args);
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            //process.StartInfo.RedirectStandardError = !settings.ffmpegDebug;
            process.Start();

            frameSize = width * height * 4;
            bufferFrame = new byte[frameSize];
        }

        public void Dispose()
        {
            if (writeTask != null) writeTask.Wait();
            process.StandardInput.Close();
            process.WaitForExit();
        }
    }
}
